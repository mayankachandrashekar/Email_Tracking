twitter311Module.controller('twitter311Ctrl', ['$scope',
    function ($scope) {
        $scope.templates =
            [
                { name: 'query1', title: 'Email', subtitle: 'Broad casting', url: 'html/query0.html'},
                { name: 'query2', title: 'Email Analysis', subtitle: 'by Sender Email Address', url: 'html/query1.html'},
                { name: 'query3', title: 'Email Analysis', subtitle: 'by Email', url: 'html/query3.html'},
                { name: 'query4', title: 'Top Readers', subtitle: ' ', url: 'html/query4.html'},
               
              ];
        $scope.template = $scope.templates[0];

        $scope.click = function(name) {
            for(var i = 0; i < $scope.templates.length; i++) {
                if ($scope.templates[i].name == name) {
                    $scope.template = $scope.templates[i];
                    break;
                }
            }
        }

        $scope.isActive = function(name) {
            return name === $scope.template.name;
        }
    }
]);
