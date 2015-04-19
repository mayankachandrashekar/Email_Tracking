using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace RestService
{
    public class Util
    {

        public static string getEmailFooter(string emailId, string receiverId)
        {
            string footer = string.Format(@"<div style='font-size:10px;color:#888888;font-family:Verdana;'>
<p>You are receiving this email because the email address was added to our subscription service.
If you prefer not to receive further emails from us,
<a href='http://kc-sce-cs551-2.kc.umkc.edu/aspnet_client/qwer/RestService/EmailTrackingService.svc/unsubscribe/{1}'>click here 
to unsubscribe</a>.
<img src='http://kc-sce-cs551-2.kc.umkc.edu/aspnet_client/qwer/RestService/EmailTrackingService.svc/get/{0}/{1}' width='1' height='1' />
</p>
</div>
", emailId, receiverId);

            return footer;
        }


        public static bool SendMail(string sendTo, string subject, string message, long emailId, long receiverId)
        {


            message = message + getEmailFooter(emailId.ToString(), receiverId.ToString());

            SmtpClient myHost = new SmtpClient("smtp.googlemail.com");
            myHost.UseDefaultCredentials =

            false;
            System.Net.

            NetworkCredential myCredential = new System.Net.NetworkCredential(Properties.Settings.Default.UN, Properties.Settings.Default.PW);
            myHost.Credentials = myCredential;

            myHost.Port = 587;

            myHost.EnableSsl =

            true;
            try
            {

                //This is the message
                MailMessage theMessage = new MailMessage(Properties.Settings.Default.UN, sendTo, subject, message);
                theMessage.IsBodyHtml = true; // must send as HTML
                //Send the message
                myHost.Send(theMessage);


            }

            catch (Exception ex)
            {


                return false;
            }

            return true;
        }


    }
}