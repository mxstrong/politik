using Politics.Dtos;
using Politics.Model;
using SendGrid;
using System.Threading.Tasks;

namespace Politics.Services
{
  public interface IEmailSender
  {
    Task<Response> SendActivationEmail(User recipient, ActivationToken token);
    Task<Response> SendEmailConfirmation(UserProfileDto recipient, string newEmail, EmailChangeToken token);
  }
}
