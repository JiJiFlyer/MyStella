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
    <script src="../js/RPC.js"></script>    
    <script src="echars/codemirror.js"></script>
    <script src="echars/javascript.js"></script>
</head>
<body>
    <%=errorshow1%><br />
    <%=errorshow2%>
    <div id="fee6601" style="width: 800px; height: 600px;float:left;"></div>
    <div id="fee6602" style="width: 800px; height: 600px;float:left;"></div>
    <div id="fee6603" style="width: 800px; height: 600px;float:left;"></div>
    <div id="feeQJ" style="width: 800px; height: 600px;float:left;"></div>
    <div id="feeML" style="width: 800px; height: 600px;float:left;"></div>
    
    <script type="text/javascript">
        var echart_fee6601 = echarts.init(document.getElementById('fee6601'), 'macarons');
            option6601 = {
                title: {
                    text: '销售费用率',
                    subtext: '年度数据',
                },
                tooltip: {
                    trigger: 'item',
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                legend: {
                    data: ['费用', '费率']
                },
            xAxis: [
                {
                    type: 'category',
                    data: <%=flDate%>
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '费用',
                },
                {
                    type: 'value',
                    name: '费率',
                    axisLabel: {
                        formatter: '{value}'
                    }
                }
            ],
            series: [
                {
                    name: '费用',
                    type: 'bar',
                    data: <%=Bdata%>
                },
                {
                    name: '费率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=Ldata%>,
                    markPoint: { data:[<%=Lmark%>]}
                } 
            ]
        };

            echart_fee6601.setOption(option6601);
    </script>
</body>
</html>
