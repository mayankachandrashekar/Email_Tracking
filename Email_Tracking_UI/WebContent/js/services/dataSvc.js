twitter311Module.factory('dataSvc', ['$http',
    function ($http) {
        return {
            query1: function() {
                return $http.get('http://kc-sce-cs551-2.kc.umkc.edu/aspnet_client/qwer/RestService/EmailTrackingService.svc/subjectStats');
                //return $http.get('http://localhost:8834/Trial1/MasterServlet?query1=KCMO', {timeout: 500000});
            },
            query2: function() {
                //return $http.get('queryResults/DepartmentGeo.json');
            	 return $http.get('http://localhost:8834/Trial1/MasterServlet?query2=KCMO', {timeout: 500000});
            },
            query3: function() {
                return $http.get('http://kc-sce-cs551-2.kc.umkc.edu/aspnet_client/qwer/RestService/EmailTrackingService.svc/getReadCountAll/');
            },
            query4: function() {
            	return $http.get('http://kc-sce-cs551-2.kc.umkc.edu/aspnet_client/qwer/RestService/EmailTrackingService.svc/topReaders');
            }
        }
    }
]);

