using Microsoft.Extensions.Configuration;
using Politics.Dtos;
using Politics.Model;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Politics.Services
{
  public class EmailSender : IEmailSender
  {
    public EmailSender(IConfiguration config)
    {
      _config = config;
    }
    private readonly IConfiguration _config;

    public async Task<Response> SendActivationEmail(User recipient, ActivationToken token)
    {
      var subject = "Aktyvuokite savo Politik paskyrą";
      var to = new EmailAddress(recipient.Email);
      var plainTextContent = $"Sveiki, {recipient.DisplayName}," +
          " Norėdami užbaigti savo registraciją paspauskite žemiau esančią nuorodą." +
          " Jei nekūrėte Politik paskyros, ignoruokite šį laišką.";
      var htmlContent = $"<html><body><h2 style=\"font-size: 36\">Sveiki, {recipient.DisplayName},</h2><br>" +
          "<div style=\"font-size: 20;\">Norėdami užbaigti savo registraciją " +
          $"<a style=\"color: blue\" href=\"https://politikapi.azurewebsites.net/api/auth/activate/{token.Id}\">paspauskite čia</a><br><br>" +
          "Jei nekūrėte Politik paskyros, ignoruokite šį laišką.</div></body></html>";
      return await SendMail(subject, to, plainTextContent, htmlContent);
    }

    public async Task<Response> SendEmailConfirmation(UserProfileDto recipient, string newEmail, EmailChangeToken token)
    {
      var subject = "Patvirtinkite savo Politik paskyros el. pašto adreso keitimą";
      var to = new EmailAddress(newEmail);
      var plainTextContent = $"Sveiki, {recipient.DisplayName}," +
          " Norėdami pakeisti Politik el. pašto adresą paspauskite žemiau." +
          " Jei nenorėjote pakeisti savo el. pašto praneškite administracijai ir pasikeiskite Politik slaptažodį.";
      var htmlContent = $"<html><body><h2 style=\"font-size: 36\">Sveiki, {recipient.DisplayName},</h2><br>" +
          "<div style=\"font-size: 20;\">Norėdami užbaigti savo registraciją " +
          $"<a style=\"color: blue\" href=\"https://politikapi.azurewebsites.net/api/auth/confirmEmailChange/{token.Id}\">paspauskite čia</a><br><br>" +
          "Jei nekūrėte Politik paskyros, ignoruokite šį laišką.</div></body></html>";
      return await SendMail(subject, to, plainTextContent, htmlContent);
    }

    private async Task<Response> SendMail(string subject, EmailAddress to, string plainTextContent, string htmlContent)
    {
      var apiKey = _config.GetValue<string>("SendGridApiKey");
      var client = new SendGridClient(apiKey);
      var from = new EmailAddress("mantas.ptakauskas@mif.stud.vu.lt", "Politik");
      var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
      var response = await client.SendEmailAsync(msg);
      return response;
    }

  }
}
