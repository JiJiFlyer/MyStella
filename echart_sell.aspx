<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="echart_sell.aspx.vb" Inherits="Jamada_4_0.echart_sell" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>echart_sell</title>
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
            <div id="sellbar1" class="content"></div>
            <div id="sellbar3" class="content"></div>
        </div>
        <div class="m2">
            <div id="sellbar2" class="content"></div>
            <div id="sellbar4" class="content"></div>
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
        var sellbar1 = echarts.init(document.getElementById('sellbar1'), 'dark');
        option_sellbar1 = {
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
                formatter: "{b0}月{a0}：{c0}个<br/>{b1}月{a1}：{c1}元"
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
                    name: '数量（个）',
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
                    markPoint: { data: [<%=Lmark1%>] }
                }
            ]
        };

        sellbar1.setOption(option_sellbar1);
        window.addEventListener("resize", function () {

            sellbar1.resize();

        });
    </script>
    <%--销售额第一产品量价图--%>

    <script type="text/javascript">
        var sellbar2 = echarts.init(document.getElementById('sellbar2'), 'dark');
        option_sellbar2 = {
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
                formatter: "{b0}月{a0}：{c0}个<br/>{b1}月{a1}：{c1}元"
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
                    name: '数量（个）',
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
                        markPoint: { data: [<%=Lmark2%>] }
                }
            ]
        };

        sellbar2.setOption(option_sellbar2);
        window.addEventListener("resize", function () {

            sellbar2.resize();

        });
    </script>
    <%--销售额第二产品量价图--%>

    <script type="text/javascript">
        var sellbar3 = echarts.init(document.getElementById('sellbar3'), 'dark');
        option_sellbar3 = {
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
                formatter: "{b0}月{a0}：{c0}个<br/>{b1}月{a1}：{c1}元"
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
                        name: '数量（个）',
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
                        markPoint: { data: [<%=Lmark3%>] }
                    }
                ]
        };

        sellbar3.setOption(option_sellbar3);
        window.addEventListener("resize", function () {

            sellbar3.resize();

        });
    </script>
    <%--销售额第三产品量价图--%>

    <script type="text/javascript">
        var sellbar4 = echarts.init(document.getElementById('sellbar4'), 'dark');
            option_sellbar4 = {
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
                    formatter: "{b0}月{a0}：{c0}个<br/>{b1}月{a1}：{c1}元"
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
                        name: '数量（个）',
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
                        markPoint: { data: [<%=Lmark4%>] }
                    }
                ]
            };

            sellbar4.setOption(option_sellbar4);
            window.addEventListener("resize", function () {

                sellbar4.resize();

            });
    </script>
    <%--销售额第四产品量价图--%>

    <script type="text/javascript">
            var myChart_pie_salesman = echarts.init(document.getElementById('piesalesman'), 'dark');
            option_pie_salesman = {
                title: {
                    text: '业务员销售业绩',
                    subtext: '年度前十（年初至今累计）',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}元 ({d}%)"
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
                        name: '业务员销售额',
                        type: 'pie',
                        radius: '40%',
                        center: ['50%', '55%'],
                        data: [<%=PsalesmanData%>],
                        label: {
                            normal: {//数值显示设置
                                formatter: '{b}:{d}%',
                                textStyle: {
                                    fontWeight: 'normal',
                                    fontSize: 13
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

            myChart_pie_salesman.setOption(option_pie_salesman);
            window.addEventListener("resize", function () {

                myChart_pie_salesman.resize();

            });
    </script>
    <%--业务员饼图--%>

    <script type="text/javascript">
            var myChart_pie_dept = echarts.init(document.getElementById('piedept'), 'dark');
            option_pie_dept = {
                title: {
                    text: '部门销售业绩',
                    subtext: '年度前十（年初至今累计）',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}元 ({d}%)"
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
                        name: '部门销售额',
                        type: 'pie',
                        radius: '40%',
                        center: ['50%', '55%'],
                        data: [<%=PdeptData%>],
                        label: {
                            normal: {//数值显示设置
                                formatter: '{b}:{d}%',
                                textStyle: {
                                    fontWeight: 'normal',
                                    fontSize: 13
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

            myChart_pie_dept.setOption(option_pie_dept);
            window.addEventListener("resize", function () {

                myChart_pie_dept.resize();

            });
    </script>
    <%--部门饼图--%>

    <script type="text/javascript">
            var myChart_pie_product = echarts.init(document.getElementById('pieproduct'), 'dark');
            option_pie_product = {
                title: {
                    text: '产品销售业绩',
                    subtext: '年度前十（年初至今累计）',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}元 ({d}%)"
                },
                legend: {
                    type: 'scroll',
                    top: '15%',
                    data:[<%=Pproductlegend%>],
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
                        name: '产品销售额',
                        type: 'pie',
                        radius: '40%',
                        center: ['50%', '55%'],
                        data: [<%=PproductData%>],
                        label: {
                            normal: {//数值显示设置
                                formatter: '{d}%',
                                textStyle: {
                                    fontWeight: 'normal',
                                    fontSize: 13
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

            myChart_pie_product.setOption(option_pie_product);
            window.addEventListener("resize", function () {

                myChart_pie_product.resize();

            });
    </script>
    <%--产品饼图--%>

    <script type="text/javascript">
            var myChart_pie_region = echarts.init(document.getElementById('pieregion'), 'dark');
            option_pie_region = {
                title: {
                    text: '地区销售业绩',
                    subtext: '年度前十（年初至今累计）',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}元 ({d}%)"
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
                        name: '地区销售额',
                        type: 'pie',
                        radius: '40%',
                        center: ['50%', '55%'],
                        data: [<%=PregionData%>],
                        label: {
                            normal: {//数值显示设置
                                formatter: '{b}:{d}%',
                                textStyle: {
                                    fontWeight: 'normal',
                                    fontSize: 12
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

            myChart_pie_region.setOption(option_pie_region);
            window.addEventListener("resize", function () {

                myChart_pie_region.resize();

            });
    </script>
    <%--地区饼图--%>
</body>
</html>
