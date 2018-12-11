Imports System.Data
Imports System.Text
Imports MySql.Data.MySqlClient
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl
Imports Microsoft.VisualBasic
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Utils.DataBase.Impl
    Public Class MySqlImpl
        Implements IDataAccess
        Implements IDisposable
        '数据库连接属性
        Property Conn As MySqlConnection
        '数据库事务属性
        Property Tran As MySqlTransaction

        Public Sub Dispose() Implements IDisposable.Dispose
            If Conn IsNot Nothing Then
                Conn.Dispose()
                Conn = Nothing
            End If
            If Tran IsNot Nothing Then
                Tran.Dispose()
                Tran = Nothing
            End If
        End Sub

        '构造一个实例，并赋值连接属性
        Sub New(conStr As String)
            Try
                Conn = New MySqlConnection(conStr)
            Catch ex As Exception
                Throw New Exception()
            End Try
        End Sub

#Region "IDataAccess 成员"
        '打开连接
        Public Sub Open() Implements IDataAccess.Open
            If Conn Is Nothing OrElse Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
        End Sub

        '关闭连接
        Public Sub Close() Implements IDataAccess.Close
            If Conn IsNot Nothing OrElse Tran IsNot Nothing Then
                Conn.Close()
            End If
        End Sub

        '开始事务
        Public Sub BeginTran() Implements IDataAccess.BeginTran
            Tran = Conn.BeginTransaction()
        End Sub

        '提交事务
        Public Sub CommitTran() Implements IDataAccess.CommitTran
            Tran.Commit()
        End Sub

        '回滚事务
        Public Sub RollBackTran() Implements IDataAccess.RollBackTran
            Tran.Rollback()
        End Sub

        '执行增删改
        Public Function ExecuteNonQuery(sql As String, ByVal ParamArray para() As QueryParameter) As Integer Implements IDataAccess.ExecuteNonQuery
            Dim cmd As New MySqlCommand
            PrepareCommand(cmd, CommandType.Text, sql, para)
            ExecuteNonQuery = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return ExecuteNonQuery
        End Function

        '返回单个值
        Public Function ExecuteScalar(sql As String, ByVal ParamArray para() As QueryParameter) As Object Implements IDataAccess.ExecuteScalar
            Dim cmd As New MySqlCommand
            PrepareCommand(cmd, CommandType.Text, sql, para)
            ExecuteScalar = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return ExecuteScalar
        End Function

        '返回查询结果表格
        Public Function GetTable(sql As String, ByVal ParamArray para() As QueryParameter) As DataTable Implements IDataAccess.GetTable
            Dim cmd As New MySqlCommand
            PrepareCommand(cmd, CommandType.Text, sql, para)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
            cmd.Parameters.Clear()
            Return dt
        End Function

        '返回一个DataReader
        Public Function GetReader(sql As String, ByVal ParamArray para() As QueryParameter) As IDataReader Implements IDataAccess.GetReader
            Dim cmd As New MySqlCommand
            PrepareCommand(cmd, CommandType.Text, sql, para)
            GetReader = cmd.ExecuteReader()
            cmd.Parameters.Clear()
            Return GetReader
        End Function

        '准备Command
        Private Sub PrepareCommand(cmd As MySqlCommand, commandType As CommandType, commandText As String, commandQueryParameter As QueryParameter())
            Dim paraStr As New StringBuilder
            cmd.CommandType = commandType
            cmd.CommandText = commandText
            cmd.Connection = Conn
            cmd.Transaction = Tran
            cmd.CommandTimeout = 60
            If commandQueryParameter IsNot Nothing AndAlso commandQueryParameter.Length > 0 Then
                For i = 0 To commandQueryParameter.Length - 1
                    '这里讲Add改为AddWithValue原因是VS提示该方法已过时
                    cmd.Parameters.AddWithValue(commandQueryParameter(i).Name, commandQueryParameter(i).Value)
                    paraStr.AppendLine($"Name：{commandQueryParameter(i).Name}，Value：{commandQueryParameter(i).Value}")
                Next
            End If
            If paraStr.ToString <> String.Empty Then
                IM_Log.Showlog($"SQL参数输出：{IM_AppPath.NewLine()}{paraStr.ToString()}", MsgType.DebugMsg)
            End If
        End Sub

#End Region
    End Class
End Namespace