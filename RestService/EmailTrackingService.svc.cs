using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Util;
using System.ServiceModel.Activation;
using Wurfl;
using WURFL;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmailTrackingService" in code, svc and config file together.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmailTrackingService : IEmailTrackingService
    {


        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "get/{emailId}/{recipientId}")]
        public String get(string emailId, string recipientId)
        {



            var device = AnaysisStart.wurflContainer.GetDeviceForRequest(HttpContext.Current.Request.UserAgent);

            return device.GetCapability("device_os");


        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "unsubscribe/{recipientId}")]
        public String unsubscribe(string recipientId)
        {
            //find the record where recipientId = recipientId. Update isUnscribed = 1.
            //this field is checked before we send email to that user.
            //this is to comply with industry standards.

            return "You have been successfully unsubscribed.";
        }
        /// <summary>
        /// This method gets the User Agent and returns a representation of the device that made the request
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns>DeviceInfo</returns>
        public DeviceInfo getDeviceFromRequest(string userAgent)
        {
            var inputUserAgent = userAgent.ToLower();

            DeviceInfo dInfo = new DeviceInfo();

            if (inputUserAgent.Contains("iphone"))
            {
                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }
            else if (inputUserAgent.Contains("ipad"))
            {
                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }
            else if (inputUserAgent.Contains("sie-")) //"BenQ-Siemens (Openwave)"
            {
                dInfo.isMobile = true;

                //it is not smart phone
            }
            else if (inputUserAgent.Contains("blackberry"))
            {
                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }
            else if (inputUserAgent.Contains("android"))
            {
                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }

            // Windows Mobile (IEMobile) Windows CE (Windows CE) 
            else if (inputUserAgent.Contains("windows ce"))
            {
                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }

            // HTC Smart (HTC_Smart)
            else if (inputUserAgent.Contains("htc_smart"))
            {

                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }

            // LG (LG-) (LGE-) (LG/)
            else if (inputUserAgent.Contains("lg-") || inputUserAgent.Contains("lg/"))
            {
                dInfo.isMobile = true;
            }
            else if (inputUserAgent.Contains("lge-") || inputUserAgent.Contains("lge/"))
            {
                dInfo.isMobile = true;
            }

            // Motorola - Non-Android (MOT-) (MOTORO)
            else if (inputUserAgent.Contains("mot-") || inputUserAgent.Contains("motoro"))
            {
                dInfo.isMobile = true;
            }

            // Nokia (Nokia)
            else if (inputUserAgent.Contains("nokia"))
            {
                dInfo.isMobile = true;
            }

            // Palm (Palm) (webOS)
            else if (inputUserAgent.Contains("palm") || inputUserAgent.Contains("pre/"))
            {
                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }

            // Opera Mobile (Opera Mobi)
            else if (inputUserAgent.Contains("opera mobi"))
            {
                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }

            // Opera Mini (Opera Mini)
            else if (inputUserAgent.Contains("opera mini"))
            {
                dInfo.isMobile = true;
            }

            // Samsung Non-Android (SAMSUNG-SGH) 
            else if (inputUserAgent.Contains("samsung-sgh"))
            {
                dInfo.isMobile = true;
            }

            // SonyEricsson Non-Android (sonyericsson)
            else if (inputUserAgent.Contains("sonyericsson"))
            {
                dInfo.isMobile = true;
            }

            /**************************************************
            Everything else is a wired computer (PC/MAC)
            **************************************************/

            return dInfo;
        }

    }
}
