using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using MySqlX.XDevAPI;

namespace WebAPI.Tables.Help_Tables
{
    [System.Runtime.Versioning.UnsupportedOSPlatform("browser")]
    public class EmailClient : IDisposable
    {
        BackupDatabase dbBackup = new BackupDatabase();
        static bool mailSent = false;
        public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }

            mailSent = true;
        }

        public static void Send(string adminMail, string messageBody)
        {
            SmtpClient emailClient = new SmtpClient();
            MailAddress from = new MailAddress("reports@api.com", "reports" + (char)0xD8 + "Clayton", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(adminMail);
            MailMessage message = new MailMessage(from, to);
            message.Body = messageBody;

            emailClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            string userState = "test message1";
            emailClient.SendAsync(message, userState);
            message.Dispose();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
