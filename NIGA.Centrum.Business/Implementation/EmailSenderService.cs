using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    public class EmailSenderService
    {
        /// <summary>
        /// Method implementaion for sending mail
        /// </summary>
        /// <param name="emailSenderModel"></param>
        /// <returns></returns>
        public bool SendMail(EmailSenderModel emailSenderModel, SmtpSettingsModel settingsModel)
        {
           
            try
            {
                var fromAddress = new MailAddress(settingsModel.from);
                var toAddress = new MailAddress(emailSenderModel.ToAddress);
                var smtp = new SmtpClient
                {
                    Host = settingsModel.host,
                    Port = settingsModel.port,
                    EnableSsl = settingsModel.enableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = settingsModel.defaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, settingsModel.password),

                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = emailSenderModel.Subject,
                    Body = emailSenderModel.Body,
                    IsBodyHtml = emailSenderModel.isHtml,
                })
                {
                    smtp.Send(message);
                    emailSenderModel.sentStatus = true;
                }
            }
            catch (Exception ex)
            {
                emailSenderModel.sentStatus = false;
            }
            return emailSenderModel.sentStatus;

        }
    }
}
