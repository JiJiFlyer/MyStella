Public Class DataSheet
    Inherits System.Web.UI.Page
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
                AgeLabel_R.Append("<th>" & （abegin + 1） & "天及以上</th>")
            Else
                aend = drDB.Item("end")
                AgeLabel_R.Append("<th>" & abegin & "-" & aend & "天</th>")
            End If
            drDB.Close()
            '账龄表标签行
            AgeLabel.Append("<th>1-" & AgeData(0) & "天</th>")
            For i = 1 To agenumber - 1
                AgeLabel.Append("<th>" & (AgeData(i - 1) + 1) & "-" & AgeData(i) & "天</th>")
            Next i
            AgeLabel.Append("<th>" & （AgeData(agenumber - 1) + 1) & "天及以上</th>")

            '提醒表区间总额定义
            Dim agetotal_r As Double = 0
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
            '提醒表数据寄存
            Dim StringHolder_R As String
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
                    '若上一客户在提醒表范围内有金额，则填入表中
                    If agetotal_r <> 0 Then
                        AccountAge_R.Append(StringHolder1)
                        AccountAge_R.Append(StringHolder_R)
                    End If
                    '若是新客户，重置前一客户统计的pre_t_bal和agetotal和agetotal_r
                    For i = 0 To 10
                        agetotal(i) = 0
                    Next i
                    pre_t_bal = 0
                    agetotal_r = 0
                    '将上一客户的数据填入账龄表
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
                If aend = 0 Then
                    If drDB.Item("a_age") >= abegin + 1 Then
                        agetotal_r = agetotal_r + a_bal
                    End If
                Else
                    If drDB.Item("a_age") >= abegin + 1 And drDB.Item("a_age") <= aend Then
                        agetotal_r = agetotal_r + a_bal
                    End If
                End If
                StringHolder_R = """<td>" & agetotal_r & "</td></tr>"""
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
            '填写提醒表最后一行
            If agetotal_r <> 0 Then
                AccountAge_R.Append(StringHolder1)
                AccountAge_R.Append(StringHolder_R)
            End If
            '填写账龄表最后一行
            AccountAge.Append(StringHolder1)
            AccountAge.Append(StringHolder2)
            drDB.Close()

        Catch ex As Exception
            DB.Close(db, drDB)
        End Try
    End Sub

End Class