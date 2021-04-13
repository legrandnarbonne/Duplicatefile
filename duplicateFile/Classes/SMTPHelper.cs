using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using duplicateFile.Classes.Exports;
using SimpleLogger;
using System.Windows.Forms;

namespace duplicateFile.Classes
{
    static class SMTPHelper
    {
        public static void sendReport(DataGridView dgvDoublons, DataGridView dgvError)
        {
            MailMessage mail = new MailMessage(Analyser.Config.mailFrom, Analyser.Config.ReportMail)
            {
                Subject = "Rapport d'analyse",
                Body = "Rapport d'analyse :\n" + Analyser.Stats.ToString()
            };

            SmtpClient client = new SmtpClient()
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = Analyser.Config.ServerSMTP
            };

            //MemoryStream msDupFile=null;

            bool sendNeeded = false;

            System.Net.Mime.ContentType pdfContentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);

            //report duplicate files
            if (Analyser.Config.AttDup)
            {
                MemoryStream msDupFile = PDFExport.PDFFromGridView(dgvDoublons, true);

                SimpleLog.Log("Preparing duplicate file report", SimpleLog.Severity.Exception);
                Attachment rap = new Attachment(msDupFile, pdfContentType);
                rap.ContentDisposition.FileName = "DupFile.pdf";
                mail.Attachments.Add(rap);
                SimpleLog.Log("file" + msDupFile.Length, SimpleLog.Severity.Exception);
                sendNeeded = sendIfNeeded(ref mail, client);

                msDupFile.Close();

            }
            // report stats
            if (Analyser.Config.Bdd & Analyser.Config.AttStats)
            {
                SimpleLog.Log("Preparing stats.pdf file", SimpleLog.Severity.Exception);
                var tempPath = Charts.ChartTools.draw(false);
                Attachment stat = new Attachment(tempPath, pdfContentType);
                stat.ContentDisposition.FileName = "stat.pdf";
                mail.Attachments.Add(stat);
                sendNeeded = sendIfNeeded(ref mail, client);
            }

            // report error
            if (Analyser.Config.AttError&& dgvError.RowCount>0)
            {
                var msError = PDFExport.PDFFromGridView(dgvError, false);

                SimpleLog.Log("Preparing error.pdf report", SimpleLog.Severity.Info2);
                Attachment rap = new Attachment(msError, pdfContentType);
                rap.ContentDisposition.FileName = "Error.pdf";
                mail.Attachments.Add(rap);
                SimpleLog.Log("file length " + msError.Length, SimpleLog.Severity.Exception);
                sendNeeded = sendIfNeeded(ref mail, client);

                msError.Close();


            }


            if (sendNeeded)
                try { client.Send(mail); }
                catch (Exception e) { SimpleLog.Log("Error sending email : " + e.Message, SimpleLog.Severity.Error); }


            SimpleLog.Log("Report sended", SimpleLog.Severity.Info2);

        }

        /// <summary>
        /// Send email if config "One mail" is set
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="client"></param>
        /// <returns> true if mail have been send</returns>
        private static bool sendIfNeeded(ref MailMessage mail, SmtpClient client)
        {
            if (Analyser.Config.OneMail)
            {
                try { client.Send(mail); }
                catch (Exception e) { SimpleLog.Log("Error sending email : " + e.Message, SimpleLog.Severity.Error); }
                mail = new MailMessage(Analyser.Config.mailFrom, Analyser.Config.ReportMail)
                {
                    Subject = "Rapport d'analyse (suite)",
                    Body = "Rapport d'analyse :\n" + Analyser.Stats.ToString()
                };
                return false;
            }

            return true;
        }

    }
}
