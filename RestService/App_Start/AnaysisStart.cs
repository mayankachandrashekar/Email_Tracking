using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService
{
    public class AnaysisStart
    {
        public static WURFL.IWURFLManager wurflContainer;

        public static string getEmailFooter(string emailId, string recipientId)
        {
            string footer = string.Format(@"<div style='font-size:10px;color:#888888;font-family:Verdana;'>
<p>You are receiving this email because you the email address was added to our subscription service.
If you prefer not to receive further emails from us,
<a href='http://kc-sce-cs551-2.kc.umkc.edu/aspnet_client/qwer/RestService/EmailTrackingService.svc/unsubscribe/{1}'>click here 
to unsubscribe</a>.
<img src='http://kc-sce-cs551-2.kc.umkc.edu/aspnet_client/qwer/RestService/EmailTrackingService.svc/get/{0}/{1}' width='1' height='1' />
</p>
</div>
", emailId, recipientId);

            return footer;
        }
    }

    
}