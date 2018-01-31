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
    Public AccountAge As New StringBuilder()
    Public AgeLabel As New StringBuilder()
    Public AccountAge_R As New StringBuilder()
    Public AgeLabel_R As New StringBuilder()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As DB
        Dim drDB As SqlClient.SqlDataReader
        Dim sSql As String
        Try
            '读取数据库中区间设置
            '定义agenumber用于得到区间节点数（区间数-1），用户最多设置十个节点
            Dim agenumber As Integer = 0
            '定义AgeData数组用于存数据库中读取的区间设置
            Dim AgeData(0 To 9) As Integer
            Dim i As Integer
            Dim tt As String() = {"t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7", "t8", "t9"}
            '定义abegin、aend记录数据库中begin和end参数
            Dim abegin As Integer
            Dim aend As Integer = 0
            sSql = "select * from dbo.bas_agesectionset"
            db = New DB
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            While Not IsDBNull(drDB.Item(tt(agenumber)))
                AgeData(agenumber) = drDB.Item(tt(agenumber))
                agenumber = agenumber + 1
            End While
            abegin = drDB.Item("begin")
            '提醒表标签行呈现
            If IsDBNull(drDB.Item("end")) Then
                AgeLabel_R.Append("<th>" & （abegin + 1）.ToString() & "天及以上</th>")
            Else
                aend = drDB.Item("end")
                AgeLabel_R.Append("<th>" & abegin.ToString() & "-" & aend.ToString() & "天</th>")
            End If
            drDB.Close()
            '账龄表标签行
            AgeLabel.Append("<th>1-" & AgeData(0).ToString() & "天</th>")
            For i = 1 To agenumber - 1
                AgeLabel.Append("<th>" & (AgeData(i - 1) + 1).ToString() & "-" & AgeData(i).ToString() & "天</th>")
            Next i
            AgeLabel.Append("<th>" & （AgeData(agenumber - 1) + 1).ToString() & "天及以上</th>")

            '账龄区间总额数组定义，用于累加该区间内金额
            Dim agetotal(10) As Double
            For i = 0 To 10
                agetotal(i) = 0
            Next i
            '该客户之前a_bal的累加和
            Dim pre_t_bal As Double = 0
            '该客户当前凭证经修正的a_bal
            Dim a_bal As Double
            '同客户前a_bal累计和,若其+a_bal>t_bal,则该条凭证a_bal=t_bal-pre_t_bal
            Dim pre_clientname As String
            '前三项的数据寄存
            Dim StringHolder1 As String
            '账龄数据寄存
            Dim StringHolder2 As String
            '第一个查询：初始化pre_clientname为表中第一位客户
            sSql = "select top 1 clientname from dbo.echart_accountage order by clientname,billdate desc"
            drDB = db.GetDataReader(sSql)
            drDB.Read()
            pre_clientname = drDB.Item("clientname")
            drDB.Close()
            '第二个查询：先判断是否是同一客户
            '再判断a_bal是否是超过t_bal并做调整
            '最后判断经过调整的a_bal处于哪一账龄区间，加至相应的agetotal
            sSql = "select * from dbo.echart_accountage order by clientname,billdate desc"
            drDB = db.GetDataReader(sSql)
            While drDB.Read
                '判断是否为新客户
                If drDB.Item("clientname") <> pre_clientname Then
                    '若是，重置前一客户统计的pre_t_bal和agetotal
                    For i = 0 To 10
                        agetotal(i) = 0
                    Next i
                    pre_t_bal = 0
                    '更新上一客户的数据
                    AccountAge.Append(StringHolder1)
                    AccountAge.Append(StringHolder2)
                End If
                '这个判断用于修正a_bal
                If drDB.Item("a_bal") + pre_t_bal < drDB.Item("t_bal") Then
                    '无需修正的情况
                    a_bal = drDB.Item("a_bal")
                    pre_t_bal = pre_t_bal + a_bal
                Else
                    '需修正的情况
                    a_bal = drDB.Item("t_bal") - pre_t_bal
                    pre_t_bal = pre_t_bal + a_bal
                End If
                '始终更新前三列,存入用于寄存的string变量
                StringHolder1 = """<tr><td>" & drDB.Item("clientname") & "</td><td>" & drDB.Item("bal_fx") & "</td><td>" & drDB.Item("t_bal") & "</td>"""
                '这个判断用于统计不同区间各自的总金额。先select用户设置的区间个数，根据区间的个数进行分支（简单粗暴）
                Select Case agenumber
                    Case 1
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 Then
                            agetotal(1) = agetotal(1) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td></tr>"""
                    Case 2
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 Then
                            agetotal(2) = agetotal(2) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td></tr>"""
                    Case 3
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 And drDB.Item("a_age") <= AgeData(2) Then
                            agetotal(2) = agetotal(2) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(2) + 1 Then
                            agetotal(3) = agetotal(3) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td><td>" & agetotal(3) & "</td></tr>"""
                    Case 4
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 And drDB.Item("a_age") <= AgeData(2) Then
                            agetotal(2) = agetotal(2) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(2) + 1 And drDB.Item("a_age") <= AgeData(3) Then
                            agetotal(3) = agetotal(3) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(3) + 1 Then
                            agetotal(4) = agetotal(4) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td><td>" & agetotal(3) & "</td><td>" & agetotal(4) & "</td></tr>"""
                    Case 5
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 And drDB.Item("a_age") <= AgeData(2) Then
                            agetotal(2) = agetotal(2) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(2) + 1 And drDB.Item("a_age") <= AgeData(3) Then
                            agetotal(3) = agetotal(3) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(3) + 1 And drDB.Item("a_age") <= AgeData(4) Then
                            agetotal(4) = agetotal(4) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(4) + 1 Then
                            agetotal(5) = agetotal(5) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td><td>" & agetotal(3) & "</td><td>" & agetotal(4) & "</td><td>" & agetotal(5) & "</td></tr>"""
                    Case 6
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 And drDB.Item("a_age") <= AgeData(2) Then
                            agetotal(2) = agetotal(2) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(2) + 1 And drDB.Item("a_age") <= AgeData(3) Then
                            agetotal(3) = agetotal(3) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(3) + 1 And drDB.Item("a_age") <= AgeData(4) Then
                            agetotal(4) = agetotal(4) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(4) + 1 And drDB.Item("a_age") <= AgeData(5) Then
                            agetotal(5) = agetotal(5) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(5) + 1 Then
                            agetotal(6) = agetotal(6) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td><td>" & agetotal(3) & "</td><td>" & agetotal(4) & "</td><td>" & agetotal(5) & "</td><td>" & agetotal(6) & "</td></tr>"""
                    Case 7
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 And drDB.Item("a_age") <= AgeData(2) Then
                            agetotal(2) = agetotal(2) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(2) + 1 And drDB.Item("a_age") <= AgeData(3) Then
                            agetotal(3) = agetotal(3) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(3) + 1 And drDB.Item("a_age") <= AgeData(4) Then
                            agetotal(4) = agetotal(4) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(4) + 1 And drDB.Item("a_age") <= AgeData(5) Then
                            agetotal(5) = agetotal(5) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(5) + 1 And drDB.Item("a_age") <= AgeData(6) Then
                            agetotal(6) = agetotal(6) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(6) + 1 Then
                            agetotal(7) = agetotal(7) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td><td>" & agetotal(3) & "</td><td>" & agetotal(4) & "</td><td>" & agetotal(5) & "</td><td>" & agetotal(6) & "</td><td>" & agetotal(7) & "</td></tr>"""
                    Case 8
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 And drDB.Item("a_age") <= AgeData(2) Then
                            agetotal(2) = agetotal(2) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(2) + 1 And drDB.Item("a_age") <= AgeData(3) Then
                            agetotal(3) = agetotal(3) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(3) + 1 And drDB.Item("a_age") <= AgeData(4) Then
                            agetotal(4) = agetotal(4) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(4) + 1 And drDB.Item("a_age") <= AgeData(5) Then
                            agetotal(5) = agetotal(5) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(5) + 1 And drDB.Item("a_age") <= AgeData(6) Then
                            agetotal(6) = agetotal(6) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(6) + 1 And drDB.Item("a_age") <= AgeData(7) Then
                            agetotal(7) = agetotal(7) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(7) + 1 Then
                            agetotal(8) = agetotal(8) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td><td>" & agetotal(3) & "</td><td>" & agetotal(4) & "</td><td>" & agetotal(5) & "</td><td>" & agetotal(6) & "</td><td>" & agetotal(7) & "</td><td>" & agetotal(8) & "</td></tr>"""
                    Case 9
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 And drDB.Item("a_age") <= AgeData(2) Then
                            agetotal(2) = agetotal(2) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(2) + 1 And drDB.Item("a_age") <= AgeData(3) Then
                            agetotal(3) = agetotal(3) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(3) + 1 And drDB.Item("a_age") <= AgeData(4) Then
                            agetotal(4) = agetotal(4) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(4) + 1 And drDB.Item("a_age") <= AgeData(5) Then
                            agetotal(5) = agetotal(5) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(5) + 1 And drDB.Item("a_age") <= AgeData(6) Then
                            agetotal(6) = agetotal(6) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(6) + 1 And drDB.Item("a_age") <= AgeData(7) Then
                            agetotal(7) = agetotal(7) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(7) + 1 And drDB.Item("a_age") <= AgeData(8) Then
                            agetotal(8) = agetotal(8) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(8) + 1 Then
                            agetotal(9) = agetotal(9) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td><td>" & agetotal(3) & "</td><td>" & agetotal(4) & "</td><td>" & agetotal(5) & "</td><td>" & agetotal(6) & "</td><td>" & agetotal(7) & "</td><td>" & agetotal(8) & "</td><td>" & agetotal(9) & "</td></tr>"""
                    Case 10
                        If drDB.Item("a_age") >= 1 And drDB.Item("a_age") <= AgeData(0) Then
                            agetotal(0) = agetotal(0) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(0) + 1 And drDB.Item("a_age") <= AgeData(1) Then
                            agetotal(1) = agetotal(1) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(1) + 1 And drDB.Item("a_age") <= AgeData(2) Then
                            agetotal(2) = agetotal(2) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(2) + 1 And drDB.Item("a_age") <= AgeData(3) Then
                            agetotal(3) = agetotal(3) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(3) + 1 And drDB.Item("a_age") <= AgeData(4) Then
                            agetotal(4) = agetotal(4) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(4) + 1 And drDB.Item("a_age") <= AgeData(5) Then
                            agetotal(5) = agetotal(5) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(5) + 1 And drDB.Item("a_age") <= AgeData(6) Then
                            agetotal(6) = agetotal(6) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(6) + 1 And drDB.Item("a_age") <= AgeData(7) Then
                            agetotal(7) = agetotal(7) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(7) + 1 And drDB.Item("a_age") <= AgeData(8) Then
                            agetotal(8) = agetotal(8) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(8) + 1 And drDB.Item("a_age") <= AgeData(9) Then
                            agetotal(9) = agetotal(9) + a_bal
                        ElseIf drDB.Item("a_age") >= AgeData(9) + 1 Then
                            agetotal(10) = agetotal(10) + a_bal
                        End If
                        '更新账龄数据,存入用于寄存的string变量
                        StringHolder2 = """<td>" & agetotal(0) & "</td><td>" & agetotal(1) & "</td><td>" & agetotal(2) & "</td><td>" & agetotal(3) & "</td><td>" & agetotal(4) & "</td><td>" & agetotal(5) & "</td><td>" & agetotal(6) & "</td><td>" & agetotal(7) & "</td><td>" & agetotal(8) & "</td><td>" & agetotal(9) & "</td><td>" & agetotal(10) & "</td></tr>"""
                End Select
                '更新pre_clientname
                pre_clientname = drDB.Item("clientname")
            End While
            '更新最后一行的数据
            AccountAge.Append(StringHolder1)
            AccountAge.Append(StringHolder2)
            drDB.Close()

            sSql = "select sum(a.jf_bala) as receive from f_voucher_entry a inner join f_voucher b on a.voucherid=b.autoinc where b.bookno=001 and b.fmonth=(select(select convert(char(4),year(getdate()),20))+(select convert(char(2),right('0'+CAST(month(getdate())as nvarchar(2)),2),20)))and (a.itemno like '1001%' or a.itemno like '1002%' or a.itemno like '1121%') and (a.custom11='7705CBB9-6B63-4EBB-AB88-2C278C2EBB82')"
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