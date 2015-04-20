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

            //String done = "";

            //Declare Connection by passing the connection string from the web config file
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

            //Open the connection
            conn.Open();

            Boolean unsubscribe = true;


             SqlCommand cmd1 =
                   new SqlCommand("UPDATE receiverInfo SET Unsubscribed =@unsubscribe" +
                       " WHERE receiverid ='" + receiverId + "'", conn);
                
                cmd1.Parameters.AddWithValue("@unsubscribe", unsubscribe);
                int rows = cmd1.ExecuteNonQuery();
                conn.Close();


            return "Subscribed";

        }


        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "get/{emailId}/{receiverid}")]
        public String get(String emailId, String receiverId)
        {

          
            String device ="";
            DeviceInfo deviceInfo;
            try
            {
                device = checkRequest();
                deviceInfo = getDeviceFromRequest(System.Web.HttpContext.Current.Request.UserAgent);

                String ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;//getIPAddress();
                DateTime thisDay = DateTime.UtcNow; //making it UTC in case we need to display info for other timezones

                String timeStamp = thisDay.ToString();//new DateTime().ToString();//GetTimestamp(new DateTime());
                Boolean unsubscribed = false;


                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

                //Open the connection
                conn.Open();



                //Declare the sql command
                SqlCommand cmd = new SqlCommand
                    ("Insert into receiverDetails(receiverid,emailid,device_client,timestamp,IP_Addr, isMobile, isSmartPhone, isTablet, isDeskTop)values('" + receiverId + "','" + emailId + "','" + device + "','" + timeStamp + "','" + ipAddress + "','" + deviceInfo.isMobile + "','" + deviceInfo.isSmartPhone + "','" + deviceInfo.isTablet + "','" + deviceInfo.isDesktop + "')", conn);

                //Execute the insert query
                int ret = cmd.ExecuteNonQuery();
               
                SqlCommand cmd1 =
                   new SqlCommand("UPDATE emailInfo SET read_count =read_count+1" +
                       " WHERE emailid ='" + emailId + "'", conn);
               
                int rows = cmd1.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        public DeviceInfo getDeviceInfo()
        {
            DeviceInfo dInfo = new DeviceInfo();

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
                dInfo.isMobile = true;
            }
            
            if (is_smartphone.Equals("true"))
            {
                dInfo.isMobile = true;
                dInfo.isSmartPhone = true;
            }
            
            if (is_tablet.Equals("true"))
            {
                dInfo.isMobile = true;
                dInfo.isTablet = true;
            }

            return dInfo;
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




        /// <summary>
        /// Sends email to one or more recipient.
        /// </summary>
        /// <param name="emailAddresses"></param>
        /// <param name="emailSubject"></param>
        /// <param name="emailContent"></param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat=WebMessageFormat.Json, UriTemplate = "email")]
        public String email(String emailAddresses, String emailSubject, String emailContent)
        {
            if (Util.SendMail(emailAddresses, emailSubject, emailContent, 12, 5))
            {
                return "Successfuly sent message";
            }

            if (string.IsNullOrEmpty(emailAddresses) || string.IsNullOrEmpty(emailSubject) || string.IsNullOrEmpty(emailContent))
                return "faulure: invalid request";

            String status;
            status = "";
            int read_count = 0;

            string ret = "";

            try
            {

                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

                //Open the connection
                conn.Open();

                long? emailId = null;

                //create email trace
                //Declare the sql command
                SqlCommand cmd = new SqlCommand
                    ("Insert into emailInfo(email_subject,read_count) values('" + emailSubject + "', 0) Set @emailId = @@identity", conn);

                SqlParameter param = new SqlParameter("@emailId", System.Data.SqlDbType.BigInt);
                param.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(param);

                //Execute the insert query
                cmd.ExecuteNonQuery();



                emailId = param.Value as long?; //this will give me the identity field value inserted

                cmd.Dispose();

                foreach (string emailAddress in emailAddresses.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {


                    //get receiver by email, if exists - create one if not.
                    long? receiverId = createRecieverIfNotExists(emailAddress, conn);

                    if(receiverId == -1) //must have subscribed it.
                    {
                        if (emailAddresses.Split(new char[] { ',' }).Count() > 1)
                            return "This reciever has unsubscribed";

                        //anyway continue

                        continue;
                    }

                    //so far so good. now let us send out the email(s)


                    if (Util.SendMail(emailAddresses, emailSubject, emailContent, emailId.Value, receiverId.Value))
                    {
                        return "Successfuly sent message";
                    }

                }

                status = "success";

            }
            catch (Exception e)
            {
                status = "failure: " + e.Message;
            }


            return status;

        }

        private long? createRecieverIfNotExists(string emailAddress, SqlConnection conn)
        {
           
            //do not send email to unsubscribed users
            SqlCommand cmd = new SqlCommand
                        ("Select CASE WHEN unsubscribed = 1 THEN -1 ELSE  receiverId END receiverId FROM receiverInfo where emailaddress='" + emailAddress.Trim() + "'", conn);

            
            //Execute the insert query
            long? recieverId = cmd.ExecuteScalar() as long?;

            if(recieverId == null) //could not find this receiver/user by email. let us create one
            {
                //make sure receiver is not unsubscribed initially
                cmd = new SqlCommand
               ("Insert into receiverInfo(emailaddress, unsubscribed) values('" + emailAddress + "','0')  Set @receiverId = @@identity", conn);

                SqlParameter param = new SqlParameter("@receiverId", System.Data.SqlDbType.BigInt);
                param.Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();

                recieverId = param.Value as long?;
            }

            return recieverId;
        }

        

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "subjectStats")]
        public List<String> subjectStats()
        {
            List<String> result = new List<string>();

            try
            {
                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

                //Open the connection
                conn.Open();
                SqlCommand getSubjectCountCmd = new SqlCommand("select sum(read_count) read_count, email_subject from emailInfo group by email_subject", conn);

                SqlDataReader reader = getSubjectCountCmd.ExecuteReader();
                //int subjectsCount = Convert.ToInt16(getSubjectCountCmd.ExecuteScalar().ToString());

                while (reader.Read())
                {
                   result.Add(reader["email_subject"].ToString() +"="+ reader["read_count"].ToString());
                   
                }


                
                conn.Close();
            

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return result;


        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "topReaders")]
        public List<String> topReaders()
        {
            List<String> result = new List<string>();

            try
            {
                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

                //Open the connection
                conn.Open();
                SqlCommand getSubjectCountCmd = new SqlCommand(@"
select top 5 emailAddress,COUNT(*) reads  from receiverInfo r
join receiverDetails rd on rd.receiverId=r.receiverId

group by emailAddress
order by COUNT(*) desc", conn);

                SqlDataReader reader = getSubjectCountCmd.ExecuteReader();
                //int subjectsCount = Convert.ToInt16(getSubjectCountCmd.ExecuteScalar().ToString());

                while (reader.Read())
                {
                    result.Add(reader["emailAddress"].ToString() + "=" + reader["reads"].ToString());

                }



                conn.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return result;


        }

       [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getReadCount/{idEmail}")]
        public String[] readCount(String idEmail)
        {
            String[] readList = new String[4];

            try
            {
                //Declare Connection by passing the connection string from the web config file
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

                //Open the connection
                conn.Open();
                SqlCommand getDeviceList = new SqlCommand("select device_client from receiverDetails where emailid='" + idEmail + "'", conn);


                SqlDataReader reader = getDeviceList.ExecuteReader();


              //  int i = 0;
                int mobileCount = 0;
                int smartPhoneCount = 0;
                int pcCount = 0;
                int tabletCount = 0;
                while (reader.Read())
                {
                    if (reader[0].ToString().Contains("Mobile"))
                    {
                        mobileCount++;
                        if (reader[0].ToString().Equals("Mobile-Tablet"))
                            tabletCount++;
                        else if (reader[0].ToString().Equals("Mobile-SmartPhone"))
                            smartPhoneCount++;
                    }
                    else if (reader[0].ToString().Equals("PC"))
                        pcCount++;
                }


                conn.Close();
                readList[0] = "Mobile-" + mobileCount;
                readList[1] = "SmartPhone-" + smartPhoneCount;
                readList[2] = "Tablet-" + tabletCount;
                readList[3] = "PC-" + pcCount;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return readList;

        }


       [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getReadCountAll/")]
       public String[] readEntireCount()
       {
           String[] readList = new String[4];

           try
           {
               //Declare Connection by passing the connection string from the web config file
               SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["emailReader"].ConnectionString);

               //Open the connection
               conn.Open();
               SqlCommand getDeviceList = new SqlCommand("select device_client from receiverDetails", conn);


               SqlDataReader reader = getDeviceList.ExecuteReader();


               //  int i = 0;
               int mobileCount = 0;
               int smartPhoneCount = 0;
               int pcCount = 0;
               int tabletCount = 0;
               while (reader.Read())
               {
                   if (reader[0].ToString().Contains("Mobile"))
                   {
                       mobileCount++;
                       if (reader[0].ToString().Equals("Mobile-Tablet"))
                           tabletCount++;
                       else if (reader[0].ToString().Equals("Mobile-SmartPhone"))
                           smartPhoneCount++;
                   }
                   else if (reader[0].ToString().Equals("PC"))
                       pcCount++;
               }


               conn.Close();
               readList[0] = "Mobile-" + mobileCount;
               readList[1] = "SmartPhone-" + smartPhoneCount;
               readList[2] = "Tablet-" + tabletCount;
               readList[3] = "PC-" + pcCount;


           }
           catch (Exception e)
           {
               Console.WriteLine(e.Message);
           }
           return readList;

       }


    }
}
