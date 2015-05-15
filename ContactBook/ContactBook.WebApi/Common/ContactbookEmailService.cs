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
using ContactBook.Domain.Common.Logging;
using ContactBook.Domain.IoC;
using SendGrid;

namespace ContactBook.WebApi.Common
{
    public class ContactbookEmailService : IIdentityMessageService
    {
        public  readonly ICBLogger _logger;

        public ContactbookEmailService()
        {
            _logger = DependencyFactory.Resolve<ICBLogger>();
        }

        public async Task SendAsync(IdentityMessage message)
        {
            try
            {

                #region formatter
                string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
                string html = "Please click on this link: <a href=\"" + message.Body + "\">link</a><br/>";

                html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
                #endregion

 
                var smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string emailFrom = smtpSec.From;
                string userName = await ContactBookCrypto.DecryptAsync(smtpSec.Network.UserName);
                string password = await ContactBookCrypto.DecryptAsync(smtpSec.Network.Password);
                
                var mailFrom = new MailAddress(emailFrom);
                var mailTo = new MailAddress(message.Destination);
                
                var sendGridMessage = new SendGridMessage();
                var mailMessage = new MailMessage();
                sendGridMessage.Subject = message.Subject;
                sendGridMessage.AddTo(message.Destination);
                sendGridMessage.From = new MailAddress(emailFrom);
                sendGridMessage.Text = text;
                sendGridMessage.Html = html;
                
                //SmtpClient smtpClient = new SmtpClient();

                NetworkCredential networkCredential = new NetworkCredential(userName, password);
                var webTransport = new Web(networkCredential);

                Task retSend = webTransport.DeliverAsync(sendGridMessage);

                retSend.Wait();
            }
            catch (AggregateException ex)
            {
                ex.Handle(hex =>
                {
                    _logger.Error("SendAynsc (AggregateException): " + hex.Message, hex);
                    return true;
                });
            }
            catch (Exception ex)
            {
                _logger.Error("SendAysnc: " + ex.Message, ex);
            }

        }
    }
}