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
    <script src="echars/darks.js"></script>
    <script src="echars/vintage.js"></script>
    <script src="../js/RPC.js"></script>
    <script src="echars/codemirror.js"></script>
    <script src="echars/javascript.js"></script>

    <style type="text/css">
        .left {
            float: left;
            width: 700px;
        }

        .left-content {
            width: 100%;
            height: 450px;
            padding: 5px;
        }

        .right {
            margin-left: 710px;
        }

        .right-content {
            width: 100%;
            height: 600px;
            padding: 5px;
        }
    </style>
</head>
<body>
    <%=errorshow1%><br />
    <%=errorshow2%>
    <div class="header">Header</div>
    <div class="left">
        <div id="fee6601" class="left-content"></div>
        <div id="fee6602" class="left-content"></div>
        <div id="fee6603" class="left-content"></div>
        <div id="feeQJ" class="left-content"></div>
        <div id="feeML" class="left-content"></div>
    </div>
    <div class="right">
        <div id="feePie" class="right-content"></div>
        <div id="feeTotal" class="right-content"></div>
    </div>
    <div class="footer">Footer</div>
    <script type="text/javascript">
        var echart_fee6601 = echarts.init(document.getElementById('fee6601'), 'vintage');
        option6601 = {
            title: {
                text: '销售费用率',
                subtext: '年度数据',
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}销售费用：{c0}元<br/>{b1}销售费用率：{c1}%"
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
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '费用',
                    type: 'bar',
                    data: <%=Bdata6601%>
                },
                {
                    name: '费率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=Ldata6601%>,
                    markPoint: { data: [<%=Lmark6601%>] }
                }
            ]
        };

        echart_fee6601.setOption(option6601);
    </script>
    <%--6601销售--%>

    <script type="text/javascript">
        var echart_fee6602 = echarts.init(document.getElementById('fee6602'), 'vintage');
        option6602 = {
            title: {
                text: '管理费用率',
                subtext: '年度数据',
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}管理费用：{c0}元<br/>{b1}管理费用率：{c1}%"
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
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '费用',
                    type: 'bar',
                    data: <%=Bdata6602%>
                },
                {
                    name: '费率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=Ldata6602%>,
                    markPoint: { data: [<%=Lmark6602%>] }
                }
            ]
        };

        echart_fee6602.setOption(option6602);
    </script>
    <%--6602管理--%>

    <script type="text/javascript">
        var echart_fee6603 = echarts.init(document.getElementById('fee6603'), 'vintage');
        option6603 = {
            title: {
                text: '财务费用率',
                subtext: '年度数据',
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}财务费用：{c0}元<br/>{b1}财务费用率：{c1}%"
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
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '费用',
                    type: 'bar',
                    data: <%=Bdata6603%>
                },
                {
                    name: '费率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=Ldata6603%>,
                        markPoint: { data: [<%=Lmark6603%>] }
                }
            ]
        };

        echart_fee6603.setOption(option6603);
    </script>
    <%--6603财务--%>

    <script type="text/javascript">
        var echart_feeQJ = echarts.init(document.getElementById('feeQJ'), 'vintage');
        optionQJ = {
            title: {
                text: '期间费用率',
                subtext: '年度数据',
            },
            tooltip: {
                trigger: 'axis',
                formatter: "{b0}期间费用：{c0}元<br/>{b1}期间费用率：{c1}%"
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
                            formatter: '{value}%'
                        }
                    }
                ],
                series: [
                    {
                        name: '费用',
                        type: 'bar',
                        data: <%=BdataQJ%>
                },
                    {
                        name: '费率',
                        type: 'line',
                        lineStyle: { width: 20 },
                        symbolSize: 7,
                        yAxisIndex: 1,
                        data: <%=LdataQJ%>,
                    markPoint: { data: [<%=LmarkQJ%>] }
                    }
                ]
            };

            echart_feeQJ.setOption(optionQJ);
    </script>
    <%--QJ期间--%>

    <script type="text/javascript">
            var echart_feeML = echarts.init(document.getElementById('feeML'), 'vintage');
            optionML = {
                title: {
                    text: '毛利率',
                    subtext: '年度数据',
                },
                tooltip: {
                    trigger: 'axis',
                    formatter: "{b0}毛利：{c0}元<br/>{b1}毛利率：{c1}%"
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
                    data: ['毛利', '毛利率']
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
                    name: '毛利',
                },
                {
                    type: 'value',
                    name: '毛利率',
                    axisLabel: {
                        formatter: '{value}%'
                    }
                }
            ],
            series: [
                {
                    name: '毛利',
                    type: 'bar',
                    data: <%=BdataML%>
                },
                {
                    name: '毛利率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: <%=LdataML%>,
                        markPoint: { data: [<%=LmarkML%>] }
                }
            ]
        };

        echart_feeML.setOption(optionML);
    </script>
    <%--ML毛利，6401无数据，待检验--%>

    <script type="text/javascript">
        var echart_pie_fee = echarts.init(document.getElementById('feePie'), 'vintage');
        option_pie_fee = {
            title: {
                text: '年度费用占比情况',
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
                    name: '年度费用占比',
                    type: 'pie',
                    radius: '50%',
                    data: [<%=feePiedata%>],
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

            echart_pie_fee.setOption(option_pie_fee);
    </script>
    <%--年度费用占比情况--%>

    <script type="text/javascript">
            var echart_feeTotal = echarts.init(document.getElementById('feeTotal'), 'vintage');
            optionTotal = {
                title: {
                    text: '累计费用费率情况',
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
                    data: ['费用', '费率']
                },
                xAxis: [
                    {
                        type: 'category',
                        data: ['销售费用(率)', '管理费用(率)', '财务费用(率)', '期间费用(率)', '销售收入&毛利率']
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
                    name: '费用',
                    type: 'bar',
                    stack: '总量',
                    itemStyle: { normal: { label: { show: true, position: 'inside' } } },
                    data: [<%=BdataTotal%>]
                },
                {
                    name: '费率',
                    type: 'line',
                    lineStyle: { width: 20 },
                    symbolSize: 7,
                    yAxisIndex: 1,
                    data: [<%=LdataTotal%>],
                    markPoint: { data: [<%=LmarkTotal%>] }
                }
            ]
            };

            echart_feeTotal.setOption(optionTotal);
    </script>
    <%--Total累计--%>
</body>
</html>
