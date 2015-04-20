using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService
{
    public class DeviceInfo
    {
        public bool isMobile { get; set; }
        public bool isSmartPhone { get; set; }
        public bool isTablet { get; set; }
       
        /// <summary>
        /// read-only field. if not mobile, it is a desktop
        /// </summary>
        public bool isDesktop{

            get{return !isMobile;}

        }
    }

    public class EmailInfo
    {
        public long emailId {get;set;}
        public long receiverId { get; set; }
        public int readCount;
        //public 

    }

}