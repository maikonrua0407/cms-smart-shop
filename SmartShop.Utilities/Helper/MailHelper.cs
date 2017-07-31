using System;
using System.ComponentModel;
using System.Text;
using System.Reflection;
using System.Net.Mail;
using System.Configuration;
using System.Data;
using System.Web;
namespace SmartShop.Utilities.Helper
{
    public class MailHelper
    {
        //MailHelper.Send(mailFrom, mailTo, mailCc, subject, templateMailOrder);

        //public static bool Send(string to, string subject, string body)
        //{

        //    string strFrom = "admin@knic.vn";

        //    return Send(strFrom, to, subject, body);
        //}

        public static bool Send(string from, string nameFrom, string pass, string smtp, string smtpport, string ssl, string to, string subject, string body)
        {
            return Send(from, nameFrom, pass, smtp, smtpport, ssl, to, string.Empty, subject, body);
        }

        public static bool Send(string from, string nameFrom, string pass, string smtp, string smtpport,string ssl, string to, string cc, string subject, string body)
        {
            try
            {
                var message = new MailMessage { From = new MailAddress(from, nameFrom) };

                if (!string.IsNullOrEmpty(to))
                    foreach (var st in to.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(st))
                            message.To.Add(st);
                    }
                if (!string.IsNullOrEmpty(cc))
                    foreach (var st in cc.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(st))
                        {
                            if ("1".Equals(AppSettings.GetConfigString("SendMailBCC")))
                                message.Bcc.Add(st);
                            else
                                message.CC.Add(st);
                        }
                    }
                //message.Bcc.Add("lienhe@shoptretho.com.vn");
                message.Subject = subject;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = body;
                message.IsBodyHtml = true;

                if (string.IsNullOrEmpty(smtp))
                    smtp = "smtp.gmail.com";
                if (string.IsNullOrEmpty(smtpport))
                    smtpport = "587";
                var useSSl = ssl;
                if (string.IsNullOrEmpty(useSSl))
                    useSSl = "1";
                var cnt = 0;
                var client = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = "1".Equals(useSSl),
                    Host = smtp,
                    Port = int.Parse(smtpport),
                    UseDefaultCredentials = false,
                };
                var user = string.Empty;
                do
                {
                    try
                    {
                        user = from;
                        if (string.IsNullOrEmpty(user)) break;
                        client.Credentials = new System.Net.NetworkCredential(user,pass);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(message);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Common.OutputLog(HttpContext.Current, ex);
                    }
                    cnt++;
                } while (!string.IsNullOrEmpty(user));
                //object ob = "Test";
                //client.SendAsync(message,null);
                //message.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Common.OutputLog(HttpContext.Current, ex);
                return false;
            }
        }
        static void SendAsync()
        {
            //create the mail message
            MailMessage mail = new MailMessage();

            //set the addresses
            mail.From = new MailAddress("me@mycompany.com");
            mail.To.Add("you@yourcompany.com");

            //set the content
            mail.Subject = "This is an email";
            mail.Body = "this is the body content of the email.";

            //send the message
            SmtpClient smtp = new SmtpClient("127.0.0.1"); //specify the mail server address
            //the userstate can be any object. The object can be accessed in the callback method
            //in this example, we will just use the MailMessage object.
            object userState = mail;

            //wire up the event for when the Async send is completed
            smtp.SendCompleted += new SendCompletedEventHandler(SmtpClient_OnCompleted);

            smtp.SendAsync(mail, userState);
        }
        public static void SmtpClient_OnCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //Get the Original MailMessage object
            MailMessage mail = (MailMessage)e.UserState;

            //write out the subject
            string subject = mail.Subject;

            if (e.Cancelled)
            {
                Console.WriteLine("Send canceled for mail with subject [{0}].", subject);
            }
            if (e.Error != null)
            {
                Console.WriteLine("Error {1} occurred when sending mail [{0}] ", subject, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message [{0}] sent.", subject);
            }
        }

    }
}