twitter311Module.controller('query1Ctrl', ['$scope', 'dataSvc',
    function ($scope, dataSvc) {
        var series = [];

        dataSvc.query1().success(function(data) {
        	
            for (i = 0; i < data.length; i++) {
            	var temp=data[i].split("=");
            	var t=parseInt(temp[1]);
                series.push({"data":[t], "name": temp[0]});
            }

            $('#container').highcharts({
                chart: {
                    type: 'bar',
                    type: 'column'
                },
                title: {
                    text: 'Read Count - Email Based'
                },
                subtitle: {
                    text: ' '
                },
                xAxis: {
                    title: {
                        text: 'Email Subjects'
                    }
                },
                yAxis: {

                    title: {
                        text: 'Read Counts'
                    },
                    labels: {
                        overflow: 'justify'
                    }
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                series: series
            });
        });
    }
]);