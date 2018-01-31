<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataSheet.aspx.vb" Inherits="Jamada_4_0.DataSheet" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>DataSheet</title>
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <style type="text/css">
        table {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            width: 90%;
            margin-left: auto;
            margin-right: auto;
            border-collapse: collapse;
        }

            table caption {
                font-weight: 700;
                font-size: 18px;
            }

            table td, table th {
                font-size: 1em;
                border: 1px solid #98bf21;
                padding: 3px 7px 2px 7px;
            }

            table th {
                font-size: 1.1em;
                text-align: left;
                padding-top: 5px;
                padding-bottom: 4px;
                background-color: #A7C942;
                color: #ffffff;
            }
    </style>
</head>
<body>
    <select id="select">
        <%=IntervalSelect %>
    </select>
    <table id="accountage_custom" border="1">
    </table>
    <script type="text/javascript">
        $(function () {
            $('#select').on('change', function () {
                var val = $(this).val();
                _ajax(val);
            });
        });
        /*post value，再把value作为数组编号获得区间*/
        function _ajax(num) {
            $.ajax({
                type: 'post',
                url: 'IntervalSelect.ashx',
                dataType: "html",
                data: {
                    num: num
                },
                success: function (data) {
                    $("#accountage_custom").html(data);
                },
                error: function () {
                    alert("出错了！请稍候再试！");
                }
            });
        }
    </script>
    <table id="accountage_remind" border="1">
        <caption>自定义账龄提醒表</caption>
        <tr>
            <th>客户</th>
            <th>余额方向</th>
            <th>余额</th>
            <%=AgeLabel_R%>
        </tr>
        <%=AccountAge_R%>
    </table>
    <table id="accountage" border="1">
        <caption>账龄表</caption>
        <tr>
            <th>客户</th>
            <th>余额方向</th>
            <th>余额</th>
            <%=AgeLabel%>
        </tr>
        <%=AccountAge%>
    </table>
</body>
</html>
