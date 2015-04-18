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
using System.Data.SqlClient;
using System.Configuration;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmailTrackingService" in code, svc and config file together.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmailTrackingService : IEmailTrackingService
    {
        /*

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "get/{emailId}/{recipientId}")]
        public String get(string emailId, string recipientId)
        {



            var device = AnaysisStart.wurflContainer.GetDeviceForRequest(HttpContext.Current.Request.UserAgent);

            return device.GetCapability("device_os");


        }
         */

        /*

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "unsubscribe/{recipientId}")]
        public String unsubscribe(string recipientId)
        {
            //find the record where recipientId = recipientId. Update isUnscribed = 1.
            //this field is checked before we send email to that user.
            //this is to comply with industry standards.

            return "You have been successfully unsubscribed.";
        }
         
         */
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

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "unsubscribe/{receiverId}")]
        public String unsubscribe(String receiverId)
        {

            String done = "";

            //Declare Connection by passing the connection string from the web config file
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

            //Open the connection
            conn.Open();

            Boolean unsubscribe = true;


             SqlCommand cmd1 =
                   new SqlCommand("UPDATE receiverDetails SET Unsubscribed =@unsubscribe" +
                       " WHERE receiver_id ='" + receiverId + "'", conn);
                
                cmd1.Parameters.AddWithValue("@unsubscribe", unsubscribe);
                int rows = cmd1.ExecuteNonQuery();
                conn.Close();


            return "Subscribed";

        }


        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "get/{idEmail}/{receiverid}")]
        public String get(String idEmail, String receiverId)
        {

            //        Console.WriteLine("Received emailId is:" + emailId);


            /*
           var device = AnaysisStart.wurflContainer.GetDeviceForRequest(HttpContext.Current.Request.UserAgent);


           return string.Format("You entered: \n {0} \n {1} \n {2} \n {3} \n {4}"
               , device.GetCapability("device_os"),device.GetCapability("device_os_version"),device.GetVirtualCapability("is_mobile"), device.GetCapability("is_tablet"), device.GetVirtualCapability("is_smartphone"));
           */
            //return emailId;

         //   String status = "";
            String device ="";
            try
            {
                device = checkRequest();
                String ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;//getIPAddress();
                String timeStamp = new DateTime().ToString();//GetTimestamp(new DateTime());
                Boolean unsubscribed = false;


                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

                //Open the connection
                conn.Open();



                //Declare the sql command
                SqlCommand cmd = new SqlCommand
                    ("Insert into receiverDetails(receiver_id,id_email,device_client,timestamp,IP_Addr,Unsubscribed)values('" + receiverId + "','" + idEmail + "','" + device + "','" + timeStamp + "','" + ipAddress + "','" + unsubscribed + "')", conn);

                //Execute the insert query
                int ret = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                //close the connection

                conn.Open();
                SqlCommand readerCountCmd = new SqlCommand("select read_count from senderDetails where id_email='" + idEmail + "'", conn);

                SqlDataReader reader = readerCountCmd.ExecuteReader();


                int readCount = 0;
                while (reader.Read())
                {
                    readCount = Convert.ToInt32(reader[0].ToString());
                }


                conn.Close();

                conn.Open();
                SqlCommand cmd1 =
                   new SqlCommand("UPDATE senderDetails SET read_count =@readerCount" +
                       " WHERE id_email ='" + idEmail + "'", conn);
                readCount++;
                cmd1.Parameters.AddWithValue("@readerCount", readCount);
                int rows = cmd1.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
            }


         //   status = "success";

            return device;
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        protected string getIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public String checkRequest()
        {
            String deviceType = "";
            try
            {

                // String request = "";
                var device = AnaysisStart.wurflContainer.GetDeviceForRequest(HttpContext.Current.Request.UserAgent);

                String is_mobile;
                String is_smartphone;
                String is_tablet;

                is_mobile = device.GetVirtualCapability("is_mobile");
                is_smartphone = device.GetVirtualCapability("is_smartphone");
                is_tablet = device.GetCapability("is_tablet");

                if (is_mobile.Equals("true"))
                {
                    if (is_smartphone.Equals("true"))
                    {
                        deviceType = "Mobile-SmartPhone";
                    }
                    else if (is_tablet.Equals("true"))
                    {
                        deviceType = "Mobile-Tablet";
                    }
                    else
                    {
                        deviceType = "Mobile";
                    }
                }
                else
                {
                    deviceType = "PC";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return deviceType;

        }





        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "emailInsert/{senderId}/{idEmail}/{emailSubject}")]
        public String emailInsert(String senderId, String idEmail, String emailSubject)
        {
            String status;
            status = "";
            int read_count = 0;

            try
            {

                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

                //Open the connection
                conn.Open();



                //Declare the sql command
                SqlCommand cmd = new SqlCommand
                    ("Insert into senderDetails(sender_id,id_email,email_subject,read_count)values('" + senderId + "','" + idEmail + "','" + emailSubject + "'," + read_count + ")", conn);

                //Execute the insert query
                int ret = cmd.ExecuteNonQuery();
                cmd.Dispose();
                //close the connection
                conn.Close();



                if (ret == 1)
                    status = "success";
                else
                    status = "fail";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return status;

        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getSubjects/{senderId}")]
        public String[] senderSubjects(String senderId)
        {
            String[] subjects = { "" };

            try
            {
                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

                //Open the connection
                conn.Open();
                SqlCommand getSubjectCountCmd = new SqlCommand("select count (email_subject) from senderDetails where sender_id='" + senderId + "'", conn);

                int subjectsCount = Convert.ToInt16(getSubjectCountCmd.ExecuteScalar().ToString());



                subjects = new String[subjectsCount];



                SqlCommand getSubjectCmd = new SqlCommand("select email_subject from senderDetails where sender_id='" + senderId + "'", conn);

                SqlDataReader reader = getSubjectCmd.ExecuteReader();


                int i = 0;
                while (reader.Read())
                {
                    subjects[i++] = reader[0].ToString();
                }
                conn.Close();
                return subjects;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return subjects;


        }


    }
}
