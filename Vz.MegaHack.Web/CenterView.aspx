<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CenterView.aspx.cs" Inherits="Vz.MegaHack.Web.CenterView" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/Scripts/Libs/jquery-1.10.2.min.js"></script>
    <script src="Content/Scripts/Libs/jquery.layout.js"></script>
    <script src="Content/Scripts/Libs/highcharts.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('body').layout({ applyDefaultStyles: true });

            //codepedia.info/2015/07/highcharts-asp-net-create-pie-chart-with-jquery-ajax-in-c-sharp-ms-sql-database/
            //www.tutorialized.com/tutorial/Charts-and-graphs-using-jquery-and-charting-library-the-HighCharts/67976
            //jsfiddle.net/gh/get/jquery/1.7.2/highslide-software/highcharts.com/tree/master/samples/highcharts/plotoptions/series-point-events-click/
            //$('.rcorners').toggle(function () {
            //    $(".rcorners").addClass("active");
            //}, function () {
            //    $(".rcorners").removeClass("active");
            //});
            var centerid = 3
            $(".rcorners").click(function () {
                $('p').removeClass('active');
                $(this).addClass('active');
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Services/CCTService.asmx/GetSupervisorInfo",
                data: "{ intCenterID : '" + centerid + "'}",
                dataType: "json",
                success: function (Result) {
                    //alert(Result.d.UserName);
                    //alert(Result.d.UserValue);
                    //Result = Result.d;
                    var data = [];
                    for (var i in Result) {
                        var serie = new Array(Result[i].Name, Result[i].Value);
                        data.push(serie);
                    }
                    //DreawChart(data);
                    var seriesName = new Array();
                    var seriesScore = new Array();
                    var seriesTopKPI = new Array();
                    var seriesBottomKPI = new Array();

                    seriesName = Result.d.UserName;//.split(',');
                    seriesTopKPI = Result.d.TopKPIs;
                    seriesBottomKPI = Result.d.BottomKPIs;
                    //for (var i = 0; i < seriesOne.length; i++) {
                    //    seriesOne[i] = parseInt(seriesOne[i]);
                    //}

                    seriesScore = Result.d.UserScore;//.split(',');
                    DreawChart(seriesName, seriesScore, seriesTopKPI, seriesBottomKPI);
                },
                error: function (Result) {
                    alert("Error");
                }
            });

        });

        function DreawChart(uname, uvalue, utopkpi, ubottomkpi) {
            var arr = []
            $.map(uvalue, function (item, index) {
                arr.push(parseInt(item));
            });

            
            $('#container').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: 'Supervisor Details'
                },
                xAxis: {
                    categories: uname,
                    labels: {
                        style: {
                            cursor: 'pointer',
                            fontSize: '14px'
                        },
                    events: {
                            click: function(e)
                            {
                                alert('adfasdfasdf');
                                console.log(e.xAxis[0].value);
                                alert(e.xAxis[0].value);
                                //ajax post
                            }
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Points (in %)'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
               
                tooltip: {
                    shared: true,
                    formatter: function () {
                        var ind = uname.indexOf(this.x);
                        var s;
                        s = "<b>Name: </b>" + uname[ind] + "<br/><b>Score: </b>" + uvalue[ind] + "%<br/><b>Top KPIs: </b>" + utopkpi[ind] + "<br/><b>Bottom KPIs: </b>" + ubottomkpi[ind];
                        //$.each(this.points, function (i, point) {
                        //    //s += point.series.options.composition[ind];

                        //    s = "<b>Name: </b>" + point.x + "<br/><b>Score: </b>" + point.y + "%<br/><b>Top KPIs: </b>" + utopkpi[ind] + "<br/><b>Bottom KPIs: </b>" + ubottomkpi[ind];
                        //});
                        return s;
                    }
                    //headerFormat: '<b>{point.x}</b><br/>',
                    //pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                },
                plotOptions: {
                    column: {
                        stacking: 'percent',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                            style: {
                                textShadow: '0 0 3px black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Supervisors',
                    data: arr
                }

                //, {
                //    name: 'Jane',
                //    data: [2, 2, 3, 2, 1]
                //}, {
                //    name: 'Joe',
                //    data: [3, 4, 4, 2, 5]
                //}
                ]



            });
        }


        $('.highcharts-xaxis-labels text').on('click', function () {
            console.log($(this).text());
            alert($(this).text());
        });


        //$(function () {
        //    $('#container2').highcharts({
        //        chart: {
        //            type: 'column'
        //        },
        //        title: {
        //            text: 'Stacked column chart'
        //        },
        //        xAxis: {
        //            categories: ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
        //        },
        //        yAxis: {
        //            min: 0,
        //            title: {
        //                text: 'Total fruit consumption'
        //            },
        //            stackLabels: {
        //                enabled: true,
        //                style: {
        //                    fontWeight: 'bold',
        //                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
        //                }
        //            }
        //        },
        //        legend: {
        //            align: 'right',
        //            x: -30,
        //            verticalAlign: 'top',
        //            y: 25,
        //            floating: true,
        //            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
        //            borderColor: '#CCC',
        //            borderWidth: 1,
        //            shadow: false
        //        },
        //        tooltip: {
        //            headerFormat: '<b>{point.x}</b><br/>',
        //            pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
        //        },
        //        plotOptions: {
        //            column: {
        //                stacking: 'normal',
        //                dataLabels: {
        //                    enabled: true,
        //                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
        //                    style: {
        //                        textShadow: '0 0 3px black'
        //                    }
        //                }
        //            }
        //        },
        //        series: [{
        //            name: 'John',
        //            data: [5, 3, 4, 7, 2]
        //        }, {
        //            name: 'Jane',
        //            data: [2, 2, 3, 2, 1]
        //        }, {
        //            name: 'Joe',
        //            data: [3, 4, 4, 2, 5]
        //        }]
        //    });
        //});



        $(function () {
            $('#containerrrrr').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: 'Stacked column chart'
                },
                xAxis: {
                    categories: ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total fruit consumption'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false
                },
                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                            style: {
                                textShadow: '0 0 3px black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'John',
                    data: [5, 3, 4, 7, 2]
                }, {
                    name: 'Jane',
                    data: [2, 2, 3, 2, 1]
                }, {
                    name: 'Joe',
                    data: [3, 4, 4, 2, 5]
                }]
            });
        });



        //$(function () {
        //    // Create the chart
        //    $('#container3').highcharts({
        //        chart: {
        //            type: 'pie'
        //        },
        //        title: {
        //            text: 'Browser market shares. January, 2015 to May, 2015'
        //        },

        //        plotOptions: {
        //            series: {
        //                dataLabels: {
        //                    enabled: true,
        //                    format: '{point.name}: {point.y:.1f}%'
        //                }
        //            }
        //        },

        //        tooltip: {
        //            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
        //            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
        //        },
        //        series: [{
        //            name: "Brands",
        //            colorByPoint: true,
        //            data: [{
        //                name: "Microsoft Internet Explorer",
        //                y: 56.33,
        //                drilldown: "Microsoft Internet Explorer"
        //            }, {
        //                name: "Chrome",
        //                y: 24.03,
        //                drilldown: "Chrome"
        //            }, {
        //                name: "Firefox",
        //                y: 10.38,
        //                drilldown: "Firefox"
        //            }, {
        //                name: "Safari",
        //                y: 4.77,
        //                drilldown: "Safari"
        //            }, {
        //                name: "Opera",
        //                y: 0.91,
        //                drilldown: "Opera"
        //            }, {
        //                name: "Proprietary or Undetectable",
        //                y: 0.2,
        //                drilldown: null
        //            }]
        //        }],
        //        drilldown: {
        //            series: [{
        //                name: "Microsoft Internet Explorer",
        //                id: "Microsoft Internet Explorer",
        //                data: [
        //                    ["v11.0", 24.13],
        //                    ["v8.0", 17.2],
        //                    ["v9.0", 8.11],
        //                    ["v10.0", 5.33],
        //                    ["v6.0", 1.06],
        //                    ["v7.0", 0.5]
        //                ]
        //            }, {
        //                name: "Chrome",
        //                id: "Chrome",
        //                data: [
        //                    ["v40.0", 5],
        //                    ["v41.0", 4.32],
        //                    ["v42.0", 3.68],
        //                    ["v39.0", 2.96],
        //                    ["v36.0", 2.53],
        //                    ["v43.0", 1.45],
        //                    ["v31.0", 1.24],
        //                    ["v35.0", 0.85],
        //                    ["v38.0", 0.6],
        //                    ["v32.0", 0.55],
        //                    ["v37.0", 0.38],
        //                    ["v33.0", 0.19],
        //                    ["v34.0", 0.14],
        //                    ["v30.0", 0.14]
        //                ]
        //            }, {
        //                name: "Firefox",
        //                id: "Firefox",
        //                data: [
        //                    ["v35", 2.76],
        //                    ["v36", 2.32],
        //                    ["v37", 2.31],
        //                    ["v34", 1.27],
        //                    ["v38", 1.02],
        //                    ["v31", 0.33],
        //                    ["v33", 0.22],
        //                    ["v32", 0.15]
        //                ]
        //            }, {
        //                name: "Safari",
        //                id: "Safari",
        //                data: [
        //                    ["v8.0", 2.56],
        //                    ["v7.1", 0.77],
        //                    ["v5.1", 0.42],
        //                    ["v5.0", 0.3],
        //                    ["v6.1", 0.29],
        //                    ["v7.0", 0.26],
        //                    ["v6.2", 0.17]
        //                ]
        //            }, {
        //                name: "Opera",
        //                id: "Opera",
        //                data: [
        //                    ["v12.x", 0.34],
        //                    ["v28", 0.24],
        //                    ["v27", 0.17],
        //                    ["v29", 0.16]
        //                ]
        //            }]
        //        }
        //    });
        //});
    </script>
    <style type="text/css">
        .active {
            border-style: solid;
            border-width: 2px;
            border-color: red;
        }
        body
        {
            font-family: Arial;
        }
        .topheader {
            background-color: #3bbfaf !important;
            font-weight: bold;
            color: white;
        }

        .leftpane {
            background-color: #FFFFFF !important;
        }

        .rightpane {
            background-color: #363636 !important;
        }

        .rcorners {
            border-radius: 25px;
            background: #8AC007;
            padding: 20px;
            width: 118px;
            height: 110px;
        }


        #upleft {
            width: 59%;
            height: 100%;
            float: left;
            /*border-style: solid;*/
            border-width: 1px;
            border-right: solid;
            border-right-color: gray;
        }

        #upright {
            width: 40%;
            height: 100%;
            /*border-style: solid;
            border-width: 1px;
            border-color: gray;*/
            float: left;
        }

        #below {
            height: 290px;
            width: 99%;
            /*border-style: solid;*/
            border-width: 1px;
            border-top: solid;
            border-top-color: gray;
            clear: both;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div class="ui-layout-center leftpane">
        <div id="upleft">

            <div id="container" style="min-width: 310px; height: 600px; margin: 0 auto"></div>
        </div>

        <%--<div id="upright" style="padding-left: 10px">

            <table id="t1" style="width: 90%; border:1px solid;">
                <thead>
                    <th><b>Supervisor Name</b></th>
                    <th><b>Top KPI</b></th>
                    <th><b>Worst KPI</b></th>
                </thead>
                <tr>
                    <td>Supervisor 1</td>
                    <td>FCR</td>
                    <td>FCR 2</td>
                </tr>
                <tr>
                    <td>Supervisor 1</td>
                    <td>FCR</td>
                    <td>FCR 2</td>
                </tr>
                <tr>
                    <td>Supervisor 1</td>
                    <td>FCR</td>
                    <td>FCR 2</td>
                </tr>
                <tr>
                    <td>Supervisor 1</td>
                    <td>FCR</td>
                    <td>FCR 2</td>
                </tr>
            </table>
        </div>--%>
    </div>
    <div class="ui-layout-north topheader">Call Center Gamfication</div>
    
    <div class="ui-layout-east rightpane" style="background-color: #363636">
        <div style="background-color: #FF3333; color: white; margin-top:-25px; width:100%">
            <h2 style="vertical-align: middle">Leader Board</h2>
        </div>
        <p id="rcorners1" class="rcorners">Agent 1</p>
        <p id="rcorners1" class="rcorners">Agent 2</p>
        <p id="rcorners1" class="rcorners" style="background: yellow">Agent 3</p>
        <p id="rcorners1" class="rcorners">Agent 4</p>
        <p id="rcorners1" class="rcorners">Agent 5</p>
        <p id="rcorners1" class="rcorners">Agent 6</p>
        <p id="rcorners1" class="rcorners">Agent 7</p>
        <p id="rcorners1" class="rcorners">Agent 8</p>
        <p id="rcorners1" class="rcorners">Agent 9</p>
        <p id="rcorners1" class="rcorners">Agent 10</p>
    </div>

    </form>
</body>
</html>

