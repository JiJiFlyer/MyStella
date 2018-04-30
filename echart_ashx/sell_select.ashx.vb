Imports System.Web
Imports System.Web.Services
Imports System.Data
Imports System.Text

Public Class sell_select
    Implements System.Web.IHttpHandler
    Public topgoods(10) As String
    Public unit(5) As String
    Public flDate As New StringBuilder()
    Public Bdata1 As New StringBuilder()
    Public Ldata1 As New StringBuilder()
    Public Lmark1 As New StringBuilder()
    Public Bdata2 As New StringBuilder()
    Public Ldata2 As New StringBuilder()
    Public Lmark2 As New StringBuilder()
    Public Bdata3 As New StringBuilder()
    Public Ldata3 As New StringBuilder()
    Public Lmark3 As New StringBuilder()
    Public Bdata4 As New StringBuilder()
    Public Ldata4 As New StringBuilder()
    Public Lmark4 As New StringBuilder()
    Public PsalesmanData As New StringBuilder()
    Public PdeptData As New StringBuilder()
    Public PproductData As New StringBuilder()
    Public Pproductlegend As New StringBuilder()
    Public PregionData As New StringBuilder()


    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "text/plain"
        Dim bookno As String
        bookno = context.Request.Form("booksno")

        Dim db As DB
        Dim drDB As SqlClient.SqlDataReader
        Dim sSql As String
        db = New DB

        Try
            '获取当前月份
            Dim i As Integer
            Dim month As Integer = Format(Now(), "MM")
            For i = 1 To month
                flDate.Append("" & i.ToString() & ",")
            Next i

            Dim a As Integer = 1
            '先得出本年度至今销量额最高的四个产品
            sSql = "select goodsname,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) 
                    and bookno = " & bookno & "
                    group by goodsname order by tprice desc"
            drDB = db.GetDataReader(sSql)
            While drDB.Read()
                If Not IsDBNull(drDB.Item("goodsname")) Then '去除产品名为空的产品
                    topgoods(a) = drDB.Item("goodsname")
                    a = a + 1
                End If
            End While
            drDB.Close()

            '获取前四产品的计量单位
            For i = 1 To 4
                sSql = "select distinct goodsunit from echart_sell where goodsname = '" & topgoods(i) & "' and bookno = " & bookno & ""
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                unit(i) = drDB.Item("goodsunit")
                drDB.Close()
            Next i

            Bdata1.Append("[")
            Ldata1.Append("[")
            Lmark1.Append("[")
            Bdata2.Append("[")
            Ldata2.Append("[")
            Lmark2.Append("[")
            Bdata3.Append("[")
            Ldata3.Append("[")
            Lmark3.Append("[")
            Bdata4.Append("[")
            Ldata4.Append("[")
            Lmark4.Append("[")
            For i = 1 To month
                '销售额第一产品量价图数据填入
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_sell 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                        and goodsname = '" & topgoods(1) & "' and bookno = " & bookno & ""
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                Bdata1.Append("" & drDB.Item("amount") & ",")
                If Not IsDBNull(drDB.Item("avgprice")) Then
                    Ldata1.Append("" & Format(drDB.Item("avgprice"), "#.##") & ",")
                    Lmark1.Append("{value: " & Format(drDB.Item("avgprice"), "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(drDB.Item("avgprice"), "#.##") & "},")
                Else
                    Ldata1.Append(",")
                End If
                drDB.Close()

                '销售额第二产品量价图数据填入
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_sell 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                        and goodsname = '" & topgoods(2) & "' and bookno = " & bookno & ""
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                Bdata2.Append("" & drDB.Item("amount") & ",")
                If Not IsDBNull(drDB.Item("avgprice")) Then
                    Ldata2.Append("" & Format(drDB.Item("avgprice"), "#.##") & ",")
                    Lmark2.Append("{value: " & Format(drDB.Item("avgprice"), "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(drDB.Item("avgprice"), "#.##") & "},")
                Else
                    Ldata2.Append(",")
                End If
                drDB.Close()

                '销售额第三产品量价图数据填入
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_sell 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                        and goodsname = '" & topgoods(3) & "' and bookno = " & bookno & ""
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                Bdata3.Append("" & drDB.Item("amount") & ",")
                If Not IsDBNull(drDB.Item("avgprice")) Then
                    Ldata3.Append("" & Format(drDB.Item("avgprice"), "#.##") & ",")
                    Lmark3.Append("{value: " & Format(drDB.Item("avgprice"), "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(drDB.Item("avgprice"), "#.##") & "},")
                Else
                    Ldata3.Append(",")
                End If
                drDB.Close()

                '销售额第四产品量价图数据填入
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_sell 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                        and goodsname = '" & topgoods(4) & "' and bookno = " & bookno & ""
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                Bdata4.Append("" & drDB.Item("amount") & ",")
                If Not IsDBNull(drDB.Item("avgprice")) Then
                    Ldata4.Append("" & Format(drDB.Item("avgprice"), "#.##") & ",")
                    Lmark4.Append("{value: " & Format(drDB.Item("avgprice"), "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(drDB.Item("avgprice"), "#.##") & "},")
                Else
                    Ldata4.Append(",")
                End If
                drDB.Close()

            Next i

            Bdata1.Append("]")
            Ldata1.Append("]")
            Lmark1.Append("]")
            Bdata2.Append("]")
            Ldata2.Append("]")
            Lmark2.Append("]")
            Bdata3.Append("]")
            Ldata3.Append("]")
            Lmark3.Append("]")
            Bdata4.Append("]")
            Ldata4.Append("]")
            Lmark4.Append("]")

            '今年业务员业绩前十填入
            sSql = "select top 10 salesmanname,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & " 
                    group by salesmanname order by tprice desc"
            drDB = db.GetDataReader(sSql)
            PsalesmanData.Append("[")
            While drDB.Read
                If drDB.Item("salesmanname").ToString = "" Then
                    Continue While
                End If '无视空名数据
                PsalesmanData.Append("{")
                PsalesmanData.Append("""value"":")
                PsalesmanData.Append("""" & Format(drDB.Item("tprice"), "#.##") & """,")
                PsalesmanData.Append("""name"":")
                PsalesmanData.Append("""" & drDB.Item("salesmanname").ToString() & """")
                PsalesmanData.Append("},")
            End While
            PsalesmanData.Append("]")
            drDB.Close()

            '今年部门业绩前十填入
            sSql = "select top 10 dept,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & "
                    group by dept order by tprice desc"
            drDB = db.GetDataReader(sSql)
            PdeptData.Append("[")
            While drDB.Read
                If drDB.Item("dept").ToString = "" Then
                    Continue While
                End If '无视空名数据
                PdeptData.Append("{")
                PdeptData.Append("""value"":")
                PdeptData.Append("""" & Format(drDB.Item("tprice"), "#.##") & """,")
                PdeptData.Append("""name"":")
                PdeptData.Append("""" & drDB.Item("dept").ToString() & """")
                PdeptData.Append("},")
            End While
            PdeptData.Append("]")
            drDB.Close()

            '今年产品销售额前十填入
            sSql = "select top 10 goodsname,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & " 
                    group by goodsname order by tprice desc"
            drDB = db.GetDataReader(sSql)
            PproductData.Append("[")
            Pproductlegend.Append("[")
            While drDB.Read
                If drDB.Item("goodsname").ToString = "" Then
                    Continue While
                End If '无视空名数据
                Pproductlegend.Append("""" & drDB.Item("goodsname").ToString() & """,")
                PproductData.Append("{")
                PproductData.Append("""value"":")
                PproductData.Append("""" & Format(drDB.Item("tprice"), "#.##") & """,")
                PproductData.Append("""name"":")
                PproductData.Append("""" & drDB.Item("goodsname").ToString() & """")
                PproductData.Append("},")
            End While
            Pproductlegend.Append("]")
            PproductData.Append("]")
            drDB.Close()

            '今年地区销售额前十填入
            sSql = "select top 10 region,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & "
                    group by region order by tprice desc"
            drDB = db.GetDataReader(sSql)
            PregionData.Append("[")
            While drDB.Read
                If drDB.Item("region").ToString = "" Then
                    Continue While
                End If '无视空名数据
                PregionData.Append("{")
                PregionData.Append("""value"":")
                PregionData.Append("""" & Format(drDB.Item("tprice"), "#.##") & """,")
                PregionData.Append("""name"":")
                PregionData.Append("""" & drDB.Item("region").ToString() & """")
                PregionData.Append("},")
            End While
            PregionData.Append("]")
            drDB.Close()
            Dim json As New StringBuilder
            json.Append("
<script type=""text/javascript"">
        var sellbar1 = echarts.init(document.getElementById('sellbar1'), 'dark');
        option_sellbar1 = {
            grid: {
                left: '15%',
                right: '15%',
            },
            title: {
                text: 'TOP1:")
            json.Append("" & topgoods(1) & "',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis',
                formatter: ""{b0}月{a0}：{c0}" & unit(1) & "<br/>{b1}月{a1}：{c1}元""
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
                    data: [")
            json.Append(flDate)
            json.Append("]
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '数量（" & unit(1) & "）',
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
                    data: ")
            json.Append(Bdata1)
            json.Append("
                },
                {
                    name: '价格',
                    type: 'line',
                    yAxisIndex: 1,
                    data: ")
            json.Append(Ldata1)
            json.Append(",
                    markPoint: {
                        symbol: 'circle',
                        symbolSize: 25,
                        data: ")
            json.Append(Lmark1)
            json.Append("
                    }
                }
            ]
        };

        sellbar1.setOption(option_sellbar1);
        window.addEventListener(""resize"", function () {

            sellbar1.resize();

        });
    </script>

    <script type=""text/javascript"">
        var sellbar2 = echarts.init(document.getElementById('sellbar2'), 'dark');
        option_sellbar2 = {
            grid: {
                left: '15%',
                right: '15%',
            },
            title: {
                text: 'TOP2:" & topgoods(2) & "',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis',
                formatter: ""{b0}月{a0}：{c0}" & unit(2) & "<br/>{b1}月{a1}：{c1}元""
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
                    data: [")
            json.Append(flDate)
            json.Append("]
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '数量（" & unit(2) & "）',
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
                    data: ")
            json.Append(Bdata2)
            json.Append("
                },
                {
                    name: '价格',
                    type: 'line',
                    yAxisIndex: 1,
                    data: ")
            json.Append(Ldata2)
            json.Append(",
                    markPoint: {
                        symbol: 'circle',
                        symbolSize: 25,
                        data: ")
            json.Append(Lmark2)
            json.Append("
                    }
                }
            ]
        };

        sellbar2.setOption(option_sellbar2);
        window.addEventListener(""resize"", function () {

            sellbar2.resize();

        });
    </script>

    <script type=""text/javascript"">
        var sellbar3 = echarts.init(document.getElementById('sellbar3'), 'dark');
        option_sellbar3 = {
            grid: {
                left: '15%',
                right: '15%',
            },
            title: {
                text: 'TOP3:" & topgoods(3) & "',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis',
                formatter: ""{b0}月{a0}：{c0}" & unit(3) & "<br/>{b1}月{a1}：{c1}元""
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
                    data: [")
            json.Append(flDate)
            json.Append("]
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '数量（" & unit(3) & "）',
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
                    data: ")
            json.Append(Bdata3)
            json.Append("
                },
                {
                    name: '价格',
                    type: 'line',
                    yAxisIndex: 1,
                    data: ")
            json.Append(Ldata3)
            json.Append(",
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: ")
            json.Append(Lmark3)
            json.Append("
                        }
                }
            ]
        };

        sellbar3.setOption(option_sellbar3);
        window.addEventListener(""resize"", function () {

            sellbar3.resize();

        });
    </script>

    <script type=""text/javascript"">
        var sellbar4 = echarts.init(document.getElementById('sellbar4'), 'dark');
        option_sellbar4 = {
            grid: {
                left: '15%',
                right: '15%',
            },
            title: {
                text: 'TOP4:" & topgoods(4) & "',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis',
                formatter: ""{b0}月{a0}：{c0}" & unit(4) & "<br/>{b1}月{a1}：{c1}元""
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
                    data: [")
            json.Append(flDate)
            json.Append("]
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        name: '数量（" & unit(4) & "）',
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
                        data: ")
            json.Append(Bdata4)
            json.Append("
                    },
                    {
                        name: '价格',
                        type: 'line',
                        yAxisIndex: 1,
                        data: ")
            json.Append(Ldata4)
            json.Append(",
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: ")
            json.Append(Lmark4)
            json.Append("
                        }
                    }
                ]
        };

        sellbar4.setOption(option_sellbar4);
        window.addEventListener(""resize"", function () {

            sellbar4.resize();

        });
    </script>

    <script type=""text/javascript"">
        var myChart_pie_salesman = echarts.init(document.getElementById('piesalesman'), 'dark');
        option_pie_salesman = {
            title: {
                text: '业务员销售业绩',
                subtext: '年度前十（年初至今累计）',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: ""{a} <br/>{b} : {c}元 ({d}%)""
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
                    data: ")
            json.Append(PsalesmanData)
            json.Append(",
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
            window.addEventListener(""resize"", function () {

                myChart_pie_salesman.resize();

            });
    </script>

    <script type=""text/javascript"">
            var myChart_pie_dept = echarts.init(document.getElementById('piedept'), 'dark');
            option_pie_dept = {
                title: {
                    text: '部门销售业绩',
                    subtext: '年度前十（年初至今累计）',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: ""{a} <br/>{b} : {c}元 ({d}%)""
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
                        data: ")
            json.Append(PdeptData)
            json.Append(",
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
            window.addEventListener(""resize"", function () {

                myChart_pie_dept.resize();

            });
    </script>

    <script type=""text/javascript"">
            var myChart_pie_product = echarts.init(document.getElementById('pieproduct'), 'dark');
            option_pie_product = {
                title: {
                    text: '产品销售业绩',
                    subtext: '年度前十（年初至今累计）',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: ""{a} <br/>{b} : {c}元 ({d}%)""
                },
                legend: {
                    type: 'scroll',
                    top: '15%',
                    data: ")
            json.Append(Pproductlegend)
            json.Append(", //名称过长，添加标签加以区分
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
                        data: ")
            json.Append(PproductData)
            json.Append(",
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
            window.addEventListener(""resize"", function () {

                myChart_pie_product.resize();

            });
    </script>

    <script type=""text/javascript"">
            var myChart_pie_region = echarts.init(document.getElementById('pieregion'), 'dark');
            option_pie_region = {
                title: {
                    text: '地区销售业绩',
                    subtext: '年度前十（年初至今累计）',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: ""{a} <br/>{b} : {c}元 ({d}%)""
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
                        data: ")
            json.Append(PregionData)
            json.Append(",
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
            window.addEventListener(""resize"", function () {

                myChart_pie_region.resize();

            });
    </script>")
            '生成相应json数据
            context.Response.Write(json)

        Catch ex As Exception
            DB.Close(db, drDB)
            context.Response.Write(ex.Message)
            context.Response.Write(ex.StackTrace)
        End Try
    End Sub


    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class