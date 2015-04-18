using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEmailTrackingService
    {      
       /*
        [OperationContract]
        String get(string emailId, string recipientId);*/

        [OperationContract]
        String unsubscribe(String receiverId);

        [OperationContract]
        String get(String idEmail, String receiverId);

        [OperationContract]
        String emailInsert(String senderId, String idEmail, String emailSubject);

        [OperationContract]
        String[] senderSubjects(String senderId);

       
    }
    
}
