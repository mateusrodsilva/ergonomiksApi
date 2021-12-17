using ergonomiks.Common.Models;
using ergonomiks.Common.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Common.Services
{
    public class MailService : IMailService
    {
        // to access JSON data at runtime
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            // creating a MimeMessage object (MimeKit) and sending it using the SMTPClient (MailKit)
            var email = new MimeMessage();

            // creates a new MimeMessage object and adds the Sender, To Address and Subject in that object
            // would be the data related to the message (subject, body) of the Request email and the data we get from the JSON file
            email.Sender = MailboxAddress.Parse(_mailSettings.From);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.To.Add(MailboxAddress.Parse(mailRequest.ToPassword));
            email.Subject = mailRequest.Subject;

            // if you have any attach (files) in the request object, turn the file into an attachment and add it to the email message with a body builder (class) attachment object
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            // here we have the HTML part of the email from the request's body property
            builder.HtmlBody = mailRequest.Body;

            // here add the attachment and HTML body to the email body
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.From, _mailSettings.Password);

            // sends the message using the SMTP 'SendMailAsync' method
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendAlertEmail(string emailUser, string passwordUser)
        {
            AlertRequest request = new AlertRequest();

            // put the path of the file where the HTML template is
            string CurrentFilePath = Directory.GetCurrentDirectory();
            string FilePath = CurrentFilePath.Replace("ergonomiks.Api", "ergonomiks.Common\\Templates\\");

            StreamReader streamReader = new StreamReader(FilePath + "Template.html");

            //StreamReader streamReader = new StreamReader(FilePath);
            string MailText = streamReader.ReadToEnd();
            streamReader.Close();

            // get the user's email
            request.ToEmail = emailUser;
            request.ToPassword = passwordUser;

            // replaces the email tag for the actual data (the email itself)
            MailText = MailText.Replace("[email]", request.ToEmail);
            MailText = MailText.Replace("[password]", request.ToPassword);

            // prepare the email
            var email = new MimeMessage();
            // put who will send
            email.Sender = MailboxAddress.Parse(_mailSettings.From);
            // put it to whoever will send it
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            // put a subject
            email.Subject = "Ergonomiks";

            // add a default body
            var builder = new BodyBuilder();
            // define the email from the string template (which was the HTML)
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();

            // connects with SMTP
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.From, _mailSettings.Password);

            // send the email
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

        }        
    }
}
