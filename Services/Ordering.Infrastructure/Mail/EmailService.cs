using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail;

public class EmailService : IEmailService
{
    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        EmailSettings = emailSettings.Value;
        Logger = logger;
    }

    private EmailSettings EmailSettings { get; }
    private ILogger<EmailService> Logger { get; }

    public async Task<bool> SendEmailAsync(Email email)
    {
        var client = new SendGridClient(EmailSettings.ApiKey);
        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress {
            Email = EmailSettings.ApiKey,
            Name = EmailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage).ConfigureAwait(false);

        Logger.LogInformation("Email sent.");

        if (response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.OK)
            return true;

        Logger.LogError("Email wasn't sent.");

        return false;
    }
}
