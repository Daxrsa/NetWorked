using JobService.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace JobService.Services
{
    public class EmailService: IEmail
    {
        public string SendEmail()
        {
            string senderEmail = "networked758@gmail.com";
            string senderPassword = "pllvflwxgwjletje";

            string recipientEmail = "ttahirifitore@gmail.com";

            MailMessage mail = new MailMessage(senderEmail, recipientEmail);
            mail.Subject = "Hello from .NET";
            mail.Body = "This is a test email sent from a .NET application.";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully.");
                return "Email sent successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending the email: " + ex.Message);
                return "An error occurred while sending the email: " + ex.Message;
            }
        }
    }
}
