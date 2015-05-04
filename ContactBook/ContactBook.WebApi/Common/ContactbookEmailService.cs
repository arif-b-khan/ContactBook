using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Configuration;
using System.Configuration;
using System.Net;
using ContactBook.Domain.Security;

namespace ContactBook.WebApi.Common
{
    public class ContactbookEmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            #region formatter
            string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
            string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
            #endregion
            var smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string emailFrom = await ContactBookCrypto.DecryptAsync(smtpSec.From);
            string userName = await ContactBookCrypto.DecryptAsync(smtpSec.Network.UserName);
            string password = await ContactBookCrypto.DecryptAsync(smtpSec.Network.Password);

            MailMessage msg = new MailMessage(emailFrom, message.Destination);
            //msg.From = new MailAddress(emailFrom);
            //msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            
            SmtpClient smtpClient = new SmtpClient();

            NetworkCredential networkCredential = new NetworkCredential(userName, password);
            smtpClient.Host = smtpSec.Network.Host;
            smtpClient.Port = smtpSec.Network.Port;
            smtpClient.EnableSsl = smtpSec.Network.EnableSsl;
            smtpClient.Credentials = networkCredential;

            Task retSend = Task.Run(() =>
            {
                smtpClient.SendAsync(msg, null);
            });
            
        }
    }
}