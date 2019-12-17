using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FinancialPortal.Extensions
{
    public  static class InvitationsExtensions
    {
        public static async Task EmailInvitation(this Invitations invitations)
        {
            var Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var callbackUrl = Url.Action("AcceptInvitation", "Account", new { recipientEmail = invitations.RecipientEmail, code = invitations.Code }, protocol: HttpContext.Current.Request.Url.Scheme);
            var from = $"Financial Portal<{WebConfigurationManager.AppSettings["emailfrom"]}>";

            var emailMessage = new MailMessage(from, invitations.RecipientEmail)
            {
                Subject = $"You have been invited to join the Financial Portal Application!",
                Body = $"Please accept this invitation and register as a new household member <a href=\"{callbackUrl}\">here</a>",
                IsBodyHtml = true
            };

            var svc = new EmailService();
            await svc.SendAsync(emailMessage);
        }
    }
}