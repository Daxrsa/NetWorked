using JobService.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace JobService.Services
{
    public class EmailService: IEmail
    {
        public string SendEmail(string email, string username, string job)
        {
            string senderEmail = "networked758@gmail.com";
            string senderPassword = "pllvflwxgwjletje";

            string recipientEmail = email;

            MailMessage mail = new MailMessage(senderEmail, recipientEmail);
            mail.Subject = $"[{job}]Applied successfully!";
            mail.Body = $"Dear {username},\nYou have successfully applied to job position \"{job}\". Please do not reply to this email. \nBest of luck,\nTeam Networked";

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
