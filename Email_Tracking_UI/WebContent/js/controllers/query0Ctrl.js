/*twitter311Module.controller('query1Ctrl', ['$scope', function buttonFunction($scope)
{
	var emailTo = document.getElementById("emailTo").value;
	var subject=document.getElementById("subject").value;
	var message=document.getElementById("message").value;
	var full_post=pos.concat(";",emailTo,";",subject,";",message);
	$.post("http://demomongodb.mybluemix.net/",full_post,
	          function(data) {
	              alert(data);
	          }
	          );
	
	}
]);*/
function buttonfunction()
{
	alert(hi);
var emailaddresses = document.getElementById("emailTo").value;
var emailsubject=document.getElementById("subject").value;
var emailcontent=document.getElementById("message").value;
var full_post=pos.concat(";",emailaddresses,";",emailsubject,";",emailcontent);
$.post("http://kc-sce-cs551-2.kc.umkc.edu/aspnet_client/qwer/RestService/EmailTrackingService.svc/email",full_post,
          function(data) {
              alert(data);
          }
          );
}