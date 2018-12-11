

Imports TapdCollect.Utils.DataBase.Dict

Namespace Utils.DataBase.Model
    Public Class DataCase
        Property DataType As DbTypeEnum

        Property DataConn As String

        Sub New(xDataType As DbTypeEnum, xDataConn As String)
            DataType = xDataType
            DataConn = xDataConn
        End Sub

        Sub New()
        End Sub
    End Class
End Namespace
