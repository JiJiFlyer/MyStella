Public Partial Class Changescale
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        common.FillDropDownList("select unit from bas_moneyunit order by number", "unit", "unit", salesUnit)
        common.FillDropDownList("select unit from bas_moneyunit order by number", "unit", "unit", bankrollUnit)
        common.FillDropDownList("select unit from bas_moneyunit order by number", "unit", "unit", ticketUnit)
    End Sub
    Public Function name(ByVal a As Char, ByVal b As Char)
        Dim db As DB
        Dim sSql As String = ""
        sSql = "update tickset set min='" & a & "',max='" & b & "' where autoinc='10FE9836-AE6B-47F1-AF19-05D1C1D1140C'"
        db = New DB
        Try
            db.ExecSql(sSql)
            db.Dispose()
            Return "0"
        Catch ex As Exception
            db.Dispose()
            Response.Write(ex.Message)
        End Try
    End Function
    Function runSql(ByVal sql As String)
        Dim db As DB
        Try
            db = New DB
            db.BeginTrans()
            db.ExecSql(sql)
            db.Commit()
            db.Dispose()
            Return ""
        Catch ex As Exception
            Try
                db.Rollback()
            Catch
            End Try
            db.Dispose()
            Throw ex
        End Try
    End Function
End Class
