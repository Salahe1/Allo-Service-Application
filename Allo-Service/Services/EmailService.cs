    using System.Net;
    using System.Net.Mail;
    using System.Security.Cryptography;


namespace Allo_Service.Services
{


    public class EmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "eddinesalah1999@gmail.com";
        private readonly string _smtpPassword = "iovv rfzk nyqp wsuw"; // Ensure to secure your credentials

        public async Task SendVerificationEmailAsync(string email)
        {
            // Generate a verification token
            var verificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));//HttpContext.Session.GetString("VerificationToken");

            // Create the verification URL
            var verificationUrl = $"https://localhost:7062/Users/VerifyEmail?token={verificationToken}&email={email}";

            // Create the email message
            var mail = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = "Verify your email address",
                Body = $"Please click on this link to verify your email address: {verificationUrl}",
                IsBodyHtml = true
            };
            mail.To.Add(new MailAddress(email));

            // Send the email
            using (var smtpClient = new SmtpClient(_smtpHost, _smtpPort))
            {
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
                await smtpClient.SendMailAsync(mail);
            }
        }
    }

}
