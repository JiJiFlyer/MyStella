Partial Public Class echarstest
    Inherits System.Web.UI.Page

    Public fReceive As Double
    Public fSales As Double
    Public fBankroll As Double
    Public fTicket As Double
    Public fReceiveMax As Double
    Public fSalesMax As Double
    Public fBankrollMax As Double
    Public fTicketMax As Double
    Public fReceiveMin As Double
    Public fSalesMin As Double
    Public fBankrollMin As Double
    Public fTicketMin As Double
    Public sSalesUnit As String
    Public fSalesUnitNum As Integer
    Public sBankrollUnit As String
    Public fBankrollUnitNum As Integer
    Public sTicketUnit As String
    Public fTicketUnitNum As Integer
    Public flDate As New StringBuilder()
    Public DataLegend As New StringBuilder()
    Public DataLegend_false As New StringBuilder()
    Public Bdata As New StringBuilder()
    Public Ldata As New StringBuilder()
    Public Psalesmanlegend As New StringBuilder()
    Public PsalesmanData As New StringBuilder()
    Public PsalesmanTop As New StringBuilder()
    Public Pregionlegend As New StringBuilder()
    Public PregionData As New StringBuilder()
    Public PregionTop As New StringBuilder()
    Public Pproductlegend As New StringBuilder()
    Public PproductData As New StringBuilder()
    Public PproductTop As New StringBuilder()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As DB
        Dim drDB As SqlClient.SqlDataReader
        Dim sSql As String
        Try
            sSql = "select sum(a.jf_bala) as receive from f_voucher_entry a inner join f_voucher b on a.voucherid=b.autoinc where b.bookno=001 and b.fmonth=(select(select convert(char(4),year(getdate()),20))+(select convert(char(2),right('0'+CAST(month(getdate())as nvarchar(2)),2),20)))and (a.itemno like '1001%' or a.itemno like '1002%' or a.itemno like '1121%') and (a.custom11='7705CBB9-6B63-4EBB-AB88-2C278C2EBB82')"
            db = New DB
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fReceive = drDB.Item("receive")
            drDB.Close()
            sSql = "select sum(a.df_bala) as sales from f_voucher_entry a inner join f_voucher b on a.voucherid=b.autoinc where(bookno = 1)and b.fmonth=(select(select convert(char(4),year(getdate()),20))+(select convert(char(2),right('0'+CAST(month(getdate())as nvarchar(2)),2),20)))and (a.itemno like '6001%' or a.itemno like '217100106%')"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fSales = drDB.Item("sales")
            drDB.Close()
            sSql = "select(select sum(a.jf_bala-a.df_bala) from f_voucher_entry a inner join f_voucher b on a.voucherid =b.autoinc where(bookno = 1)and b.fyear=(select convert(char(4),year(getdate())))and (a.itemno like '1001%' or a.itemno like '1002%'))+(select SUM(inital_bala) from f_inital_balance where itemno like '1001%' or itemno like '1002%') as bankroll"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fBankroll = drDB.Item("bankroll")
            drDB.Close()
            sSql = "select(select sum(a.jf_bala-a.df_bala) from f_voucher_entry a inner join f_voucher b on a.voucherid=b.autoinc where(bookno = 1) and b.fyear=(select convert(char(4),year(getdate())))and (a.itemno like '1121%'))+(select SUM(inital_bala) from f_inital_balance where itemno like '1121%') as ticket"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fTicket = drDB.Item("ticket")
            drDB.Close()
            sSql = "select min from tickset where autoinc='cc12decb-b5ec-4c9d-a4a9-945b31a40474'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fReceiveMin = drDB.Item("min")
            drDB.Close()
            sSql = "select max from tickset where autoinc='cc12decb-b5ec-4c9d-a4a9-945b31a40474'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fReceiveMax = drDB.Item("max")
            drDB.Close()
            sSql = "select min from tickset where autoinc='10fe9836-ae6b-47f1-af19-05d1c1d1140c'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fSalesMin = drDB.Item("min")
            drDB.Close()
            sSql = "select max from tickset where autoinc='10fe9836-ae6b-47f1-af19-05d1c1d1140c'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fSalesMax = drDB.Item("max")
            drDB.Close()
            sSql = "select min from tickset where autoinc='b8485bee-2599-4ae5-83be-bda31bb0434d'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fBankrollMin = drDB.Item("min")
            drDB.Close()
            sSql = "select max from tickset where autoinc='b8485bee-2599-4ae5-83be-bda31bb0434d'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fBankrollMax = drDB.Item("max")
            drDB.Close()
            sSql = "select min from tickset where autoinc='5a06d2e6-594a-4e31-9396-8ad472a720df'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fTicketMin = drDB.Item("min")
            drDB.Close()
            sSql = "select max from tickset where autoinc='5a06d2e6-594a-4e31-9396-8ad472a720df'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fTicketMax = drDB.Item("max")
            drDB.Close()
            sSql = "select unit from tickset where autoinc='10fe9836-ae6b-47f1-af19-05d1c1d1140c'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            sSalesUnit = drDB.Item("unit").ToString()
            drDB.Close()
            sSql = "select number from bas_moneyunit where unit='" & sSalesUnit & "'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fSalesUnitNum = drDB.Item("number")
            drDB.Close()
            sSql = "select unit from tickset where autoinc='B8485BEE-2599-4AE5-83BE-BDA31BB0434D'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            sBankrollUnit = drDB.Item("unit").ToString()
            drDB.Close()
            sSql = "select number from bas_moneyunit where unit='" & sBankrollUnit & "'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fBankrollUnitNum = drDB.Item("number")
            drDB.Close()
            sSql = "select unit from tickset where autoinc='5A06D2E6-594A-4E31-9396-8AD472A720DF'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            sTicketUnit = drDB.Item("unit").ToString()
            drDB.Close()
            sSql = "select number from bas_moneyunit where unit='" & sTicketUnit & "'"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            fTicketUnitNum = drDB.Item("number")
            drDB.Close()
            ' Dim iRowCount As Integer
            'sSql = "select count(distinct billdate) as count from echart_sell where goodsname='维生素A-(电仪)(照明)'"
            'db = New DB
            'drDB = db.GetDataReader(sSql)
            'drDB.Read()
            'iRowCount = drDB.Item("count")
            'drDB.Close()
            'Dim j As Integer = 0
            'sSql = "select distinct billdate from echart_sell"
            'db = New DB
            'drDB = db.GetDataReader(sSql)
            'While drDB.Read
            'flDate(j) = drDB.Item("billdate").ToString()
            'j = j + 1
            'End While

            Dim j As Integer = 0
            Dim t As Integer = 0
            Dim iRowCount As Integer
            sSql = "select count(distinct salesmanname) as count from echart_sell"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            iRowCount = drDB.Item("count")
            drDB.Close()
            sSql = "select salesmanname,sum(tprice) as tprice from echart_sell group by salesmanname order by tprice desc"
            drDB = db.GetDataReader(sSql)
            Psalesmanlegend.Append("[")
            PsalesmanData.Append("[")
            PsalesmanTop.Append("{")
            While drDB.Read
                If drDB.Item("salesmanname").ToString = "" Then
                    Continue While
                End If
                j = j + 1
                t = t + 1
                Psalesmanlegend.Append("""" & drDB.Item("salesmanname").ToString() & """,")
                PsalesmanData.Append("{")
                PsalesmanData.Append("""value"":")
                PsalesmanData.Append("""" & Format(drDB.Item("tprice"), "#.##") & """,")
                PsalesmanData.Append("""name"":")
                PsalesmanData.Append("""" & drDB.Item("salesmanname").ToString() & """")
                If t > 10 Then
                    PsalesmanTop.Append("""" & drDB.Item("salesmanname").ToString() & """:")
                    PsalesmanTop.Append("false ,")
                End If
                If j < iRowCount Then
                    PsalesmanData.Append("},")
                Else
                    PsalesmanData.Append("}")
                End If
            End While
            Psalesmanlegend.Append("]")
            PsalesmanData.Append("]")
            PsalesmanTop.Append("}")
            drDB.Close()

            j = 0
            t = 0
            sSql = "select count(distinct region) as count from echart_sell"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            iRowCount = drDB.Item("count")
            drDB.Close()
            sSql = "select region,sum(tprice) as tprice from echart_sell group by region order by tprice desc"
            drDB = db.GetDataReader(sSql)
            Pregionlegend.Append("[")
            PregionData.Append("[")
            PregionTop.Append("{")
            While drDB.Read
                If drDB.Item("region").ToString = "" Then
                    Continue While
                End If
                j = j + 1
                t = t + 1
                PregionData.Append("{")
                PregionData.Append("""value"":")
                PregionData.Append("""" & Format(drDB.Item("tprice"), "#.##") & """,")
                PregionData.Append("""name"":")
                Pregionlegend.Append("""" & drDB.Item("region").ToString() & """,")
                PregionData.Append("""" & drDB.Item("region").ToString() & """")
                If t > 10 Then
                    PregionTop.Append("""" & drDB.Item("region").ToString() & """:")
                    PregionTop.Append("false ,")
                End If
                If j < iRowCount Then
                    PregionData.Append("},")
                Else
                    PregionData.Append("}")
                End If
            End While
            Pregionlegend.Append("]")
            PregionData.Append("]")
            PregionTop.Append("}")
            drDB.Close()

            j = 0
            t = 0
            sSql = "select count(distinct goodsname) as count from echart_sell"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            iRowCount = drDB.Item("count")
            drDB.Close()
            sSql = "select goodsname,sum(tprice) as tprice from echart_sell group by goodsname order by tprice desc"
            drDB = db.GetDataReader(sSql)
            Pproductlegend.Append("[")
            PproductData.Append("[")
            PproductTop.Append("{")
            While drDB.Read
                If drDB.Item("goodsname").ToString = "" Then
                    Continue While
                End If
                j = j + 1
                t = t + 1
                PproductData.Append("{")
                PproductData.Append("""value"":")
                PproductData.Append("""" & Format(drDB.Item("tprice"), "#.##") & """,")
                PproductData.Append("""name"":")
                Pproductlegend.Append("""" & drDB.Item("goodsname").ToString() & """,")
                PproductData.Append("""" & drDB.Item("goodsname").ToString() & """")
                If t > 10 Then
                    PproductTop.Append("""" & drDB.Item("goodsname").ToString() & """:")
                    PproductTop.Append("false ,")
                End If
                If j < iRowCount Then
                    PproductData.Append("},")
                Else
                    PproductData.Append("}")
                End If
            End While
            Pproductlegend.Append("]")
            PproductData.Append("]")
            PproductTop.Append("}")
            drDB.Close()

            Dim a As Integer = 0
            Dim DateMatch(0 To 7) As String
            '先取最近七天的数据升序存入DateMatch数组
            sSql = "select distinct billdate from echart_sell 
                    where billdate in (select distinct top 7 billdate from echart_sell order by billdate desc)
                    order by billdate"
            drDB = db.GetDataReader(sSql)
            flDate.Append("[")
            While drDB.Read
                flDate.Append("""" & drDB.Item("billdate").ToString() & """,")
                DateMatch(a) = "" & drDB.Item("billdate").ToString() & ""
                a = a + 1
            End While
            flDate.Append("]")
            drDB.Close()

            Dim b As Integer = 0
            Dim GoodsMatch(0 To 100) As String
            '取产品名，升序存入GoodsMatch数组,DataLegend（用作前台的图例显示）,DataLegend_false(使默认不选择图例)
            sSql = "select distinct goodsname from echart_sell order by goodsname"
            drDB = db.GetDataReader(sSql)
            DataLegend.Append("[")
            DataLegend_false.Append("{")
            While drDB.Read
                DataLegend.Append("""" & drDB.Item("goodsname").ToString() & """,")
                DataLegend_false.Append("""" & drDB.Item("goodsname").ToString() & """:")
                DataLegend_false.Append("false ,")
                GoodsMatch(b) = "" & drDB.Item("goodsname").ToString() & ""
                b = b + 1
            End While
            DataLegend.Append("]")
            DataLegend_false.Append("}")
            drDB.Close()

            '以日期和产品名作控制变量双循环 每次循环读一次sql写入Bdata
            b = 0
            While GoodsMatch(b) <> ""
                Bdata.Append("{")
                Bdata.Append("name:")
                Bdata.Append("""" & GoodsMatch(b) & """,")
                Bdata.Append("type:'bar',") '条形图 销售量
                Bdata.Append("data: [")
                a = 0
                While DateMatch(a) <> ""
                    sSql = "select goodsname,billdate,SUM(amount) as amount from echart_sell
                     where goodsname='" & GoodsMatch(b) & "' and billdate=" & DateMatch(a) & "
                     group by goodsname,billdate order by billdate"
                    drDB = db.GetDataReader(sSql)
                    If drDB.Read() Then '若当天无数据 销量为0
                        Bdata.Append("" & drDB.Item("amount") & ",")
                        a = a + 1
                    Else
                        Bdata.Append("0,")
                        a = a + 1
                    End If
                    drDB.Close() '用完一次即关闭
                End While
                Bdata.Append("]},")
                b = b + 1
            End While

            Dim priceholder As Double
            Dim Lmark As New StringBuilder()
            b = 0
            While GoodsMatch(b) <> ""
                Ldata.Append("{")
                Ldata.Append("name:")
                Ldata.Append("""" & GoodsMatch(b) & """,")
                Ldata.Append("type:'line',") '折线图 当日平均单价
                Ldata.Append("lineStyle:{width:20},")
                Ldata.Append("symbolSize:7,") '设置端点大小
                Ldata.Append("yAxisIndex: 1,") '换右侧坐标轴表示
                Ldata.Append("data: [")
                a = 0 '用于控制标记的横坐标位置
                priceholder = 0
                While DateMatch(a) <> ""
                    sSql = "select goodsname,billdate,AVG(price) as price from echart_sell
                     where goodsname='" & GoodsMatch(b) & "' and billdate=" & DateMatch(a) & "
                     group by goodsname,billdate order by billdate"
                    drDB = db.GetDataReader(sSql)
                    If drDB.Read() Then
                        Ldata.Append("" & Format(drDB.Item("price"), "#.##") & ",")
                        Lmark.Append("{value: " & Format(drDB.Item("price"), "#.##") & ", xAxis: " & a & ", yAxis: " & Format(drDB.Item("price"), "#.##") & "},")
                        '设置标记
                        priceholder = drDB.Item("price") '存最后一个有数据的价格
                    Else '若当天无数据 单价不变
                        Ldata.Append("" & Format(priceholder, "#.##") & ",") '显示上一个数据的价格 保持不变 若是第一项则为空
                        If priceholder <> 0 Then '若无数据项不是第一项，标记价格
                            Lmark.Append("{value: " & Format(priceholder, "#.##") & ", xAxis: " & a & ", yAxis: " & Format(priceholder, "#.##") & "},")
                        End If
                    End If
                    a = a + 1
                    drDB.Close() '用完一次即关闭
                End While
                Ldata.Append("],")
                Ldata.Append("markPoint: { data: [")
                Ldata.Append(Lmark)
                Ldata.Append("]}")
                Ldata.Append("},")
                b = b + 1
                Lmark.Clear()
            End While

        Catch ex As Exception
            DB.Close(db, drDB)
        End Try
    End Sub

End Class