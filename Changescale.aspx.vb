Public Partial Class Changescale
    Inherits Form

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        common.FillDropDownList("select unit from bas_moneyunit order by number", "unit", "unit", salesUnit)
        common.FillDropDownList("select unit from bas_moneyunit order by number", "unit", "unit", bankrollUnit)
        common.FillDropDownList("select unit from bas_moneyunit order by number", "unit", "unit", ticketUnit)
    End Sub
    Private Function runSql(ByVal sql As String) As String
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
