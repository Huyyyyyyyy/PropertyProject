using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace PropertyProject.Models.mapContactEmail
{
    public class mapContactEmail
    {

        public void SendEmail(string toEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("nhanhanh.prop@gmail.com");

            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com"; //hoặc smtp.live.com
                smtp.Port = 587; 
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("nhanhanh.prop@gmail.com", "ugnmcjrhidjhgnqx");

                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }

       //create an otp
        public string GenerateOTP()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }




        //mask the email for security
        public string MaskEmail(String email)
        {
            int atIndex = email.IndexOf('@');

            if (atIndex > 1)
            {
                string maskedPart = new string('*', atIndex - 2);
                string maskedEmail = email.Substring(0, 2) + maskedPart + email.Substring(atIndex);
                return maskedEmail;
            }
            else
            {
                return email; 
            }
        }

    }
}