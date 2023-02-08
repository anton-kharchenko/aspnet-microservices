using Ordering.Application.Models.Email;

namespace Ordering.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Email email);
}
