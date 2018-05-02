<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="echart_fee.aspx.vb" Inherits="Jamada_4_0.echart_fee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="ECharts">
    <title>echart_fee</title>
    <script src="js/common.js"></script>
    <script src="echars/echarts.js" charset='utf-8'></script>
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script src="echars/macarons.js"></script>
    <script src="echars/fee_tax.js"></script>
    <script src="echars/vintage.js"></script>
    <script src="../js/RPC.js"></script>
    <script src="echars/codemirror.js"></script>
    <script src="echars/javascript.js"></script>

    <style type="text/css"> 
        html,body {
            margin: 0;
            padding: 0 5px;
            height: 100%;
        }

        .main {
            background-color: black;
            height: 100%;
        }
        
        .main-left {
            width: 30%;
            height: 100%;
            background-color: #ebe8dd;
            float: left;
        }

        .main-center {
            width: 30%;
            height: 100%;
            background-color: #ebe8dd;
            float: left;
        }

        .main-right {
            width: 40%;
            background-color: #ebe8dd;
            height: 100%;
            float: left;
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
        <div class="main-left">
            <div id="fee6601" style="height: 33.33%; width: 100%"></div>
            <div id="fee6602" style="height: 33.33%; width: 100%"></div>
            <div id="fee6603" style="height: 33.33%; width: 100%"></div>
        </div>
        <div class="main-center">
            <div id="feeQJ" style="height: 50%; width: 100%"></div>
            <div id="feeML" style="height: 50%; width: 100%"></div>
        </div>
        <div class="main-right">
            <div id="feePie" style="height: 50%; width: 100%"></div>
            <div id="feeTotal" style="height: 50%; width: 100%"></div>
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
                url: 'echart_ashx/fee_select.ashx',
                dataType: "html",
                data: {
                    booksno: booksno
                },
                success: function (data) {
                    $("#script").html(data);

                },
                error: function (a, b, c) {
                    alert("出错了！请稍候再试！ 出错原因：" + a + b + c);
                }
            });
        }
    </script>

</body>
</html>
