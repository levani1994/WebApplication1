using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net.Mail;

namespace WebApplication1.Models
{
    public class Helper
    {
        public void SendMail(string fromMail, string fromName, string toMail, string subject, string body, bool isHtml)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isHtml;
            mail.From = new MailAddress(fromMail, fromName);
            mail.To.Add(toMail);
            mail.Subject = subject;
            mail.Body = body;


           SmtpClient SmtpServer = new SmtpClient("***");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("****", "****");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);


           
        }

        static public string AuthKey = "ef28fd9d2a92dd4846d586bc555772e27f70d5e9745c47f71d21d34bc27048a5";
        static public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 32)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public static string Random32()
        {
            return Guid.NewGuid().ToString("N");  
        }

    }

}  
