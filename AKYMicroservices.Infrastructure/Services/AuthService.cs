using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AKYMicroservices.Domain.Enums;
using AKYMicroservices.Domain.Interfaces;
using AKYMicroservices.Domain.ViewModals.Auth;
using AKYMicroservices.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AKYMicroservices.Infrastructure.Services;

public class AuthService : IAuthService
    {
        private readonly string _jwtSecret;
        private readonly UserManager<IApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public AuthService(
            IConfiguration configuration,
            UserManager<IApplicationUser> userManager,
            IHttpContextAccessor context,
            RoleManager<IdentityRole> roleManager
            )
        {
            _jwtSecret = configuration.GetSection("JwtSettings:Key").Value!;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _roleManager = roleManager;
        }
        
        public Task<string> GenerateJwtToken(IApplicationUser user, string role)
        {
            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role)
            };
            SecurityKey securityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSecret)
                );
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    issuer: _configuration.GetSection("JwtSettings")["Issuer"],
                    audience: _configuration.GetSection("JwtSettings")["Audience"],
                    signingCredentials: signingCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return Task.FromResult(token);
        }
        public async Task<LoginResultVm> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new LoginResultVm()
                {
                    Status = false,
                    ErrorMessage = "Invalid credentials."
                };
            }

            if (await _userManager.CheckPasswordAsync(user,password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userVm = new UserVm(user);
                return new LoginResultVm { Status = true, Token = await GenerateJwtToken(user,roles[0])};
            }

            return new LoginResultVm { Status = false, ErrorMessage = "Something went wrong." };
        }

        public async Task<RegisterResultVm> RegisterAsync(string email, string password,string firstName, string lastName)
        {
            if (email.Length == 0 || password.Length == 0 || firstName.Length == 0 || lastName.Length == 0 ||
                lastName.Length == 0 )
                return new RegisterResultVm()
                {
                    Status = false,
                    ErrorMessage = "All fields must be filled."
                };
            var existUser = await _userManager.FindByEmailAsync(email);
            if (existUser is not null)
                return new RegisterResultVm()
                {
                    Status = false,
                    ErrorMessage = "User already exists"
                };
            var userEmail = _context.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail is null)
                return new RegisterResultVm()
                {
                    Status = false,
                    ErrorMessage = "You are not authorized to create a user."
                };
            var createdBy = await _userManager.FindByEmailAsync(userEmail);
            if (createdBy is null)
                return new RegisterResultVm()
                {
                    Status = false,
                    ErrorMessage = "You are not authorized to create a user."
                };
            var creatorRoles = await _userManager.GetRolesAsync(createdBy);
            if (!creatorRoles.Contains("Admin"))
                return new RegisterResultVm()
                {
                    Status = false,
                    ErrorMessage = "You are not authorized to create a user."
                };
            var applicationUser = new ApplicationUser()
            {
                Email = email,
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                CreatedBy = createdBy.Id,
                Status = EntityStatus.Created,
            };

            await _userManager.CreateAsync(applicationUser, password);
            await _userManager.AddToRoleAsync(applicationUser, "User");
            return new RegisterResultVm()
            {
                Status = true,
                ErrorMessage = ""
            };
        }
    }
