<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupView.aspx.cs" Inherits="Vz.MegaHack.Web.SupView" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/Scripts/Libs/jquery-1.10.2.min.js"></script>
    <script src="Content/Scripts/Libs/jquery.layout.js"></script>
    <script src="Content/Scripts/Libs/highcharts.js"></script>
    <script src="Content/Scripts/Libs/customEvents.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script type="text/javascript">

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function goback()
        {
            window.location.href = "CenterView.aspx?cid=" + getParameterByName('cid');
        }

        function gotodetailview()
        {
            window.location.href = "DetailView.aspx?sid=" + getParameterByName('sid') + "&cid=" + getParameterByName('cid');
        }
       
        $(document).ready(function () {
            $('body').layout({ applyDefaultStyles: true });
            $('#divSupName').html('Tester');
            var centerid = getParameterByName('cid');
            var supid = getParameterByName('sid');

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Services/CCTService.asmx/GetAgentInfo",
                data: "{ strSupID : '" + supid + "'}",
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

                    $('#divSupName').html(Result.d.HeaderName);
                    //DreawChart(data);
                    var seriesName = new Array();
                    var seriesScore = new Array();
                    var seriesTopKPI = new Array();
                    var seriesBottomKPI = new Array();
                    var seriesAgentID = new Array();

                    seriesName = Result.d.UserName;//.split(',');
                    seriesTopKPI = Result.d.TopKPIs;
                    seriesBottomKPI = Result.d.BottomKPIs;
                    seriesAgentID = Result.d.AgentID;
                    //for (var i = 0; i < seriesOne.length; i++) {
                    //    seriesOne[i] = parseInt(seriesOne[i]);
                    //}

                    seriesScore = Result.d.UserScore;//.split(',');
                    DreawChart(seriesName, seriesScore, seriesTopKPI, seriesBottomKPI, seriesAgentID, centerid, supid);
                },
                error: function (Result) {
                    alert("Error");
                }
            });

        });

        function DreawChart(uname, uvalue, utopkpi, ubottomkpi, agentids, cid, sid) {
            var arr = []
            $.map(uvalue, function (item, index) {
                arr.push(parseInt(item));
            });


            $('#container').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Agent Details'
                },
                xAxis: {
                    type: 'category',
                    categories: uname,
                    labels: {
                        rotation: -45,
                        style: {
                            cursor: 'pointer',
                            fontSize: '14px'
                        },
                        events: {
                            click: function (e) {
                                window.location.href = "AgentView.aspx?cid=" + cid + "&aid=" + agentids[jQuery.inArray(this.value, uname)] + "&sid=" + sid;

                            }
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Points (in %)'
                    }
                    //stackLabels: {
                    //    enabled: true,
                    //    style: {
                    //        fontWeight: 'bold',
                    //        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                    //    }
                    //}
                },
                legend: {
                    enabled: false
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
                series: [{
                    name: 'Agents',
                    data: arr
                }

                //, {
                //    name: 'Jane',
                //    data: [2, 2, 3, 2, 1]
                //}, {
                //    name: 'Joe',
                //    data: [3, 4, 4, 2, 5]
                //}
                ],
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#FFFFFF',
                    align: 'right',
                    format: '{point.y:.1f}', // one decimal
                    y: 10, // 10 pixels down from the top
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }


            });
            //, function (chart) {

            //    var bottom = chart.plotHeight - 20;

            //    $.each(chart.series[0].data, function (i, data) {

            //        data.dataLabel.attr({
            //            y: bottom
            //        });
            //    });

            //});
        }


        $('.highcharts-xaxis-labels text').on('click', function () {
            console.log($(this).text());
            alert($(this).text());
        });



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
            background-color: #FF3333 !important;
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
            width: 99%;
            height: 100%;
            float: left;
            /*border-style: solid;
            border-width: 1px;
            border-right: solid;
            border-right-color: gray;*/
        }

        /*#upright {
            width: 40%;
            height: 100%;
            float: left;
        }*/

        #below {
            height: 290px;
            width: 99%;
            /*border-style: solid;*/
            border-width: 1px;
            border-top: solid;
            border-top-color: gray;
            clear: both;
        }

       .curvedimage {
            border-top-right-radius:50% 10%;
            border-top-left-radius:50% 10%;
            border-bottom-right-radius:50% 10%;
            border-bottom-left-radius:50% 10%;
            width: 130px;
            height:130px;
        }

       .ui-layout-resizer-east
       {
           right : 40% !important;
       }

    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div class="ui-layout-center leftpane">
        <div id="upleft">
            <div style="float:right">
                <a style="cursor:pointer; color: darkblue; text-decoration-line: underline" onclick="gotodetailview()">Detail View</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a style="cursor:pointer; color: darkblue; text-decoration-line: underline" onclick="goback()">Back</a>
            </div>
            <div id="container" style="min-width: 310px; height: 600px; margin: 0 auto"></div>
        </div>

        
    </div>
    <div class="ui-layout-north topheader">Call Center Gamfication

        <div style="float:right; color: yellow" id="divSupName"></div>
        <div style="float:right">Supervisor Name: &nbsp;&nbsp; </div>
    </div>
    
    

    </form>
</body>
</html>