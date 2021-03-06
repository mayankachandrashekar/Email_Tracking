twitter311Module.controller('query4Ctrl', ['$scope', 'dataSvc',
    function ($scope, dataSvc) {
        var series = [];
        

        dataSvc.query4().success(function(data) {
            for (i = 0; i < data.length; i++) {
            	var temp=data[i].split("=");
            	var t=parseInt(temp[1]);
            	series.push([temp[0],t]);
                //series.push([data[i].word, data[i].num]);
            }

            $('#container').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 1,//null,
                    plotShadow: false
                },
                title: {
                    text: 'Top Readers'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Top Readers',
                    data: series
                }]
            });

        });
    }]);


















