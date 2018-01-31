Public Class echart_fee
    Inherits System.Web.UI.Page

    Public errorshow1 As String
    Public errorshow2 As String
    Public flDate As New StringBuilder()
    Public Bdata As New StringBuilder()
    Public Ldata As New StringBuilder()
    Public Lmark As New StringBuilder()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As DB
        Dim drDB As SqlClient.SqlDataReader
        Dim sSql As String
        db = New DB
        Try
            flDate.Append("[")
            Dim i As Integer
            Dim fee_6601(13) As Double
            Dim fee_6001(13) As Double
            Dim fee_6051(13) As Double
            Dim month As Integer = Format(Now(), "MM")
            For i = 1 To month
                flDate.Append("""" & i.ToString() & "月"",")
            Next i
            flDate.Append("]")

            '6601 每次循环读一次sql写入Bdata,存fee数组用于计算费率
            '销售费用
            Bdata.Append("[")
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as xsfy from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '6601%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6601(i) = drDB.Item("xsfy")
                Bdata.Append("" & Format(drDB.Item("xsfy"), "#.##") & ",")
                drDB.Close()
            Next i
            Bdata.Append("]")

            '以月份作控制变量循环 存fee数组用于计算费率
            '6001
            For i = 1 To month
                sSql = "select isnull(sum(df_bala),0) as fee from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '6001%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6001(i) = drDB.Item("fee")
                drDB.Close()
            Next i

            '6051
            For i = 1 To month
                sSql = "select isnull(sum(df_bala),0) as fee from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '6051%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6051(i) = drDB.Item("fee")
                drDB.Close()
            Next i


            '销售费率
            Dim xsfl As Double
            Ldata.Append("[")
            For i = 1 To month
                xsfl = fee_6601(i) / (fee_6001(i) + fee_6051(i))
                Ldata.Append("" & Format(xsfl, "#.##") & ",")
                Lmark.Append("{name : '销售费用率',value: " & Format(xsfl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(xsfl, "#.##") & "},")
            Next i
            Ldata.Append("]")

        Catch ex As Exception
            DB.Close(db, drDB)
            errorshow1 = ex.Message
            errorshow2 = ex.StackTrace
        End Try
    End Sub

End Class