<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="echart_asset-liability.aspx.vb" Inherits="Jamada_4_0.echart_asset_liability" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>echart_asset-liability</title>
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

        .content {
            height: 100%;
            width: 50%;
            float: left;
            padding: 20px 0px;
            background-color: #2b3642 /*#ebe8dd*/;
        }
    </style>
</head>
<body>
    <%=errorshow1%><br />
    <%=errorshow2%>
    <div class="main">
        <div id="AssetLiabilityBar" class="content"></div>
        <div id="AssetPie" class="content"></div>
    </div>
    <script type="text/javascript">
        var AssetLiabilityBar = echarts.init(document.getElementById('AssetLiabilityBar'), 'dark');
        option_AssetLiabilityBar = {
            grid: {
                left: '8%',
                right: '8%',
            },
            title: {
                text: '资产负债率',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}月{a0}：{c0}万元<br/>{b1}月{a1}：{c1}%"
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
                data: ['资产', '资产负债率'],
                top: '8%'
            },
            xAxis: [
                {
                    type: 'category',
                    data: [<%=flDate%>]
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '资产（万元）',
                },
                {
                    type: 'value',
                    name: '资产负债率',
                    axisLabel: {
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '资产',
                    type: 'bar',
                    data: [<%=Bdata%>]
                },
                {
                    name: '资产负债率',
                    type: 'line',
                    yAxisIndex: 1,
                    data: [<%=Ldata%>],
                    markPoint: {
                        symbol: 'circle',
                        symbolSize: 25,
                        data: [<%=Lmark%>]
                    }
                }
            ]
        };

        AssetLiabilityBar.setOption(option_AssetLiabilityBar);
        window.addEventListener("resize", function () {

            AssetLiabilityBar.resize();

        });
    </script>
    <%--资产负债率柱折图--%>

    <script type="text/javascript">
        var myChart_AssetPie = echarts.init(document.getElementById('AssetPie'), 'dark');
        option_AssetPie = {
            title: {
                text: '资产构成',
                subtext:'企业当前资产构成情况',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c}万元 ({d}%)"
            },
            legend: {
                type: 'scroll',
                top: '15%',
                data: ['速动资产','非速动资产','非流动资产']
            },
            toolbox: {
                show: true,
                feature: {
                    mark: { show: true },
                    dataView: { show: true, readOnly: false },
                    restore: { show: true },
                    saveAsImage: { show: true }
                }
            },
            calculable: true,
            series: [
                {
                    name: '资产构成',
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '55%'],
                    data: [<%=PData%>],
                    label: {
                        normal: {//数值显示设置
                            formatter: '{b}:{d}%',
                            textStyle: {
                                fontWeight: 'normal',
                                fontSize: 15
                            }
                        }
                    },
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };

        myChart_AssetPie.setOption(option_AssetPie);
        window.addEventListener("resize", function () {

            myChart_AssetPie.resize();

        });
    </script>
    <%--资产构成饼图--%>
</body>
</html>
