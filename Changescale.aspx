<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Changescale.aspx.vb"  %>

<!DOCTYPE HTML>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="ECharts">
    <title>修改刻度</title>
	<script src="js/common.js"></script>
	<script src="js/recordSet.js"></script>
	<script src="js/RPC.js"></script>
	<script src="js/format.js"></script>
	<script src="js/check.js"></script>
	<script src="js/common.vbs"></script>
    <style type="text/css"> 
        #form1{
        width:90%;
        margin-left: auto; 
        margin-right: auto;
        border-collapse:collapse;
        padding:5px;
        }
        td{
        font-family:"Trebuchet MS", Arial, Helvetica, sans-serif;
        font-size:14px;
        font-weight:bold;
        text-align:center; 
        padding:2px;
        }
        th{
        font-family:"Trebuchet MS", Arial, Helvetica, sans-serif;
        font-size:16px;
        font-weight:bold;
        text-align:center; 
        }
        .drop{
            height:30px;
        }
        input{
            border: 1px solid #bbbbbb;
            border-radius: 2px;
            color: #000;
            height:25px;
            padding: 0 16px;
            transition: background 0.3s ease-in-out;
            width:100px;
        }
        :-ms-input-placeholder{
            color: #bbbbbb;
        }
        input:focus {
            outline: none;
            border-color: #9ecaed;
            box-shadow: 0 0 10px #9ecaed;
        }
        .btn{
            -webkit-appearance: none;
            background: #009dff;
            border: none;
            border-radius: 2px;
            color: #fff;
            cursor: pointer;
            height: 80px;
            font-family: 'Open Sans', sans-serif;
            font-size: 1.2em;
            letter-spacing: 0.05em;
            text-align: center;
            text-transform: uppercase;
            transition: background 0.3s ease-in-out;
        }
        .btn:hover {
            background: #00c8ff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr>
    <td>销售收入</td>
    <td><input type="text" id="salesMin" placeholder="最小值" value=""></td>
    <td><input type="text" id="salesMax" placeholder="最大值" value=""></td>
    <td><asp:dropdownlist id="salesUnit" class="drop" style="WIDTH: 100px" readOnle="false" runat="server"></asp:dropdownlist></td>
    <td rowspan="3"><button id="button" class="btn" onclick = "change()">确定</button></td>
    </tr>
    <tr>
    <td>银行存款</td>
    <td><input type="text" id="bankrollMin" placeholder="最小值"></td>
    <td><input type="text" id="bankrollMax" placeholder="最大值"></td>
    <td><asp:dropdownlist id="bankrollUnit" class="drop" style="WIDTH: 100px" readOnle="false" runat="server"></asp:dropdownlist></td>
    </tr>
    <tr>
    <td>应收票据</td>
    <td><input type="text" id="ticketMin"  placeholder="最小值"></td>
    <td><input type="text" id="ticketMax"  placeholder="最大值"></td>
    <td><asp:dropdownlist id="ticketUnit" class="drop" style="WIDTH: 100px" readOnle="false" runat="server"></asp:dropdownlist></td>
    </tr>
    </table>
    </form>
    
    <script type="text/javascript">
        function change() 
        {
            var fsalesmin = document.getElementById("salesMin").value;
            var fsalesmax = document.getElementById("salesmax").value;
            window.dialogArguments.Macro_RPC.runSql("update tickset set min = '" & fsalesmin & "' where autoinc='10FE9836-AE6B-47F1-AF19-05D1C1D1140C'")


        }
	</script>
</body>
</html>
