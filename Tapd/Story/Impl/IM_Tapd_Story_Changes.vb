Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Bug.Model
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Story.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Story.Impl
    Public Class IM_Tapd_Story_Changes
        
        Public Shared Function GetCount(ByVal workspace_id As String) As Integer
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.StoriesChangesCount,
                    .ParmStr=$"workspace_id={workspace_id}"
                    }
            Dim tapd As MD_Tapd=IM_Req.DoGet(ReqParm)
            Return CInt(tapd.data("count").ToString())
        End Function

        Public Shared Function Delete(workspace_id As String) As Boolean
            Dim StrBuff As New StringBuilder
            StrBuff.AppendLine($"Delete FROM tapd_story_changes where workspace_id='{workspace_id}' and collect_date='{IM_JsDate.GetNowStr("yyyy-MM-dd")}'")
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
                StrBuff.AppendLine($"INSERT INTO tapd_story_changes ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, workspace_id, creator, created,")
                StrBuff.AppendLine($"       change_summary, comment, changes, entity_type, story_id, url, collect_date")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @workspace_id, @creator, @created,")
                StrBuff.AppendLine($"       @change_summary, @comment, @changes, @entity_type, @story_id, @url, @collect_date")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim storychange = JsonConvert.DeserializeObject(Of MD_Tapd_Story_Changes)(TData("WorkitemChange").ToString())
                    Dim par = New QueryParameter() {New QueryParameter("@id", storychange.id, DbType.String),
                                                    New QueryParameter("@workspace_id", storychange.workspace_id, DbType.String),
                                                    New QueryParameter("@creator", storychange.creator, DbType.String),
                                                    New QueryParameter("@created", storychange.created, DbType.String),
                                                    New QueryParameter("@change_summary", storychange.change_summary, DbType.String),
                                                    New QueryParameter("@comment", storychange.comment, DbType.String),
                                                    New QueryParameter("@changes", Nothing, DbType.String),
                                                    New QueryParameter("@entity_type", storychange.entity_type, DbType.String),
                                                    New QueryParameter("@story_id", storychange.story_id, DbType.String),
                                                    New QueryParameter("@url", $"{Cfg_Constant.Url}/{storychange.workspace_id}/prong/stories/view/{storychange.story_id}", DbType.String),
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