using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface for sending email
    /// </summary>
    public interface IEmailSenderService
    {
        /// <summary>
        /// interface for sending mail
        /// </summary>
        /// <param name="emailSenderModel"></param>
        /// <returns></returns>
        bool SendMail(EmailSenderModel emailSenderModel);
    }
}
