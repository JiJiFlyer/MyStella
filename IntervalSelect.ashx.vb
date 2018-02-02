Imports System.Web
Imports System.Web.Services

Public Class IntervalSelect
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        '接受前台传的num获得begin和end数组的标号，确定区间
        Dim num As Integer
        num = context.Request.Form("num")
        context.Response.Write(Selecttable(num))
    End Sub

    Function Selecttable(ByVal num As Integer) As StringBuilder

        Dim db As DB
        Dim drDB As SqlClient.SqlDataReader
        Dim sSql As String

        '建立数组存数据库中begin和end数据
        Dim Intervalbegin(20) As Integer
        Dim Intervalend(20) As Integer
        Dim a As Integer = 0
        sSql = "select * from dbo.bas_custominterval"
        db = New DB
        drDB = db.GetDataReader(sSql)
        While drDB.Read()
            If IsDBNull(drDB.Item("end")) Then
                Intervalbegin(a) = drDB.Item("begin")
                Intervalend(a) = 0
            Else
                Intervalbegin(a) = drDB.Item("begin")
                Intervalend(a) = drDB.Item("end")
            End If
            a = a + 1
        End While
        drDB.Close()

        '定义返回的html代码字符串
        '标签行填写：
        Dim customtable As New StringBuilder()
        'customtable.Append("<caption>自定义账龄提醒表</caption>
        '<tr>
        '    <th>客户</th>
        '    <th>余额方向</th>
        '    <th>余额</th>")
        'If Intervalend(num) = 0 Then
        '    customtable.Append("<th>" & Intervalbegin(num) & "天及以上</th>")
        'Else
        '    customtable.Append("<th>" & Intervalbegin(num) & "-" & Intervalend(num) & "天</th>")
        'End If
        'customtable.Append("</tr>")

        '数据处理计算及填写：
        '提醒表区间总额定义
        Dim agetotal_r As Double = 0
        '该客户之前a_bal的累加和
        Dim pre_t_bal As Double = 0
        '该客户当前凭证经修正的a_bal
        Dim a_bal As Double
        '同客户前a_bal累计和,若其+a_bal>t_bal,则该条凭证a_bal=t_bal-pre_t_bal
        Dim pre_clientname As String
        '前三项的数据寄存
        Dim StringHolder1 As String
        '提醒表数据寄存
        Dim StringHolder_R As String
        '第一个查询：初始化pre_clientname为表中第一位客户
        sSql = "select top 1 clientname from dbo.echart_accountage order by clientname,billdate desc"
        db = New DB
        drDB = db.GetDataReader(sSql)
        drDB.Read()
        pre_clientname = drDB.Item("clientname")
        drDB.Close()
        '第二个查询：先判断是否是同一客户
        '再判断a_bal是否是超过t_bal并做调整
        '最后判断经过调整的a_bal处于哪一账龄区间，加至相应的agetotal
        sSql = "select * from dbo.echart_accountage order by clientname,billdate desc"
        db = New DB
        drDB = db.GetDataReader(sSql)
        While drDB.Read
            '判断是否为新客户
            If drDB.Item("clientname") <> pre_clientname Then
                '若上一客户在提醒表范围内有金额，则填入表中
                If agetotal_r <> 0 Then
                    customtable.Append(StringHolder1)
                    customtable.Append(StringHolder_R)
                End If
                '若是新客户，重置前一客户统计的pre_t_bal和agetotal_r
                pre_t_bal = 0
                agetotal_r = 0
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
            StringHolder1 = "<tr><td>" & drDB.Item("clientname") & "</td><td>" & drDB.Item("bal_fx") & "</td><td>" & Format(drDB.Item("t_bal"), "0.00") & "</td>"
            If Intervalend(num) = 0 Then
                If drDB.Item("a_age") >= Intervalbegin(num) Then
                    agetotal_r = agetotal_r + a_bal
                End If
            Else
                If drDB.Item("a_age") >= Intervalbegin(num) And drDB.Item("a_age") <= Intervalend(num) Then
                    agetotal_r = agetotal_r + a_bal
                End If
            End If
            StringHolder_R = "<td>" & Format(agetotal_r, "0.00") & "</td></tr>"
            '更新pre_clientname
            pre_clientname = drDB.Item("clientname")
        End While
        '填写提醒表最后一行
        If agetotal_r <> 0 Then
            customtable.Append(StringHolder1)
            customtable.Append(StringHolder_R)
        End If
        drDB.Close()
        '若此时custom与填数据前无变化，显示无账龄信息
        If customtable.ToString() = "" Then
            customtable.Clear()
            customtable.Append("<td colspan=""4"">当前自定义区间内无账龄信息</td>")
        End If
        Return customtable
    End Function




    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class

'Imports System.Web
'Imports System.Web.Services

'Public Class IntervalSelect
'    Implements System.Web.IHttpHandler

