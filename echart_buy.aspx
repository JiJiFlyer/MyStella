<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="echart_buy.aspx.vb" Inherits="Jamada_4_0.echart_buy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>echart_buy</title>
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
        html,body {
            margin: 0;
            padding: 0 5px;
            height: 100%;
        }

        .main {
            background-color: black;
            height: 100%;
        }

        .m1,.m2,.m3,.m4{
            width: 25%;
            height: 100%;
            background-color: #2b3642/*#ebe8dd*/;
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
    <div class="main">
        <div class="m1">
            <div id="buybar1" class="content"></div>
            <div id="buybar3" class="content"></div>
        </div>
        <div class="m2">
            <div id="buybar2" class="content"></div>
            <div id="buybar4" class="content"></div>
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
    <script type="text/javascript">
        var buybar1 = echarts.init(document.getElementById('buybar1'), 'dark');
        option_buybar1 = {
            grid: {
                left: '15%',
                right: '15%',
            },
            title: {
                text: 'TOP1:<%=topgoods(1)%>',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}月{a0}：{c0}<%=unit(1)%><br/>{b1}月{a1}：{c1}元"
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
                data: ['数量', '价格'],
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
                    name: '数量（<%=unit(1)%>）',
                },
                {
                    type: 'value',
                    name: '价格（元）'
                }
            ],
            series: [
                {
                    name: '数量',
                    type: 'bar',
                    data: [<%=Bdata1%>]
                },
                {
                    name: '价格',
                    type: 'line',
                    yAxisIndex: 1,
                    data: [<%=Ldata1%>],
                    markPoint: {
                        symbol: 'circle',
                        symbolSize: 25,
                        data: [<%=Lmark1%>]
                    }
                }
            ]
        };

        buybar1.setOption(option_buybar1);
        window.addEventListener("resize", function () {

            buybar1.resize();

        });
    </script>
    <%--采购额第一产品量价图--%>

    <script type="text/javascript">
        var buybar2 = echarts.init(document.getElementById('buybar2'), 'dark');
        option_buybar2 = {
            grid: {
                left: '15%',
                right: '15%',
            },
            title: {
                text: 'TOP2:<%=topgoods(2)%>',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}月{a0}：{c0}<%=unit(2)%><br/>{b1}月{a1}：{c1}元"
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
                data: ['数量', '价格'],
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
                    name: '数量（<%=unit(2)%>）',
                },
                {
                    type: 'value',
                    name: '价格（元）'
                }
            ],
            series: [
                {
                    name: '数量',
                    type: 'bar',
                    data: [<%=Bdata2%>]
                },
                {
                    name: '价格',
                    type: 'line',
                    yAxisIndex: 1,
                    data: [<%=Ldata2%>],
                    markPoint: {
                        symbol: 'circle',
                        symbolSize: 25,
                        data: [<%=Lmark2%>]
                    }
                }
            ]
        };

        buybar2.setOption(option_buybar2);
        window.addEventListener("resize", function () {

            buybar2.resize();

        });
    </script>
    <%--采购额第二产品量价图--%>

    <script type="text/javascript">
        var buybar3 = echarts.init(document.getElementById('buybar3'), 'dark');
        option_buybar3 = {
            grid: {
                left: '15%',
                right: '15%',
            },
            title: {
                text: 'TOP3:<%=topgoods(3)%>',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}月{a0}：{c0}<%=unit(3)%><br/>{b1}月{a1}：{c1}元"
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
                data: ['数量', '价格'],
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
                        name: '数量（<%=unit(3)%>）',
                    },
                    {
                        type: 'value',
                        name: '价格（元）'
                    }
                ],
                series: [
                    {
                        name: '数量',
                        type: 'bar',
                        data: [<%=Bdata3%>]
                    },
                    {
                        name: '价格',
                        type: 'line',
                        yAxisIndex: 1,
                        data: [<%=Ldata3%>],
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [<%=Lmark3%>]
                        }
                    }
                ]
        };

        buybar3.setOption(option_buybar3);
        window.addEventListener("resize", function () {

            buybar3.resize();

        });
    </script>
    <%--采购额第三产品量价图--%>

    <script type="text/javascript">
        var buybar4 = echarts.init(document.getElementById('buybar4'), 'dark');
            option_buybar4 = {
                grid: {
                    left: '15%',
                    right: '15%',
                },
                title: {
                    text: 'TOP4:<%=topgoods(4)%>',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'axis',
                    formatter: "{b0}月{a0}：{c0}<%=unit(4)%><br/>{b1}月{a1}：{c1}元"
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
                    data: ['数量', '价格'],
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
                        name: '数量（<%=unit(4)%>）',
                    },
                    {
                        type: 'value',
                        name: '价格（元）'
                    }
                ],
                series: [
                    {
                        name: '数量',
                        type: 'bar',
                        data: [<%=Bdata4%>]
                    },
                    {
                        name: '价格',
                        type: 'line',
                        yAxisIndex: 1,
                        data: [<%=Ldata4%>],
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [<%=Lmark4%>]
                        }
                    }
                ]
            };

            buybar4.setOption(option_buybar4);
            window.addEventListener("resize", function () {

                buybar4.resize();

            });
    </script>
    <%--采购额第四产品量价图--%>

   
</body>
</html>
