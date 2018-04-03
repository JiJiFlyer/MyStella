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
    <script type="text/javascript">
        var echart_fee6601 = echarts.init(document.getElementById('fee6601'), 'fee_tax');
        option6601 = {
            grid: {
                left: '15%',
                right: '15%',
                bottom: '10%'
            },
            title: {
                text: '销售费用率',
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
                    data: <%=flDate%>,
                }
                ],
                yAxis: [
                    {
                        type: 'value',
                        name: '费用（元）',
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
                        yAxisIndex: 1,
                        data: <%=Ldata6601%>,
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [<%=Lmark6601%>]
                        }
                    }
                ]
        };

        echart_fee6601.setOption(option6601);
        window.addEventListener("resize", function () {

            echart_fee6601.resize();

        });
    </script>
    <%--6601销售--%>

    <script type="text/javascript">
            var echart_fee6602 = echarts.init(document.getElementById('fee6602'), 'fee_tax');
            option6602 = {
                grid: {
                    left: '15%',
                    right: '15%',
                    bottom:'10%'
                },
                title: {
                    text: '管理费用率',
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
                        name: '费用（元）',
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
                        yAxisIndex: 1,
                        data: <%=Ldata6602%>,
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [<%=Lmark6602%>]
                        }
                    }
                ]
            };

            echart_fee6602.setOption(option6602);
            window.addEventListener("resize", function () {

                echart_fee6602.resize();

            });
    </script>
    <%--6602管理--%>

    <script type="text/javascript">
            var echart_fee6603 = echarts.init(document.getElementById('fee6603'), 'fee_tax');
            option6603 = {
                grid: {
                    left: '15%',
                    right: '15%',
                    bottom: '10%'
                },
                title: {
                    text: '财务费用率',
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
                        name: '费用（元）',
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
                        yAxisIndex: 1,
                        data: <%=Ldata6603%>,
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [<%=Lmark6603%>]
                        }
                    }
                ]
            };

            echart_fee6603.setOption(option6603);
            window.addEventListener("resize", function () {

                echart_fee6603.resize();

            });
    </script>
    <%--6603财务--%>

    <script type="text/javascript">
            var echart_feeQJ = echarts.init(document.getElementById('feeQJ'), 'fee_tax');
            optionQJ = {
                grid: {
                    left: '15%',
                    right: '15%',
                    bottom: '10%'
                },
                title: {
                    text: '期间费用率',
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
                        name: '费用（元）',
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
                        yAxisIndex: 1,
                        data: <%=LdataQJ%>,
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [<%=LmarkQJ%>]
                        }
                    }
                ]
            };

            echart_feeQJ.setOption(optionQJ);
            window.addEventListener("resize", function () {

                echart_feeQJ.resize();

            });
    </script>
    <%--QJ期间--%>

    <script type="text/javascript">
            var echart_feeML = echarts.init(document.getElementById('feeML'), 'fee_tax');
            optionML = {
                grid: {
                    left: '15%',
                    right: '15%',
                    bottom: '10%'
                },
                title: {
                    text: '毛利率',
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
                        name: '毛利（元）',
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
                        yAxisIndex: 1,
                        data: <%=LdataML%>,
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [<%=LmarkML%>]
                        }
                    }
                ]
            };

            echart_feeML.setOption(optionML);
            window.addEventListener("resize", function () {

                echart_feeML.resize();

            });
    </script>
    <%--ML毛利，6401无数据，待检验--%>

    <script type="text/javascript">
            var echart_pie_fee = echarts.init(document.getElementById('feePie'), 'fee_tax');
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
                        radius: '60%',
                        center: ['50%', '55%'],
                        data: [<%=feePiedata%>],
                        label: {
                            normal: {//数值显示设置
                                formatter: '{b}:{d}%',
                                textStyle: {
                                    fontWeight: 'normal',
                                    fontSize: 14
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

            echart_pie_fee.setOption(option_pie_fee);
            window.addEventListener("resize", function () {

                echart_pie_fee.resize();

            });
    </script>
    <%--年度费用占比情况--%>

    <script type="text/javascript">
            var echart_feeTotal = echarts.init(document.getElementById('feeTotal'), 'fee_tax');
            optionTotal = {
                grid: {
                    left: '15%',
                    right: '15%',
                    bottom: '10%'
                },
                title: {
                    text: '累计费用费率情况',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'axis',
                    formatter: "{b0}{a1}：{c1}元<br/>{b1}{a2}：{c2}%"
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
                    data: ['费用', '费率'],
                    top: '8%'
                },
                xAxis: [
                    {
                        type: 'category',
                        data: ['销售', '管理', '财务', '期间', '销售收入/毛利率']
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        name: '费用（元）',
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
                        //itemStyle: { normal: { label: { show: true, position: 'inside' } } },
                        data: [<%=BdataTotal%>]
                    },
                    {
                        name: '费率',
                        type: 'line',
                        yAxisIndex: 1,
                        data: [<%=LdataTotal%>],
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [<%=LmarkTotal%>]
                        }
                    }
                ]
            };
            echart_feeTotal.setOption(optionTotal);
            window.addEventListener("resize", function () {

                echart_feeTotal.resize();

            });
    </script>
    <%--Total累计--%>
</body>
</html>
