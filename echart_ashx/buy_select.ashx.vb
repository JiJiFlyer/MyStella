Imports System.Web
Imports System.Web.Services

Public Class buy_select
    Implements System.Web.IHttpHandler
    Public flDate As New StringBuilder()
    Public topgoods(10) As String
    Public unit(5) As String
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
            Dim i As Integer
        Dim month As Integer = Format(Now(), "MM")
        For i = 1 To month
            flDate.Append("" & i.ToString() & ",")
        Next i

        Dim a As Integer = 1
            '先得出本年度至今采购额最高的四个产品
            sSql = "select goodsname,sum(tprice) as tprice from echart_buy where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & "
                    group by goodsname order by tprice desc"
            drDB = DB.GetDataReader(sSql)
        While drDB.Read()
            If Not IsDBNull(drDB.Item("goodsname")) Then '去除产品名为空的产品
                topgoods(a) = drDB.Item("goodsname")
                a = a + 1
            End If
        End While
        drDB.Close()

        '获取前四产品的计量单位
        For i = 1 To 4
                sSql = "select distinct goodsunit from echart_buy where goodsname = '" & topgoods(i) & "' and bookno = " & bookno & ""
                drDB = DB.GetDataReader(sSql)
            drDB.Read()
            unit(i) = drDB.Item("goodsunit")
            drDB.Close()
        Next i

        For i = 1 To month
                '销售额第一产品量价图数据填入
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_buy 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & "
                        and goodsname = '" & topgoods(1) & "'"
                drDB = DB.GetDataReader(sSql)
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_buy 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & "
                        and goodsname = '" & topgoods(2) & "'"
                drDB = DB.GetDataReader(sSql)
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_buy 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & "
                        and goodsname = '" & topgoods(3) & "'"
                drDB = DB.GetDataReader(sSql)
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_buy 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) and bookno = " & bookno & "
                        and goodsname = '" & topgoods(4) & "'"
                drDB = DB.GetDataReader(sSql)
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
            Dim json As New StringBuilder

            json.Append("<script type=""""text/javascript"">
        var buybar1 = echarts.init(document.getElementById('buybar1'), 'dark');
        option_buybar1 = {
            grid: {
                left: '15%',
                right: '15%',
            },
            title: {
                text: 'TOP1:" & topgoods(1) & "',
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
                    data: [")
            json.Append(Bdata1)
            json.Append("]
                },
                {
                    name: '价格',
                    type: 'line',
                    yAxisIndex: 1,
                    data: [")
            json.Append(Ldata1)
            json.Append("],
                    markPoint: {
                        symbol: 'circle',
                        symbolSize: 25,
                        data: [")
            json.Append(Lmark1)
            json.Append("]
                    }
                }
            ]
        };

        buybar1.setOption(option_buybar1);
        window.addEventListener(""resize"", function () {

            buybar1.resize();

        });//采购额第一产品量价图
    </script>/

    <script type=""text/javascript"">
        var buybar2 = echarts.init(document.getElementById('buybar2'), 'dark');
        option_buybar2 = {
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
                    data: [")
            json.Append(Bdata2)
            json.Append("]
                },
                {
                    name: '价格',
                    type: 'line',
                    yAxisIndex: 1,
                    data: [")
            json.Append(Ldata2)
            json.Append("],
                    markPoint: {
                        symbol: 'circle',
                        symbolSize: 25,
                        data: [")
            json.Append(Lmark2)
            json.Append("]
                    }
                }
            ]
        };

        buybar2.setOption(option_buybar2);
        window.addEventListener(""resize"", function () {

            buybar2.resize();

        });//采购额第二产品量价图
    </script>

    <script type=""text/javascript"">
        var buybar3 = echarts.init(document.getElementById('buybar3'), 'dark');
        option_buybar3 = {
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
                        data: [")
            json.Append(Bdata3)
            json.Append("]
                    },
                    {
                        name: '价格',
                        type: 'line',
                        yAxisIndex: 1,
                        data: [")
            json.Append(Ldata3)
            json.Append("],
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [")
            json.Append(Lmark3)
            json.Append("]
                        }
                    }
                ]
        };

        buybar3.setOption(option_buybar3);
        window.addEventListener(""resize"", function () {

            buybar3.resize();

        });//采购额第三产品量价图
    </script>

    <script type=""text/javascript"">
        var buybar4 = echarts.init(document.getElementById('buybar4'), 'dark');
            option_buybar4 = {
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
                        data: [")
            json.Append(Bdata4)
            json.Append("]
                    },
                    {
                        name: '价格',
                        type: 'line',
                        yAxisIndex: 1,
                        data: [")
            json.Append(Ldata4)
            json.Append("],
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [")
            json.Append(Lmark4)
            json.Append("]
                        }
                    }
                ]
            };

            buybar4.setOption(option_buybar4);
            window.addEventListener(""resize"", function () {

                buybar4.resize();

            });//采购额第四产品量价图
    </script>
")

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