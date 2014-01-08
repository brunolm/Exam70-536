using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;
using System.Net;

namespace Exam70_536
{
    /// <summary>
    /// Questions:
    /// - How to send an email?
    /// - How to attach a file?
    /// - How to write as HTML?
    /// </summary>
    [TestClass]
    public class MailingTest
    {
        [TestMethod]
        public void SendEmailTest()
        {
            MailMessage email = new MailMessage();

            email.Priority = MailPriority.High;

            email.From = new MailAddress("brunolm@mailinator.com");          // Gmail credentials ignore this prop
            email.Sender = new MailAddress("brunolm-sender@mailinator.com"); // Gmail credentials ignore this prop

            email.To.Add(new MailAddress("exam70536test@gmail.com"));
            
            email.Subject = "Exam 70-536 test project e-mail test";
            email.IsBodyHtml = true;
            email.Body = "It is over <b>NINE THOUSAND!!!</b>";

            email.Attachments.Add(new Attachment(@"..\..\..\Exam70-536\MailingTest.cs"));


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("exam70536test@gmail.com", "exam70536test");

            try
            {
                smtp.Send(email);
            }
            catch (SmtpException)
            {
                // can fail due to the provider
            }
        }
    }
}
