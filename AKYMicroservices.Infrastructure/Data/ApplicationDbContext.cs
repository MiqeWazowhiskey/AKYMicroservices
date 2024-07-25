using System.Reflection;
using AKYMicroservices.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AKYMicroservices.Infrastructure.Data;

public class ApplicationDbContext: IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(DbContextOptions options,IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    DbSet<Email> Emails { get; set; }
}
