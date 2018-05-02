<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="echart_tax.aspx.vb" Inherits="Jamada_4_0.echart_tax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="ECharts">
    <title>echart_tax</title>
    <script src="js/common.js"></script>
    <script src="echars/echarts.js" charset='utf-8'></script>
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script src="echars/macarons.js"></script>
    <script src="echars/darks.js"></script>
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
    <div class="main">
        <div class="main-left">
            <div id="tax2221002" style="height: 50%; width: 100%"></div>
            <div id="tax2221005" style="height: 50%; width: 100%"></div>
        </div>
        <div class="main-center">
            <div id="taxDS" style="height: 50%; width: 100%"></div>
            <div id="taxQS" style="height: 50%; width: 100%"></div>
        </div>
        <div class="main-right">
            <div id="taxPie" style="height: 50%; width: 100%"></div>
            <div id="taxTotal" style="height: 50%; width: 100%"></div>
        </div>
    </div>
    <script type="text/javascript">
        var echart_tax2221002 = echarts.init(document.getElementById('tax2221002'), 'vintage');
        option2221002 = {
            title: {
                text: '增值税税负率',
                subtext: '年度数据',
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}增值税税额：{c0}元<br/>{b1}增值税税负率：{c1}%"
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
                data: ['税额', '税负率']
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
                    name: '税额',
                },
                {
                    type: 'value',
                    name: '税负率',
                    axisLabel: {
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '税额',
                    type: 'bar',
                    data: <%=Bdata2221002%>
                },
                {
                    name: '税负率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=Ldata2221002%>,
                    markPoint: { data: [<%=Lmark2221002%>] }
                }
            ]
        };

        echart_tax2221002.setOption(option2221002);
        window.addEventListener("resize", function () {

            echart_tax2221002.resize();

        });
    </script>
    <%--2221002增值税--%>

    <script type="text/javascript">
        var echart_tax2221005 = echarts.init(document.getElementById('tax2221005'), 'vintage');
        option2221005 = {
            title: {
                text: '所得税税负率',
                subtext: '年度数据',
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}所得税税额：{c0}元<br/>{b1}所得税税负率：{c1}%"
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
                data: ['税额', '税负率']
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
                    name: '税额',
                },
                {
                    type: 'value',
                    name: '税负率',
                    axisLabel: {
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '税额',
                    type: 'bar',
                    data: <%=Bdata2221005%>
                },
                {
                    name: '税负率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=Ldata2221005%>,
                    markPoint: { data: [<%=Lmark2221005%>] }
                }
            ]
        };

        echart_tax2221005.setOption(option2221005);
        window.addEventListener("resize", function () {

            echart_tax2221005.resize();

        });
    </script>
    <%--2221005所得税--%>

    <script type="text/javascript">
        var echart_taxDS = echarts.init(document.getElementById('taxDS'), 'vintage');
        optionDS = {
            title: {
                text: '其它地税税负率',
                subtext: '年度数据',
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}地税税额：{c0}元<br/>{b1}地税税负率：{c1}%"
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
                data: ['税额', '税负率']
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
                    name: '税额',
                },
                {
                    type: 'value',
                    name: '税负率',
                    axisLabel: {
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '税额',
                    type: 'bar',
                    data: <%=BdataDS%>
                },
                {
                    name: '税负率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=LdataDS%>,
                    markPoint: { data: [<%=LmarkDS%>] }
                }
            ]
        };

        echart_taxDS.setOption(optionDS);
        window.addEventListener("resize", function () {

            echart_taxDS.resize();

        });
    </script>
    <%--DS地税--%>

    <script type="text/javascript">
        var echart_taxQS = echarts.init(document.getElementById('taxQS'), 'vintage');
        optionQS = {
            title: {
                text: '全税税负率',
                subtext: '年度数据',
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}全税税额：{c0}元<br/>{b1}全税税负率：{c1}%"
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
                data: ['税额', '税负率']
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
                    name: '税额',
                },
                {
                    type: 'value',
                    name: '税负率',
                    axisLabel: {
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '税额',
                    type: 'bar',
                    data: <%=BdataQS%>
                },
                {
                    name: '税负率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=LdataQS%>,
                        markPoint: { data: [<%=LmarkQS%>] }
                }
            ]
        };

        echart_taxQS.setOption(optionQS);
        window.addEventListener("resize", function () {

            echart_taxQS.resize();

        });
    </script>
    <%--QS全税--%>

    <script type="text/javascript">
        var echart_pie_tax = echarts.init(document.getElementById('taxPie'), 'vintage');
        option_pie_tax = {
            title: {
                text: '年度税额占比情况',
                subtext: '1月至现在的累计额',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{b} : {c}元 ({d}%)"
            },
            toolbox: {
                show: true,
                top: '15%',
                right: '1%',
                left: '79%',
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
                    name: '年度税额占比',
                    type: 'pie',
                    radius: '50%',
                    data: [<%=taxPiedata%>],
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

            echart_pie_tax.setOption(option_pie_tax);
            window.addEventListener("resize", function () {

                echart_pie_tax.resize();

            });
    </script>
    <%--年度税额占比情况--%>

    <script type="text/javascript">
            var echart_taxTotal = echarts.init(document.getElementById('taxTotal'), 'vintage');
            optionTotal = {
                title: {
                    text: '累计税额及税负率情况',
                    subtext: '年度数据',
                },
                tooltip: {
                    trigger: 'axis',
                    formatter: "{b0}：{c1}元<br/>{b1}：{c2}%"
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
                    data: ['税额', '税负率']
                },
                xAxis: [
                    {
                        type: 'category',
                        data: ['增值税(率)', '所得税(率)', '地税(率)', '全税(率)']
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        name: '税额',
                    },
                    {
                        type: 'value',
                        name: '税负率',
                        axisLabel: {
                            formatter: '{value}%'
                        }
                    }
                ],
                series: [
                    {
                        name: '辅助',
                        type: 'bar',
                        stack: '总量',
                        itemStyle: {
                            normal: {
                                barBorderColor: 'rgba(0,0,0,0)',
                                color: 'rgba(0,0,0,0)'
                            },
                            emphasis: {
                                barBorderColor: 'rgba(0,0,0,0)',
                                color: 'rgba(0,0,0,0)'
                            }
                        },
                        data: [<%=BdataHelp%>]
                    },
                    {
                        name: '税额',
                        type: 'bar',
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'inside' } } },
                        data: [<%=BdataTotal%>]
                    },
                    {
                        name: '税负率',
                        type: 'line',
                        lineStyle: { width: 20 },
                        symbolSize: 7,
                        yAxisIndex: 1,
                        data: [<%=LdataTotal%>],
                        markPoint: { data: [<%=LmarkTotal%>] }
                    }
                ]
            };

            echart_taxTotal.setOption(optionTotal);
            window.addEventListener("resize", function () {

                echart_taxTotal.resize();

            });
    </script>
    <%--Total累计--%>
</body>
</html>
