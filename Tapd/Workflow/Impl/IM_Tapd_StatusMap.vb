Imports System.Collections.Immutable
Imports System.Data
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Story.Model
Imports TapdCollect.Tapd.Workflow.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Workflow.Impl
    Public Class IM_Tapd_StatusMap
        Public Shared Function Delete(ByVal workspace_id As String) As Boolean
            Dim StrBuff As New StringBuilder
            StrBuff.AppendLine($"Delete FROM tapd_statusmap where workspace_id='{workspace_id}' and collect_date='{IM_JsDate.GetNowStr("yyyy-MM-dd")}'")
            IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
            Dim data As IDataAccess = DbFactory.CreateConnection("MicroWork")
            Dim result = 1
            Try
                data.Open()
                data.BeginTran()
                data.ExecuteNonQuery(StrBuff.ToString())
                data.CommitTran()
            Catch ex As Exception
                data.RollBackTran()
                IM_Log.Showlog(ex.ToString(), MsgType.ErrorMsg)
                result = 0
            Finally
                data.Close()
            End Try
            If result = 1 Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Shared Function Sync(ByVal dic As Dictionary(Of String,String) , ByVal workspace_id As String, ByVal status_system As String) As Boolean
            Dim data As IDataAccess = DbFactory.CreateConnection("MicroWork")
            Dim result = 1
            Try
                data.Open()
                data.BeginTran()
                Dim StrBuff As New StringBuilder
                StrBuff.AppendLine($"INSERT INTO tapd_statusmap ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, workspace_id, status_system, status_key, status_value, collect_date ")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @workspace_id, @status_system, @status_key, @status_value, @collect_date ")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each item As KeyValuePair(Of String,String) In dic
                    IM_Log.Showlog($"Key:{item.Key},Value:{item.Value}",MsgType.DebugMsg)
                    Dim par = New QueryParameter() {New QueryParameter("@workspace_id", workspace_id, DbType.String),
                                                    New QueryParameter("@status_system", status_system, DbType.String),
                                                    New QueryParameter("@status_key", item.Key, DbType.String),
                                                    New QueryParameter("@status_value", item.Value, DbType.String),
                                                    New QueryParameter("@collect_date", IM_JsDate.GetNowStr("yyyy-MM-dd"), DbType.String)}
                    data.ExecuteNonQuery(StrBuff.ToString(), par)
                Next
                data.CommitTran()
            Catch ex As Exception
                data.RollBackTran()
                IM_Log.Showlog(ex.ToString(), MsgType.ErrorMsg)
                result = 0
            Finally
                data.Close()
            End Try
            If result = 1 Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class
End NameSpace