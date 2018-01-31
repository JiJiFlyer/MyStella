<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataSheet.aspx.vb" Inherits="Jamada_4_0.DataSheet" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>DataSheet</title>
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
