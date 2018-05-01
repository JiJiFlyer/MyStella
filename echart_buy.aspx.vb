Public Class echart_buy

    Inherits System.Web.UI.Page
    Public errorshow1 As String
    Public errorshow2 As String
    Public SelectBook As New StringBuilder()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As DB
        Dim drDB As SqlClient.SqlDataReader
        Dim sSql As String
        db = New DB
        Try
            '下拉框编写,用于让用户选择不同公司的图表（前提是拥有相应权限，用户身份读取之后做，目前读取默认全权限的testboy）
            sSql = "select booksno,booksname from bas_grant,f_books where companyid=autoinc and userid='testboy'"
            drDB = db.GetDataReader(sSql)
            While drDB.Read()
                SelectBook.Append("<option value=""" & drDB.Item("booksno") & """>" & drDB.Item("booksname") & "</option>")
            End While
            drDB.Close()

        Catch ex As Exception
            DB.Close(db, drDB)
            errorshow1 = ex.Message
            errorshow2 = ex.StackTrace
        End Try
    End Sub

End Class