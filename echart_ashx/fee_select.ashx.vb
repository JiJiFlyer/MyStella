Imports System.Web
Imports System.Web.Services

Public Class fee_select
    Implements System.Web.IHttpHandler
    Public flDate As New StringBuilder()
    Public Bdata6601 As New StringBuilder()
    Public Ldata6601 As New StringBuilder()
    Public Lmark6601 As New StringBuilder()
    Public Bdata6602 As New StringBuilder()
    Public Ldata6602 As New StringBuilder()
    Public Lmark6602 As New StringBuilder()
    Public Bdata6603 As New StringBuilder()
    Public Ldata6603 As New StringBuilder()
    Public Lmark6603 As New StringBuilder()
    Public BdataQJ As New StringBuilder()
    Public LdataQJ As New StringBuilder()
    Public LmarkQJ As New StringBuilder()
    Public BdataML As New StringBuilder()
    Public LdataML As New StringBuilder()
    Public LmarkML As New StringBuilder()
    Public feePiedata As New StringBuilder()
    Public BdataHelp As New StringBuilder()
    Public BdataTotal As New StringBuilder()
    Public LdataTotal As New StringBuilder()
    Public LmarkTotal As New StringBuilder()

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
            flDate.Append("[")
            Dim i As Integer
            Dim fee_6001(13) As Double
            Dim fee_6051(13) As Double
            Dim fee_6401(13) As Double
            Dim month As Integer = Format(Now(), "MM")
            For i = 1 To month
                flDate.Append("" & i.ToString() & ",")
            Next i
            flDate.Append("]")

            '以月份作控制变量循环 存fee数组用于计算
            '6001 主营业务收入
            For i = 1 To month
                sSql = "select isnull(sum(df_bala),0) as fee from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where bookno=" & bookno & " and itemno like '6001%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6001(i) = drDB.Item("fee")
                drDB.Close()
            Next i

            '6051 其他业务收入
            For i = 1 To month
                sSql = "select isnull(sum(df_bala),0) as fee from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where bookno=" & bookno & " and itemno like '6051%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6051(i) = drDB.Item("fee")
                drDB.Close()
            Next i

            '6401 主营业务成本
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as fee from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where bookno=" & bookno & " and itemno like '6401%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6401(i) = drDB.Item("fee")
                drDB.Close()
            Next i

            '6601 每次循环读一次sql写入Bdata,存fee数组用于计算费率,写入Ldata
            '销售费用
            Dim fee_6601(13) As Double
            Dim xsfl As Double
            Bdata6601.Append("[")
            Ldata6601.Append("[")
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as xsfy from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where bookno=" & bookno & " and itemno like '6601%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6601(i) = drDB.Item("xsfy")
                Bdata6601.Append("" & Format(drDB.Item("xsfy"), "#.##") & ",")
                drDB.Close()
                '销售费率
                xsfl = fee_6601(i) / (fee_6001(i) + fee_6051(i)) * 100
                Ldata6601.Append("" & Format(xsfl, "#.##") & ",")
                Lmark6601.Append("{name : '销售费用率',value: " & Format(xsfl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(xsfl, "#.##") & "},")
            Next i
            Bdata6601.Append("]")
            Ldata6601.Append("]")

            '6602 每次循环读一次sql写入Bdata,存fee数组用于计算费率,写入Ldata
            '管理费用
            Dim fee_6602(13) As Double
            Dim glfl As Double
            Bdata6602.Append("[")
            Ldata6602.Append("[")
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as glfy from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where bookno=" & bookno & " and itemno like '6602%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6602(i) = drDB.Item("glfy")
                Bdata6602.Append("" & Format(drDB.Item("glfy"), "#.##") & ",")
                drDB.Close()
                '管理费率
                glfl = fee_6602(i) / (fee_6001(i) + fee_6051(i)) * 100
                Ldata6602.Append("" & Format(glfl, "#.##") & ",")
                Lmark6602.Append("{name : '管理费用率',value: " & Format(glfl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(glfl, "#.##") & "},")
            Next i
            Bdata6602.Append("]")
            Ldata6602.Append("]")

            '6603 每次循环读一次sql写入Bdata,存fee数组用于计算费率,写入Ldata
            '财务费用
            Dim fee_6603(13) As Double
            Dim cwfl As Double
            Bdata6603.Append("[")
            Ldata6603.Append("[")
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as cwfy from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where bookno=" & bookno & " and itemno like '6603%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6603(i) = drDB.Item("cwfy")
                Bdata6603.Append("" & Format(drDB.Item("cwfy"), "#.##") & ",")
                drDB.Close()
                '财务费率
                cwfl = fee_6603(i) / (fee_6001(i) + fee_6051(i)) * 100
                Ldata6603.Append("" & Format(cwfl, "#.##") & ",")
                Lmark6603.Append("{name : '财务费用率',value: " & Format(cwfl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(cwfl, "#.##") & "},")
            Next i
            Bdata6603.Append("]")
            Ldata6603.Append("]")

            'QJ 每次循环计算期间费用写入Bdata,Ldata
            '期间费用
            Dim qjfy As Double
            Dim qjfl As Double
            BdataQJ.Append("[")
            LdataQJ.Append("[")
            For i = 1 To month
                qjfy = fee_6601(i) + fee_6602(i) + fee_6603(i)
                qjfl = qjfy / (fee_6001(i) + fee_6051(i)) * 100
                BdataQJ.Append("" & Format(qjfy, "#.##") & ",")
                LdataQJ.Append("" & Format(qjfl, "#.##") & ",")
                LmarkQJ.Append("{name : '期间费用率',value: " & Format(qjfl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(qjfl, "#.##") & "},")
            Next i
            BdataQJ.Append("]")
            LdataQJ.Append("]")

            '毛利/毛利率 每次循环读一次sql写入Bdata,存fee数组用于计算费率,写入Ldata
            '毛利
            Dim fee_ml(13) As Double
            Dim mll As Double
            BdataML.Append("[")
            LdataML.Append("[")
            For i = 1 To month
                fee_ml(i) = fee_6001(i) - fee_6401(i)
                BdataML.Append("" & Format(fee_ml(i), "#.##") & ",")
                '毛利率
                mll = fee_ml(i) / fee_6001(i) * 100
                LdataML.Append("" & Format(mll, "#.##") & ",")
                LmarkML.Append("{name : '毛利率',value: " & Format(mll, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(mll, "#.##") & "},")
            Next i
            BdataML.Append("]")
            LdataML.Append("]")

            '计算三种费用累计值
            Dim fee6601_total As Double = 0
            Dim fee6602_total As Double = 0
            Dim fee6603_total As Double = 0
            Dim fee6001_total As Double = 0
            Dim fee6051_total As Double = 0
            Dim fee6401_total As Double = 0
            Dim qjfy_total As Double
            '截止某月，替换month即可
            For i = 1 To month
                fee6601_total = fee6601_total + fee_6601(i)
                fee6602_total = fee6602_total + fee_6602(i)
                fee6603_total = fee6603_total + fee_6603(i)
                fee6001_total = fee6001_total + fee_6001(i)
                fee6051_total = fee6051_total + fee_6051(i)
                fee6401_total = fee6401_total + fee_6401(i)
            Next i
            '饼图数据输入
            feePiedata.Append("{value:" & Format(fee6601_total, "#.##") & ", name:'销售费用'},
                {value:" & Format(fee6602_total, "#.##") & ", name:'管理费用'},
                {value:" & Format(fee6603_total, "#.##") & ", name:'财务费用'}")
            '累计柱状折现图计算及数据输入
            qjfy_total = fee6601_total + fee6602_total + fee6603_total
            Dim xsfl_t As Double = fee6601_total / (fee6001_total + fee6051_total) * 100
            Dim glfl_t As Double = fee6602_total / (fee6001_total + fee6051_total) * 100
            Dim cwfl_t As Double = fee6603_total / (fee6001_total + fee6051_total) * 100
            Dim qjfl_t As Double = qjfy_total / (fee6001_total + fee6051_total) * 100
            Dim mll_t As Double = (fee6001_total - fee6401_total) / fee6001_total * 100
            BdataTotal.Append("" & Format(fee6601_total, "#.##") & "," & Format(fee6602_total, "#.##") & "," & Format(fee6603_total, "#.##") & ",
                               " & Format(qjfy_total, "#.##") & "," & Format(fee6001_total, "#.##") & ",")
            BdataHelp.Append("0," & Format(fee6601_total, "#.##") & "," & Format(fee6601_total + fee6602_total, "#.##") & ",0,0,")
            LdataTotal.Append("" & Format(xsfl_t, "#.##") & "," & Format(glfl_t, "#.##") & "," & Format(cwfl_t, "#.##") & ",
                               " & Format(qjfl_t, "#.##") & "," & Format(mll_t, "#.##") & ",")
            LmarkTotal.Append("{value : " & Format(xsfl_t, "#.##") & ", xAxis: 0, yAxis: " & Format(xsfl_t, "#.##") & "},
                               {value : " & Format(glfl_t, "#.##") & ", xAxis: 1, yAxis: " & Format(glfl_t, "#.##") & "},
                               {value : " & Format(cwfl_t, "#.##") & ", xAxis: 2, yAxis: " & Format(cwfl_t, "#.##") & "},
                               {value : " & Format(qjfl_t, "#.##") & ", xAxis: 3, yAxis: " & Format(qjfl_t, "#.##") & "},
                               {value : " & Format(mll_t, "#.##") & ", xAxis: 4, yAxis: " & Format(mll_t, "#.##") & "},")
            Dim json As New StringBuilder
            json.Append("<script type=""text/javascript"">
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
                formatter: ""{b0}销售费用：{c0}元<br/>{b1}销售费用率：{c1}%""
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
                    data: ")
            json.Append(flDate)
            json.Append(",
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
                        data: ")
            json.Append(Bdata6601)
            json.Append("
                },
                    {
                        name: '费率',
                        type: 'line',
                        yAxisIndex: 1,
                        data: ")
            json.Append(Ldata6601)
            json.Append(",
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [")
            json.Append(Lmark6601)
            json.Append("]
                        }
                    }
                ]
        };

        echart_fee6601.setOption(option6601);
        window.addEventListener(""resize"", function () {

            echart_fee6601.resize();

        });//6601销售
    </script>

    <script type=""text/javascript"">
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
                    formatter: ""{b0}管理费用：{c0}元<br/>{b1}管理费用率：{c1}%""
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
                        data: ")
            json.Append(flDate)
            json.Append("
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
                        data: ")
            json.Append(Bdata6602)
            json.Append("
                },
                    {
                        name: '费率',
                        type: 'line',
                        yAxisIndex: 1,
                        data: ")
            json.Append(Ldata6602)
            json.Append(",
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [")
            json.Append(Lmark6602)
            json.Append("]
                        }
                    }
                ]
            };

            echart_fee6602.setOption(option6602);
            window.addEventListener(""resize"", function () {

                echart_fee6602.resize();

            });//6602管理
    </script>

    <script type=""text/javascript"">
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
                    formatter: ""{b0}财务费用：{c0}元<br/>{b1}财务费用率：{c1}%""
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
                        data: ")
            json.Append(flDate)
            json.Append("
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
                        data: ")
            json.Append(Bdata6603)
            json.Append("
                },
                    {
                        name: '费率',
                        type: 'line',
                        yAxisIndex: 1,
                        data: ")
            json.Append(Ldata6603)
            json.Append(",
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [")
            json.Append(Lmark6603)
            json.Append("]
                        }
                    }
                ]
            };

            echart_fee6603.setOption(option6603);
            window.addEventListener(""resize"", function () {

                echart_fee6603.resize();

            });//6603财务
    </script>

    <script type=""text/javascript"">
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
                    formatter: ""{b0}期间费用：{c0}元<br/>{b1}期间费用率：{c1}%""
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
                        data: ")
            json.Append(flDate)
            json.Append("
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
                        data: ")
            json.Append(BdataQJ)
            json.Append("
                },
                    {
                        name: '费率',
                        type: 'line',
                        yAxisIndex: 1,
                        data: ")
            json.Append(LdataQJ)
            json.Append(",
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [")
            json.Append(LmarkQJ)
            json.Append("]
                        }
                    }
                ]
            };

            echart_feeQJ.setOption(optionQJ);
            window.addEventListener(""resize"", function () {

                echart_feeQJ.resize();

            });//QJ期间
    </script>

    <script type=""text/javascript"">
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
                    formatter: ""{b0}毛利：{c0}元<br/>{b1}毛利率：{c1}%""
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
                        data: ")
            json.Append(flDate)
            json.Append("
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
                        data: ")
            json.Append(BdataML)
            json.Append("
                },
                    {
                        name: '毛利率',
                        type: 'line',
                        yAxisIndex: 1,
                        data: ")
            json.Append(LdataML)
            json.Append(",
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [")
            json.Append(LmarkML)
            json.Append("]
                        }
                    }
                ]
            };

            echart_feeML.setOption(optionML);
            window.addEventListener(""resize"", function () {

                echart_feeML.resize();

            });//ML毛利，6401无数据，待检验
    </script>

    <script type=""text/javascript"">
            var echart_pie_fee = echarts.init(document.getElementById('feePie'), 'fee_tax');
            option_pie_fee = {
                title: {
                    text: '年度费用占比情况',
                    subtext: '1月至现在的累计额',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: ""{b} : {c}元 ({d}%)""
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
                        data: [")
            json.Append(feePiedata)
            json.Append("],
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
            window.addEventListener(""resize"", function () {

                echart_pie_fee.resize();

            });//年度费用占比情况
    </script>

    <script type=""text/javascript"">
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
                    formatter: ""{b0}{a1}：{c1}元<br/>{b1}{a2}：{c2}%""
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
                        data: [")
            json.Append(BdataHelp)
            json.Append("]
                    },
                    {
                        name: '费用',
                        type: 'bar',
                        stack: '总量',
                        //itemStyle: { normal: { label: { show: true, position: 'inside' } } },
                        data: [")
            json.Append(BdataTotal)
            json.Append("]
                    },
                    {
                        name: '费率',
                        type: 'line',
                        yAxisIndex: 1,
                        data: [")
            json.Append(LdataTotal)
            json.Append("],
                        markPoint: {
                            symbol: 'circle',
                            symbolSize: 25,
                            data: [")
            json.Append(LmarkTotal)
            json.Append("]
                        }
                    }
                ]
            };
            echart_feeTotal.setOption(optionTotal);
            window.addEventListener(""resize"", function () {

                echart_feeTotal.resize();

            });//Total累计
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