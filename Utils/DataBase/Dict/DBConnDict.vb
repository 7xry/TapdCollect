Imports TapdCollect.Utils.DataBase.Model

Namespace Utils.DataBase.Dict
    Public Class DbConnDict
        Shared Property TapdCollect As DataCase = New DataCase(DbTypeEnum.Mysql, Nothing)
    End Class
End Namespace
