namespace AKYMicroservices.Domain.ViewModals.Email;

public class EmailVm:BaseVm
{
    public string From { get; set; }
    public string To { get; set; }
    public string? Subject { get; set; }
    public string? Content { get; set; }

    public EmailVm(){}
    public EmailVm(Entities.Email mail)
    {
        if (mail is null) return;
        From = mail.From;
        To = mail.To;
        Subject = mail.Subject ?? null;
        Content = mail.Content ?? null;
    }
}
