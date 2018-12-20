
Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Project.Model
Imports TapdCollect.Tapd.Relation.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Relation.Impl
    Public Class IM_Tapd_Relations
        Public Shared Function Delete(ByVal workspace_id As String) As Boolean
            Dim StrBuff As New StringBuilder
            StrBuff.Append($"Delete FROM tapd_relations where workspace_id='{workspace_id}' ")
            If Cfg_Constant.IsKeepHistory=True Then
                StrBuff.Append($" And collect_date='{IM_JsDate.GetNowStr("yyyy-MM-dd")}'")
            End If
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
        Public Shared Function Sync(tapd As MD_Tapd) As Boolean
            Dim data As IDataAccess = DbFactory.CreateConnection("MicroWork")
            Dim result = 1
            Try
                data.Open()
                data.BeginTran()
                Dim StrBuff As New StringBuilder
                StrBuff.AppendLine($"INSERT INTO tapd_relations ")
                StrBuff.AppendLine($"   (sysid, id, workspace_id, source_type, source_id, target_type, target_id, modified, created, url, collect_date)")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (uuid(), @id, @workspace_id, @source_type, @source_id, @target_type, @target_id, @modified, @created, @url, @collect_date)")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim Relation = JsonConvert.DeserializeObject(Of MD_Tapd_Relations)(TData("Relation").ToString())
                    Dim par = New QueryParameter() {New QueryParameter("@id", Relation.id, DbType.String),
                                                    New QueryParameter("@workspace_id", Relation.workspace_id, DbType.String),
                                                    New QueryParameter("@source_type", Relation.source_type, DbType.String),
                                                    New QueryParameter("@source_id", Relation.source_id, DbType.String),
                                                    New QueryParameter("@target_type", Relation.target_type, DbType.String),
                                                    New QueryParameter("@target_id", Relation.target_id, DbType.String),
                                                    New QueryParameter("@modified", Relation.modified, DbType.String),
                                                    New QueryParameter("@created", Relation.created, DbType.String),
                                                    New QueryParameter("@url", Nothing, DbType.String),
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