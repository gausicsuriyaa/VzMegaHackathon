<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailView.aspx.cs" Inherits="Vz.MegaHack.Web.DetailView" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/Scripts/Libs/jquery-1.10.2.min.js"></script>
    <script src="Content/Scripts/Libs/jquery.layout.js"></script>
    <script src="Content/Scripts/Libs/jquery-ui.min.js"></script>
    <script src="Content/Scripts/Libs/jquery-ui-slider-pips.js"></script>

    <script type="text/javascript">

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function goback() {
            window.location.href = "SupView.aspx?cid=" + getParameterByName('cid') + "&sid=" + getParameterByName('sid');
        }


        $(document).ready(function () {
            $('body').layout({ applyDefaultStyles: true });
            
            var centerid = getParameterByName('cid');


        });

        
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



        #upleft {
            width: 99%;
            height: 100%;
            float: left;
            /*border-style: solid;
            border-width: 1px;
            border-right: solid;
            border-right-color: gray;*/
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


       .ui-layout-resizer-east
       {
           right : 40% !important;
       }

       .table-header-rotated{
           border-collapse: collapse;
       }

       .csstransforms .table-header-rotated td{
           width: 30px;
       }

       .no-csstransforms .table-header-rotated th {
           padding: 5px 10px;
       }

       .table-header-rotated td{
           text-align: center;
           padding: 10px 5px;
           border: 1px solid #ccc;
       }

       .csstransforms .table-header-rotated th.rotate > div {
           -webkit-transform: translate(25px, 51px) rotate(315deg);
           -ms-transform: translate(25px, 51px) rotate(315deg);
           transform: translate(25px, 51px) rotate(315deg);
           width:30px;
       }

       .csstransforms .table-header-rotated th.rotate > div > span {
          border-bottom: 1px solid #ccc;
          padding: 5px 10px;
       }

       .table-header-rotated th.row-header{
            padding: 0 10px;
           border-bottom: 1px solid #ccc;
           border-left: 1px solid #ccc;
       }
    </style>
<link href="Content/Styles/jquery-ui-slider-pips.css" rel="stylesheet" />
<link href="Content/Styles/jquery-ui.min.css" rel="stylesheet" />
    <style>
        #rainbow-slider {
            background: linear-gradient(to right, #00FF00 0, #FFFFFF 50%, #ff0000 100%) no-repeat;
            background-size: cover;
            border-radius: 30px;
            border:none;
            box-shadow: inset 0 0 0 1px rgba(0,0,0,.18);
            height:10px;
        }

        #rainbow-slider.ui-slider-horizontal .ui-slider-range{
            background: transparent;
        }

        #rainbow-slider .ui-slider-handle{
            background: rgba(255,255,255,0.21);
            border-color: rgba(0, 0, 0, 0.56);
            box-shadow: inset 0 0 2px 2px rgba(255, 255, 255, 0.89);
            border-radius: 20px;
            top: -8px;
        }

        #rainbow-slider .ui-slider-handle.ui-state-hover,
        #rainbow-slider .ui-slider-handle:hover,
        #rainbow-slider .ui-slider-handle.ui-state-focus,
        #rainbow-slider .ui-slider-handle:focus,
        #rainbow-slider .ui-slider-handle.ui-state-active{
            background: rgba(255,255,255,0.21);
        }


        #rainbow-slider .ui-slider-pip .ui-slider-label{
            width: 6em;
            margin-left: -3em;
        }

        #rainbow-slider .ui-slider-pip .ui-slider-label {
            color: #000000;
        }

        #rainbow-slider .ui-slider-pip .ui-slider-line {
            top: 1px;
        }

        #rainbow-slider .ui-slider-pip:nth-of-type(odd){
            top: auto;
            bottom: 32px;
        }

        #rainbow-slider .ui-slider-pip:nth-of-type(odd) .ui-slider-line{
            top: 21px;
        }




    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div class="ui-layout-center leftpane">
        <div id="upleft">
            <div style="float:right">
                <a style="cursor:pointer; color: darkblue; text-decoration-line: underline" onclick="goback()">Back</a>
            </div>
            
        </div>

        
    </div>
    <div class="ui-layout-north topheader">Call Center Gamfication

        <div style="float:right; color: yellow" id="divSupName"></div>
        <div style="float:right">Supervisor Name: &nbsp;&nbsp; </div>
    </div>
    
    

    </form>
</body>
</html>