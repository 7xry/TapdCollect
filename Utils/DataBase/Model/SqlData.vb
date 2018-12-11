

Namespace Utils.DataBase.Model
    Public Class SqlData
        Property SqlSting As String
        Property SqlParm As QueryParameter()

        Sub New (ByVal sqlStr As String,par As QueryParameter())
            SqlSting=sqlStr
            SqlParm=par
        End Sub
    End Class
End Namespace
