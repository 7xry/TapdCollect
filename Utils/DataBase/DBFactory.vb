Imports System.Net.Mime
Imports System.Reflection
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Dict
Imports TapdCollect.Utils.DataBase.Impl
Imports TapdCollect.Utils.DataBase.Model

Namespace Utils.DataBase
    Public Class DbFactory
        Private Shared ReadOnly SqlType As DbTypeEnum = DbTypeEnum.Mysql

        Public Shared Function GetConnection(Optional ByVal dbName As String = "") As String
            If dbName = String.Empty Then
                Return DbConnDict.TapdCollect.DataConn
            End If
            Dim t As Type = GetType(DbConnDict)
            Dim props As PropertyInfo() = t.GetProperties()
            For Each prop As PropertyInfo In props
                If dbName <> prop.Name Then
                    Continue For
                End If
                Dim db = TryCast(prop.GetValue(Nothing, Nothing), DataCase)
                Return db.DataConn
            Next
            Return DbConnDict.TapdCollect.DataConn
        End Function

        Public Shared Function CreateConnection(Optional ByVal dbName As String = "") As IDataAccess
            If dbName = String.Empty Then
                Return New MySqlImpl(DbConnDict.TapdCollect.DataConn)
            End If
            Dim t As Type = GetType(DbConnDict)
            Dim props As PropertyInfo() = t.GetProperties()
            For Each prop As PropertyInfo In props
                If dbName <> prop.Name Then
                    Continue For
                End If
                Dim db = TryCast(prop.GetValue(Nothing, Nothing), DataCase)
                Select Case db.DataType
                    Case DbTypeEnum.Mysql
                        Return New MySqlImpl(db.DataConn)
                    Case Else
                        Return New MySqlImpl(db.DataConn)
                End Select
            Next
            Return New MySqlImpl(DbConnDict.TapdCollect.DataConn)
        End Function
    End Class
End Namespace