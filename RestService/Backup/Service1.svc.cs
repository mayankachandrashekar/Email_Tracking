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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


         [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "get/{emailId}" )]
         

        public String get(String emailId)
        {

    //        Console.WriteLine("Received emailId is:" + emailId);

            var mgr = WURFLManagerBuilder.Build(new WURFL.Aspnet.Extensions.Config.ApplicationConfigurer());
            var device = WURFLManagerBuilder.Instance.GetDeviceForRequest(HttpContext.Current.Request.UserAgent);

            return device.GetCapability("device_os");
            
            //return emailId;
        }

        public void checkRequest()
        {
            

        }
    }
}
