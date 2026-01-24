using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PoliNote.Models.Configuration;
using MailKit.Net.Smtp;

namespace PoliNote.Services.Auth
{
    public class EmailSender
    {
        private readonly EmailSettings _settings;

        public EmailSender(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            // email text
            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                // connect
                await client.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.StartTls);

                // auth
                await client.AuthenticateAsync(_settings.Username, _settings.Password);

                // send
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Email sending error: {ex.Message}");
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
