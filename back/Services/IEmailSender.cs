using Politics.Dtos;
using Politics.Model;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Services
{
  public interface IEmailSender
  {
    Task<Response> SendActivationEmail(User recipient, ActivationToken token);
    Task<Response> SendEmailConfirmation(UserProfileDto recipient, string newEmail, EmailChangeToken token);
  }
}
