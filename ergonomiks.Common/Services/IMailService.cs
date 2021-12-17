using ergonomiks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Common.Services
{
    public interface IMailService
    {
        // to send an email manually 
        Task SendEmailAsync(MailRequest mailRequest);

        // to send an email automatically with template
        Task SendAlertEmail(string emailUser, string passwordUser);
    }
}
