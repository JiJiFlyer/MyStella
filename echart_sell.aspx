<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="echart_sell.aspx.vb" Inherits="Jamada_4_0.echart_sell" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>echart_sell</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="ECharts">
    <script src="js/common.js"></script>
    <script src="echars/echarts.js" charset='utf-8'></script>
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script src="echars/dark.js"></script>
    <script src="echars/fee_tax.js"></script>
    <script src="echars/vintage.js"></script>
    <script src="../js/RPC.js"></script>
    <script src="echars/codemirror.js"></script>
    <script src="echars/javascript.js"></script>
    <style type="text/css">
        html, body {
            margin: 0;
            padding: 0 5px;
            height: 100%;
        }

        .main {
            background-color: black;
            height: 100%;
        }

        .m1, .m2, .m3, .m4 {
            width: 25%;
            height: 100%;
            background-color: #2b3642 /*#ebe8dd*/;
            float: left;
        }

        .content {
            height: 50%;
            width: 100%;
        }
    </style>
</head>
<body>
    <%=errorshow1%><br />
    <%=errorshow2%>
    <div class="selectrow">
        公司切换：<select id="select">
            <%=SelectBook%>
        </select>
    </div>
    <div class="main">
        <div class="m1">
            <div id="sellbar1" class="content"></div>
            <div id="sellbar3" class="content"></div>
        </div>
        <div class="m2">
            <div id="sellbar2" class="content"></div>
            <div id="sellbar4" class="content"></div>
        </div>
        <div class="m3">
            <div id="piesalesman" class="content"></div>
            <div id="piedept" class="content"></div>
        </div>
        <div class="m4">
            <div id="pieproduct" class="content"></div>
            <div id="pieregion" class="content"></div>
        </div>
    </div>
    <div id="script"></div>
    <script type="text/javascript">
        $(function () {
            $('#select').on('change', function () {
                var val = $(this).val();
                _ajax(val);
            });
        });
        /*post value，再把value作为数组编号获得区间*/
        function _ajax(booksno) {
            $.ajax({
                type: 'post',
                url: 'echart_ashx/sell_select.ashx',
                dataType: "html",
                data: {
                    booksno: booksno
                },
                success: function (data) {
                    $("#script").html(data);
                    
                },
                error: function (a,b,c) {
                    alert("出错了！请稍候再试！ 出错原因："+ a+b+c);
                }
            });
        }
    </script>
    
</body>
</html>
