Public Class echart_asset_liability
    Inherits System.Web.UI.Page
    Public errorshow1 As String
    Public errorshow2 As String
    Public flDate As New StringBuilder()
    Public Bdata As New StringBuilder()
    Public Ldata As New StringBuilder()
    Public Lmark As New StringBuilder()
    Public PData As New StringBuilder()

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
            '资产额获取：年初余额+本年发生至某月累计
            Dim asset_initial As Double
            Dim liability_initial As Double
            Dim asset(13) As Double
            Dim liability(13) As Double
            Dim asset_total As Double = 0
            Dim liability_total As Double = 0
            Dim zc As Double '资产
            Dim fz As Double '负债
            Dim zcfzl As Double '资产负债率

            '资产期初余额
            sSql = "select
                    (select isnull(sum(inital_bala),0) jf from f_inital_balance a left join f_item b on a.itemno=b.itemno 
                    where itemtype='4C569B56-F342-4F28-BB04-58797A1EC247' and fx_name='借方'and bookno=001)-
                    (select isnull(sum(inital_bala),0) df from f_inital_balance a left join f_item b on a.itemno=b.itemno 
                    where itemtype='4C569B56-F342-4F28-BB04-58797A1EC247' and fx_name='贷方'and bookno=001) asset_initial"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            asset_initial = drDB.Item("asset_initial")
            drDB.Close()

            '负债期初余额
            sSql = "select
                    (select isnull(sum(inital_bala),0) df from f_inital_balance a left join f_item b on a.itemno=b.itemno 
                    where itemtype='3A55DC83-38A7-4AF3-9672-9E79337563E9' and fx_name='贷方'and bookno=001)-
                    (select isnull(sum(inital_bala),0) jf from f_inital_balance a left join f_item b on a.itemno=b.itemno 
                    where itemtype='3A55DC83-38A7-4AF3-9672-9E79337563E9' and fx_name='借方'and bookno=001) liability_initial"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            liability_initial = drDB.Item("liability_initial")
            drDB.Close()

            For i = 1 To month
                '本年发生至某月累计
                sSql = "select isnull(sum(jf_bala-df_bala),0) bala from f_voucher_entry a 
                        inner join f_voucher b on a.voucherid=b.autoinc 
                        left join f_item c on a.itemno=c.itemno
                        where b.bookno=001 and itemtype='4C569B56-F342-4F28-BB04-58797A1EC247' and SUBSTRING(b.billdate,5,2)=  " & i.ToString() & " and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                asset(i) = drDB.Item("bala")
                drDB.Close()    '资产每月发生额读取

                sSql = "select isnull(sum(df_bala-jf_bala),0) bala from f_voucher_entry a 
                        inner join f_voucher b on a.voucherid=b.autoinc 
                        left join f_item c on a.itemno=c.itemno
                        where b.bookno=001 and itemtype='3A55DC83-38A7-4AF3-9672-9E79337563E9' and SUBSTRING(b.billdate,5,2)= " & i.ToString() & "  and fyear=DateName(YEAR,GetDate())"
                drDB = db.GetDataReader(sSql)
                drDB.Read()
                liability(i) = drDB.Item("bala")
                drDB.Close()    '负债每月发生额读取

                asset_total = asset_total + asset(i)
                liability_total = liability_total + liability(i)
                zc = asset_total + asset_initial    '资产类+ 资产期初余额
                fz = liability_total + liability_initial    '负债类+ 资产期初余额
                zcfzl = zc / fz * 100

                Bdata.Append("" & Format(zc / 10000, "#.##") & ",") '换算单位为万元，保留两位小数
                Ldata.Append("" & Format(zcfzl, "#.##") & ",")
                Lmark.Append("{value: " & Format(zcfzl, "#.##") & ", xAxis: " & i - 1 & ", yAxis: " & Format(zcfzl, "#.##") & "},")
            Next i

            '速动资产填入
            sSql = "select
                        (select isnull(sum(inital_bala),0) jf 
                        from f_inital_balance a 
                        left join f_item i on a.itemno=i.itemno
                        left join f_item_subtype ib on i.itemsubtype=ib.autoinc 
                        where ib.cnname='速动资产' and fx_name='借方'and bookno=001)
                        -
                        (select isnull(sum(inital_bala),0) df 
                        from f_inital_balance a 
                        left join f_item i on a.itemno=i.itemno
                        left join f_item_subtype ib on i.itemsubtype=ib.autoinc 
                        where ib.cnname='速动资产' and fx_name='贷方'and bookno=001)
                        +
                        (select sum(jf_bala-df_bala)
                        from f_voucher_entry a
                        inner join f_voucher b on a.voucherid=b.autoinc 
                        left join f_item i on a.itemno=i.itemno
                        left join f_item_subtype ib on i.itemsubtype=ib.autoinc
                        where ib.cnname='速动资产' and b.bookno=001 and fyear=DateName(YEAR,GetDate()))sudong"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            PData.Append("{""value"":")
            PData.Append("""" & Format(drDB.Item("sudong") / 10000, "#.##") & """,") '换算单位为万元，保留两位小数
            PData.Append("""name"":""速动资产""},")
            drDB.Close()

            '非速动资产填入
            sSql = "select
                        (select isnull(sum(inital_bala),0) jf 
                        from f_inital_balance a 
                        left join f_item i on a.itemno=i.itemno
                        left join f_item_subtype ib on i.itemsubtype=ib.autoinc 
                        where ib.cnname='流动资产' and fx_name='借方'and bookno=001)
                        -
                        (select isnull(sum(inital_bala),0) df 
                        from f_inital_balance a 
                        left join f_item i on a.itemno=i.itemno
                        left join f_item_subtype ib on i.itemsubtype=ib.autoinc 
                        where ib.cnname='流动资产' and fx_name='贷方'and bookno=001)
                        +
                        (select sum(jf_bala-df_bala)
                        from f_voucher_entry a
                        inner join f_voucher b on a.voucherid=b.autoinc 
                        left join f_item i on a.itemno=i.itemno
                        left join f_item_subtype ib on i.itemsubtype=ib.autoinc
                        where ib.cnname='流动资产' and b.bookno=001 and fyear=DateName(YEAR,GetDate()))feisudong"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            PData.Append("{""value"":")
            PData.Append("""" & Format(drDB.Item("feisudong") / 10000, "#.##") & """,") '换算单位为万元，保留两位小数
            PData.Append("""name"":""非速动资产""},")
            drDB.Close()

            '非流动资产填入
            sSql = "select
                (select isnull(sum(inital_bala),0) jf 
                from f_inital_balance a 
                left join f_item i on a.itemno=i.itemno
                left join f_item_subtype ib on i.itemsubtype=ib.autoinc 
                where ib.cnname='非流动资产' and fx_name='借方'and bookno=001)
                -
                (select isnull(sum(inital_bala),0) df 
                from f_inital_balance a 
                left join f_item i on a.itemno=i.itemno
                left join f_item_subtype ib on i.itemsubtype=ib.autoinc 
                where ib.cnname='非流动资产' and fx_name='贷方'and bookno=001)
                +
                (select sum(jf_bala-df_bala)
                from f_voucher_entry a
                inner join f_voucher b on a.voucherid=b.autoinc 
                left join f_item i on a.itemno=i.itemno
                left join f_item_subtype ib on i.itemsubtype=ib.autoinc
                where ib.cnname='非流动资产' and b.bookno=001 and fyear=DateName(YEAR,GetDate()))feiliudong"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            PData.Append("{""value"":")
            PData.Append("""" & Format(drDB.Item("feiliudong") / 10000, "#.##") & """,") '换算单位为万元，保留两位小数
            PData.Append("""name"":""非流动资产""},")
            drDB.Close()

        Catch ex As Exception
            DB.Close(db, drDB)
            errorshow1 = ex.Message
            errorshow2 = ex.StackTrace
        End Try
    End Sub

End Class