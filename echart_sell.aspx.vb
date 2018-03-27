Public Class echart_sell
    Inherits System.Web.UI.Page
    Public errorshow1 As String
    Public errorshow2 As String
    Public flDate As New StringBuilder()
    Public topgoods(10) As String
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
            '先得出本年度至今销量额最高的四个产品
            sSql = "select top 4 goodsname,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate())
                    group by goodsname order by tprice desc"
            drDB = db.GetDataReader(sSql)
            While drDB.Read()
                topgoods(a) = drDB.Item("goodsname")
                a = a + 1
            End While
            drDB.Close()

            For i = 1 To month
                '销售额第一产品量价图数据填入
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_sell 
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_sell 
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_sell 
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
                sSql = "select sum(amount) as amount,sum(tax_tprice)/sum(amount) as avgprice from echart_sell 
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

            '今年业务员业绩前十填入
            sSql = "select top 10 salesmanname,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) 
                    group by salesmanname order by tprice desc"
            drDB = db.GetDataReader(sSql)
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
            drDB.Close()

            '今年部门业绩前十填入
            sSql = "select top 10 dept,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) 
                    group by dept order by tprice desc"
            drDB = db.GetDataReader(sSql)
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
            drDB.Close()

            '今年产品销售额前十填入
            sSql = "select top 10 goodsname,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) 
                    group by goodsname order by tprice desc"
            drDB = db.GetDataReader(sSql)
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
            drDB.Close()

            '今年地区销售额前十填入
            sSql = "select top 10 region,sum(tprice) as tprice from echart_sell where SUBSTRING(billdate,1,4)=DateName(YEAR,GetDate()) 
                    group by region order by tprice desc"
            drDB = db.GetDataReader(sSql)
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
            drDB.Close()

        Catch ex As Exception
            DB.Close(db, drDB)
            errorshow1 = ex.Message
            errorshow2 = ex.StackTrace
        End Try
    End Sub

End Class