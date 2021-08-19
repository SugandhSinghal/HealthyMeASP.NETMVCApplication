using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.IO;

namespace HealthyMe.utils
{
    // This is to send bulk email
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail1,String toEmail2 ,String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("sugandh.singhal26@gmail.com", "HealthyMe");
            var to = new List<EmailAddress>
            {
                new EmailAddress(toEmail1,"Email1"),
                new EmailAddress(toEmail2,"Email2"),
              //  new EmailAddress("sugandh.singhal@yahoo.co.in","Email3")
             
            };
            //var to = new EmailAddress(toEmail, "");
            var plainTextContent = "Hey";
            var htmlContent = "<p>" + contents + "</p>";
            var showAllRecipients = false;
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent, showAllRecipients);
            var bytes = File.ReadAllBytes("C:\\Users\\Sugandha\\Desktop\\fileUpload.txt");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("fileUpload.txt", file);
            var response = client.SendEmailAsync(msg);

        }

    }
}