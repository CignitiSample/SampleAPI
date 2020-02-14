// <copyright file="EmailHelper.cs" company="ASTM">
// Copyright (c) ASTM. All rights reserved.
// </copyright>

namespace MCS.Test.Automation.Common.Helpers
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Contains all details of the Email Details and Setting
    /// </summary>
    public static class EmailHelper
    {

        public static void SendEmail(string mailApplicationName)
        {
            //// SendEmailFunctionality(mailApplicationName, EmailHelper.EmailBody, EmailHelper.Subject, string.Empty);
            SendEmailFunctionality(mailApplicationName, "Sample EMail", "Sample Subject", string.Empty);
        }
#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static void SendEmailFunctionality(string mailApplicationName, string body, string subject, string attachmentPath)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements must be documented
        {
            try
            {

                string sendingStatus = BaseConfiguration.EmailStatus;

                if (sendingStatus == "yes")
                {
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            System.Net.Mail.Attachment attachment;
                            string senderID = BaseConfiguration.EmailSenderUserID;
                            string senderPassword = BaseConfiguration.EmailSenderPwd;
                            string receiverID = BaseConfiguration.EmailReceiverUserID;
                            string ccID = BaseConfiguration.CcUserID;

                            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnValidateCertificate);
                            ServicePointManager.Expect100Continue = true;

                            switch (mailApplicationName)
                            {
                                case "Gmail":
                                    smtp.Host = "smtp.gmail.com";
                                    smtp.Port = 587;
                                    smtp.EnableSsl = true;
                                    break;
                                case "Cigniti":
                                    smtp.Host = "smtp.office365.com";
                                    smtp.EnableSsl = true;
                                    break;
                                default:
                                    smtp.Host = "mail.astm.org/owa";
                                    smtp.EnableSsl = true;
                                    break;
                            }

                            if (receiverID.Contains(","))
                            {
                                string[] receivers = receiverID.Split(',');
                                int count = receivers.Length;
                                for (int i = 0; i < count; i++)
                                {
                                    mail.To.Add(receivers[i]);
                                }
                            }
                            else
                            {
                                mail.To.Add(receiverID);
                            }

                            mail.From = new MailAddress(senderID);

                            if (ccID.Contains(","))
                            {
                                string[] ccIds = ccID.Split(',');
                                int count = ccIds.Length;
                                for (int i = 0; i < count; i++)
                                {
                                    mail.To.Add(ccIds[i]);
                                }
                            }
                            else
                            {
                                mail.CC.Add(ccID);
                            }

                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;

                            if (!string.IsNullOrEmpty(attachmentPath))
                            {
                                attachment = new System.Net.Mail.Attachment(attachmentPath);
                                mail.Attachments.Add(attachment);
                            }

                            smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
                            smtp.Send(mail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Exception : " + ex);
                throw;
            }
        }



        private static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

#pragma warning disable SA1201 // Elements must appear in the correct order
        /// <summary>
        /// Gets or sets gets the value of EmailBody
        /// </summary>
        public static string EmailBody
#pragma warning restore SA1201 // Elements must appear in the correct order
        {
            get { return EmailBody; }

            set { EmailBody = value; }
        }

        /// <summary>
        /// Gets or sets the Subject of Email
        /// </summary>
        public static string Subject
        {
            get { return Subject; }

            set { Subject = value; }
        }
    }
}