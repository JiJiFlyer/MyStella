Public Class echart_fee
    Inherits System.Web.UI.Page

    Public errorshow1 As String
    Public errorshow2 As String
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As DB
        Dim drDB As SqlClient.SqlDataReader
        Dim sSql As String
        db = New DB
        Try
            flDate.Append("[")
            Dim i As Integer
            Dim fee_6001(13) As Double
            Dim fee_6051(13) As Double
            Dim month As Integer = Format(Now(), "MM")
            For i = 1 To month
                flDate.Append("""" & i.ToString() & "月"",")
            Next i
            flDate.Append("]")

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

            '6601 每次循环读一次sql写入Bdata,存fee数组用于计算费率,写入Ldata
            '销售费用
            Dim fee_6601(13) As Double
            Dim xsfl As Double
            Bdata6601.Append("[")
            Ldata6601.Append("[")
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as xsfy from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '6601%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
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
                        where itemno like '6602%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
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
                        where itemno like '6603%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
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
                sSql = "select isnull(sum(jf_bala),0) as ml from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '6401%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_ml(i) = fee_6001(i) - drDB.Item("ml")
                BdataML.Append("" & Format(fee_ml(i), "#.##") & ",")
                drDB.Close()
                '毛利率
                mll = fee_ml(i) / fee_6001(i) * 100
                LdataML.Append("" & Format(mll, "#.##") & ",")
                LmarkML.Append("{name : '毛利率',value: " & Format(mll, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(mll, "#.##") & "},")
            Next i
            BdataML.Append("]")
            LdataML.Append("]")

        Catch ex As Exception
            DB.Close(db, drDB)
            errorshow1 = ex.Message
            errorshow2 = ex.StackTrace
        End Try
    End Sub

End Class