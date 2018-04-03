Public Class echart_buy

    Inherits System.Web.UI.Page
    Public errorshow1 As String
    Public errorshow2 As String
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sSql = "select goodsname,sum(tprice) as tprice from echart_buy where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
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
                sSql = "select distinct goodsunit from echart_buy where goodsname = '" & topgoods(i) & "'"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                Unit(i) = drDB.Item("goodsunit")
                drDB.Close()
            Next i

            For i = 1 To month
                '销售额第一产品量价图数据填入
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_buy 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                        and goodsname = '" & topgoods(1) & "'"
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_buy 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                        and goodsname = '" & topgoods(2) & "'"
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_buy 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                        and goodsname = '" & topgoods(3) & "'"
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_buy 
                        where SUBSTRING(billdate,5,2)= " & i.ToString() & " and SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                        and goodsname = '" & topgoods(4) & "'"
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

        Catch ex As Exception
            DB.Close(db, drDB)
            errorshow1 = ex.Message
            errorshow2 = ex.StackTrace
        End Try
    End Sub

End Class