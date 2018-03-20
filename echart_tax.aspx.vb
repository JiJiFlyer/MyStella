Public Class echart_tax
    Inherits System.Web.UI.Page

    Public errorshow1 As String
    Public errorshow2 As String
    Public flDate As New StringBuilder()
    Public Bdata2221002 As New StringBuilder()
    Public Ldata2221002 As New StringBuilder()
    Public Lmark2221002 As New StringBuilder()
    Public Bdata2221005 As New StringBuilder()
    Public Ldata2221005 As New StringBuilder()
    Public Lmark2221005 As New StringBuilder()
    Public BdataDS As New StringBuilder()
    Public LdataDS As New StringBuilder()
    Public LmarkDS As New StringBuilder()
    Public BdataQS As New StringBuilder()
    Public LdataQS As New StringBuilder()
    Public LmarkQS As New StringBuilder()
    Public taxPiedata As New StringBuilder()
    Public BdataHelp As New StringBuilder()
    Public BdataTotal As New StringBuilder()
    Public LdataTotal As New StringBuilder()
    Public LmarkTotal As New StringBuilder()

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
            Dim tax_2221(13) As Double
            Dim tax_2221001(13) As Double
            Dim month As Integer = Format(Now(), "MM")
            For i = 1 To month
                flDate.Append("""" & i.ToString() & "月"",")
            Next i
            flDate.Append("]")

            '以月份作控制变量循环 存fee数组用于计算
            '6001 主营业务收入
            For i = 1 To month
                sSql = "select isnull(sum(df_bala),0) as fee from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '6001%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6001(i) = drDB.Item("fee")
                drDB.Close()
            Next i

            '6051 其他业务收入
            For i = 1 To month
                sSql = "select isnull(sum(df_bala),0) as fee from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '6051%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                fee_6051(i) = drDB.Item("fee")
                drDB.Close()
            Next i

            '2221
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as tax from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '2221%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                tax_2221(i) = drDB.Item("tax")
                drDB.Close()
            Next i

            '2221001
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as tax from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '2221001%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                tax_2221001(i) = drDB.Item("tax")
                drDB.Close()
            Next i

            '2221002 每次循环读一次sql写入Bdata,存fee数组用于计算费率,写入Ldata
            '增值税税额
            Dim tax_2221002(13) As Double
            Dim zzsl As Double
            Bdata2221002.Append("[")
            Ldata2221002.Append("[")
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as zzse from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '2221002%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                tax_2221002(i) = drDB.Item("zzse")
                Bdata2221002.Append("" & Format(drDB.Item("zzse"), "#.##") & ",")
                drDB.Close()
                '增值税税负率
                zzsl = tax_2221002(i) / (fee_6001(i) + fee_6051(i)) * 100
                Ldata2221002.Append("" & Format(zzsl, "#.##") & ",")
                Lmark2221002.Append("{value: " & Format(zzsl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(zzsl, "#.##") & "},")
            Next i
            Bdata2221002.Append("]")
            Ldata2221002.Append("]")

            '2221005 每次循环读一次sql写入Bdata,存fee数组用于计算费率,写入Ldata
            '所得税税额
            Dim tax_2221005(13) As Double
            Dim sdsl As Double
            Bdata2221005.Append("[")
            Ldata2221005.Append("[")
            For i = 1 To month
                sSql = "select isnull(sum(jf_bala),0) as sdse from f_voucher_entry a inner join f_voucher b on b.autoinc=a.voucherid 
                        where itemno like '2221005%' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                tax_2221005(i) = drDB.Item("sdse")
                Bdata2221005.Append("" & Format(drDB.Item("sdse"), "#.##") & ",")
                drDB.Close()
                '所得税税负率
                sdsl = tax_2221005(i) / (fee_6001(i) + fee_6051(i)) * 100
                Ldata2221005.Append("" & Format(sdsl, "#.##") & ",")
                Lmark2221005.Append("{value: " & Format(sdsl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(sdsl, "#.##") & "},")
            Next i
            Bdata2221005.Append("]")
            Ldata2221005.Append("]")

            'DS 每次循环读一次sql写入Bdata,存fee数组用于计算费率,写入Ldata
            '地税税额
            Dim tax_DS(13) As Double
            Dim DSsl As Double
            BdataDS.Append("[")
            LdataDS.Append("[")
            For i = 1 To month
                tax_DS(i) = tax_2221(i) - tax_2221001(i) - tax_2221002(i) - tax_2221005(i)
                BdataDS.Append("" & Format(tax_DS(i), "#.##") & ",")
                '地税税率
                DSsl = tax_DS(i) / (fee_6001(i) + fee_6051(i)) * 100
                LdataDS.Append("" & Format(DSsl, "#.##") & ",")
                LmarkDS.Append("value: " & Format(DSsl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(DSsl, "#.##") & "},")
            Next i
            BdataDS.Append("]")
            LdataDS.Append("]")

            'QS 每次循环计算期间费用写入Bdata,Ldata
            '全税税额税率
            Dim tax_QS(13) As Double
            Dim QSsl As Double
            BdataQS.Append("[")
            LdataQS.Append("[")
            For i = 1 To month
                tax_QS(i) = tax_DS(i) + tax_2221002(i) + tax_2221005(i)
                QSsl = tax_QS(i) / (fee_6001(i) + fee_6051(i)) * 100
                BdataQS.Append("" & Format(tax_QS(i), "#.##") & ",")
                LdataQS.Append("" & Format(QSsl, "#.##") & ",")
                LmarkQS.Append("{value: " & Format(QSsl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(QSsl, "#.##") & "},")
            Next i
            BdataQS.Append("]")
            LdataQS.Append("]")

            '计算三种费用累计值
            Dim tax2221002_total As Double = 0
            Dim tax2221005_total As Double = 0
            Dim taxDS_total As Double = 0
            Dim taxQS_total As Double = 0
            Dim fee6001_total As Double = 0
            Dim fee6051_total As Double = 0
            '截止某月，替换month即可
            For i = 1 To month
                tax2221002_total = tax2221002_total + tax_2221002(i)
                tax2221005_total = tax2221005_total + tax_2221005(i)
                taxDS_total = taxDS_total + tax_DS(i)
                taxQS_total = taxQS_total + tax_QS(i)
                fee6001_total = fee6001_total + fee_6001(i)
                fee6051_total = fee6051_total + fee_6051(i)
            Next i
            '饼图数据输入
            taxPiedata.Append("{value:" & Format(tax2221002_total, "#.##") & ", name:'增值税'},
                {value:" & Format(tax2221005_total, "#.##") & ", name:'所得税'},
                {value:" & Format(taxDS_total, "#.##") & ", name:'地税'}")
            '累计柱状折现图计算及数据输入
            Dim zzsl_t As Double = tax2221002_total / (fee6001_total + fee6051_total) * 100
            Dim sdsl_t As Double = tax2221005_total / (fee6001_total + fee6051_total) * 100
            Dim DSsl_t As Double = taxDS_total / (fee6001_total + fee6051_total) * 100
            Dim QSsl_t As Double = taxQS_total / (fee6001_total + fee6051_total) * 100
            BdataTotal.Append("" & Format(tax2221002_total, "#.##") & "," & Format(tax2221005_total, "#.##") & "," & Format(taxDS_total, "#.##") & ",
" & Format(taxQS_total, "#.##") & ",")
            BdataHelp.Append("0," & Format(tax2221002_total, "#.##") & "," & Format(tax2221002_total + tax2221005_total, "#.##") & ",0,")
            LdataTotal.Append("" & Format(zzsl_t, "#.##") & "," & Format(sdsl_t, "#.##") & "," & Format(DSsl_t, "#.##") & ",
" & Format(QSsl_t, "#.##") & ",")
            LmarkTotal.Append("{value : " & Format(zzsl_t, "#.##") & ", xAxis: 0, yAxis: " & Format(zzsl_t, "#.##") & "},
{value : " & Format(sdsl_t, "#.##") & ", xAxis: 1, yAxis: " & Format(sdsl_t, "#.##") & "},
{value : " & Format(DSsl_t, "#.##") & ", xAxis: 2, yAxis: " & Format(DSsl_t, "#.##") & "},
{value : " & Format(QSsl_t, "#.##") & ", xAxis: 3, yAxis: " & Format(QSsl_t, "#.##") & "},")
        Catch ex As Exception
            DB.Close(db, drDB)
            errorshow1 = ex.Message
            errorshow2 = ex.StackTrace
        End Try
    End Sub

End Class