'    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
'        Dim db As DB
'        Dim drDB As SqlClient.SqlDataReader
'        Dim sSql As String
'        '存数据库中begin和end数据
'        Dim Intervalbegin(20) As Integer
'        Dim Intervalend(20) As Integer
'        Dim a As Integer = 0
'        sSql = "select * from dbo.bas_custominterval"
'        db = New DB
'        drDB = db.GetDataReader(sSql)
'        While drDB.Read()
'            If IsDBNull(drDB.Item("end")) Then
'                Intervalbegin(a) = drDB.Item("begin")
'                Intervalend(a) = 0
'            Else
'                Intervalbegin(a) = drDB.Item("begin")
'                Intervalend(a) = drDB.Item("end")
'            End If
'            a = a + 1
'        End While
'        drDB.Close()
'        context.Response.ContentType = "text/plain"
'        '接受前台传的数组标号
'        Dim num As Integer
'        num = context.Request.Form.Get("num")
'        '定义返回的html代码字符串
'        Dim customtable As New StringBuilder()
'        customtable.Append("<caption>自定义账龄提醒表</caption>
'        <tr>
'            <th>客户</th>
'            <th>余额方向</th>
'            <th>余额</th>")
'        If Intervalend(num) = 0 Then
'            customtable.Append("<th>" & （Intervalbegin(num) + 1） & "天及以上</th>")
'        Else
'            customtable.Append("<th>" & Intervalbegin(num) + 1 & "-" & Intervalend(num) & "天</th>")
'        End If
'        customtable.Append("</tr>")

'        '提醒表区间总额定义
'        Dim agetotal_r As Double = 0
'        '该客户之前a_bal的累加和
'        Dim pre_t_bal As Double = 0
'        '该客户当前凭证经修正的a_bal
'        Dim a_bal As Double
'        '同客户前a_bal累计和,若其+a_bal>t_bal,则该条凭证a_bal=t_bal-pre_t_bal
'        Dim pre_clientname As String
'        '前三项的数据寄存
'        Dim StringHolder1 As String
'        '提醒表数据寄存
'        Dim StringHolder_R As String
'        '第一个查询：初始化pre_clientname为表中第一位客户
'        sSql = "select top 1 clientname from dbo.echart_accountage order by clientname,billdate desc"
'        db = New DB
'        drDB = db.GetDataReader(sSql)
'        drDB.Read()
'        pre_clientname = drDB.Item("clientname")
'        drDB.Close()
'        '第二个查询：先判断是否是同一客户
'        '再判断a_bal是否是超过t_bal并做调整
'        '最后判断经过调整的a_bal处于哪一账龄区间，加至相应的agetotal
'        sSql = "select * from dbo.echart_accountage order by clientname,billdate desc"
'        db = New DB
'        drDB = db.GetDataReader(sSql)
'        While drDB.Read
'            '判断是否为新客户
'            If drDB.Item("clientname") <> pre_clientname Then
'                '若上一客户在提醒表范围内有金额，则填入表中
'                If agetotal_r <> 0 Then
'                    customtable.Append(StringHolder1)
'                    customtable.Append(StringHolder_R)
'                End If
'                '若是新客户，重置前一客户统计的pre_t_bal和agetotal_r
'                pre_t_bal = 0
'                agetotal_r = 0
'            End If
'            '这个判断用于修正a_bal
'            If drDB.Item("a_bal") + pre_t_bal < drDB.Item("t_bal") Then
'                '无需修正的情况
'                a_bal = drDB.Item("a_bal")
'                pre_t_bal = pre_t_bal + a_bal
'            Else
'                '需修正的情况
'                a_bal = drDB.Item("t_bal") - pre_t_bal
'                pre_t_bal = pre_t_bal + a_bal
'            End If
'            '始终更新前三列,存入用于寄存的string变量
'            StringHolder1 = """<tr><td>" & drDB.Item("clientname") & "</td><td>" & drDB.Item("bal_fx") & "</td><td>" & drDB.Item("t_bal") & "</td>"""
'            If Intervalend(num) = 0 Then
'                If drDB.Item("a_age") >= Intervalbegin(num) + 1 Then
'                    agetotal_r = agetotal_r + a_bal
'                End If
'            Else
'                If drDB.Item("a_age") >= Intervalbegin(num) + 1 And drDB.Item("a_age") <= Intervalend(num) Then
'                    agetotal_r = agetotal_r + a_bal
'                End If
'            End If
'            StringHolder_R = """<td>" & agetotal_r & "</td></tr>"""
'            '更新pre_clientname
'            pre_clientname = drDB.Item("clientname")
'        End While
'        '填写提醒表最后一行
'        If agetotal_r <> 0 Then
'            customtable.Append(StringHolder1)
'            customtable.Append(StringHolder_R)
'        End If
'        drDB.Close()

'        context.Response.Write(customtable(num))

'    End Sub

'    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
'        Get
'            Return False
'        End Get
'    End Property

'End Class