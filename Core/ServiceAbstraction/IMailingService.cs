using Shared.Dto;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    /// <summary>
    /// Contract for the email sending service.
    /// Moved to ServiceAbstraction so Infrastructure.Persistence no longer depends on Service.
    /// </summary>
    public interface IMailingService
    {
        Task SendEmailAsync(Email email);
    }
}