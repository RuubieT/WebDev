using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace WebDevAPI.Models
{
    public class Contactform
    {
        [Key] public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public int MaxSubjectLength = 200;
        public int MaxDescriptionLength = 600;

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }

        public async Task SendMailAsync(string email)

        {

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("test@example.com", "Example User");

            var subject = "Sending with Twilio SendGrid is Fun";

            var to = new EmailAddress(email, "Example User");

            var plainTextContent = "and easy to do anywhere, even with C#";

            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

        }

    }
}
