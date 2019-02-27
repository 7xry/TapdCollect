
Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Task.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Task.Impl
    Public Class IM_Tapd_Tasks
        Public Shared Function GetCount(ByVal workspace_id As String) As Nullable(Of Integer)
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.TasksCount,
                    .ParmStr=$"workspace_id={workspace_id}"
                    }
            Dim tapd As MD_Tapd=IM_Req.DoGet(ReqParm)
            If tapd Is Nothing Then
                IM_Log.Showlog($"接口 [ {ReqParm.BaseUrl}/{ReqParm.RequestUrl}?{ReqParm.ParmStr} ] 请求返回异常", MsgType.ErrorMsg)
                Return Nothing
            End If
            Return CInt(tapd.data("count").ToString())
        End Function
        Public Shared Function Delete(workspace_id As String) As Boolean
            Dim StrBuff As New StringBuilder
            StrBuff.Append($"Delete FROM tapd_tasks where workspace_id='{workspace_id}' ")
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
                StrBuff.AppendLine($"INSERT INTO tapd_tasks ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, name, description, workspace_id, creator,")
                StrBuff.AppendLine($"       created, modified, status, owner, cc,")
                StrBuff.AppendLine($"       begin, due, story_id, iteration_id, priority,")
                StrBuff.AppendLine($"       progress, completed, effort_completed, effort_total, exceed,")
                StrBuff.AppendLine($"       remain, effort, custom_field_one, custom_field_two, custom_field_three,")
                StrBuff.AppendLine($"       custom_field_four, custom_field_five, custom_field_six, custom_field_seven, custom_field_eight,")
                StrBuff.AppendLine($"       url, collect_date")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @name, @description, @workspace_id, @creator,")
                StrBuff.AppendLine($"       @created, @modified, @status, @owner, @cc,")
                StrBuff.AppendLine($"       @begin, @due, @story_id, @iteration_id, @priority,")
                StrBuff.AppendLine($"       @progress, @completed, @effort_completed, @effort_total, @exceed,")
                StrBuff.AppendLine($"       @remain, @effort, @custom_field_one, @custom_field_two, @custom_field_three,")
                StrBuff.AppendLine($"       @custom_field_four, @custom_field_five, @custom_field_six, @custom_field_seven, @custom_field_eight,")
                StrBuff.AppendLine($"       @url, @collect_date")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim task = JsonConvert.DeserializeObject(Of MD_Tapd_Tasks)(TData("Task").ToString())
                    If task.owner IsNot Nothing And task.owner<>"" Then
                        If task.owner.EndsWith(";")=False Then
                            task.owner+=";"
                        End If
                    Else 
                        task.owner="TapdSystem;"
                    End If
                    Dim par = New QueryParameter() {New QueryParameter("@id", task.id, DbType.String),
                                                    New QueryParameter("@name", task.name, DbType.String),
                                                    New QueryParameter("@description", Nothing, DbType.String),
                                                    New QueryParameter("@workspace_id", task.workspace_id, DbType.String),
                                                    New QueryParameter("@creator", task.creator, DbType.String),
                                                    New QueryParameter("@created", task.created, DbType.String),
                                                    New QueryParameter("@modified", task.modified, DbType.String),
                                                    New QueryParameter("@status", task.status, DbType.String),
                                                    New QueryParameter("@owner", task.owner, DbType.String),
                                                    New QueryParameter("@cc", task.cc, DbType.String),
                                                    New QueryParameter("@begin", task.begin, DbType.String),
                                                    New QueryParameter("@due", task.due, DbType.String),
                                                    New QueryParameter("@story_id", task.story_id, DbType.String),
                                                    New QueryParameter("@iteration_id", task.iteration_id, DbType.String),
                                                    New QueryParameter("@priority", task.priority, DbType.String),
                                                    New QueryParameter("@progress", task.progress, DbType.String),
                                                    New QueryParameter("@completed", task.completed, DbType.String),
                                                    New QueryParameter("@effort_completed", task.effort_completed, DbType.String),
                                                    New QueryParameter("@effort_total", task.effort_total, DbType.String),
                                                    New QueryParameter("@exceed", task.exceed, DbType.String),
                                                    New QueryParameter("@remain", task.remain, DbType.String),
                                                    New QueryParameter("@effort", task.effort, DbType.String),
                                                    New QueryParameter("@custom_field_one", task.custom_field_one, DbType.String),
                                                    New QueryParameter("@custom_field_two", task.custom_field_two, DbType.String),
                                                    New QueryParameter("@custom_field_three", task.custom_field_three, DbType.String),
                                                    New QueryParameter("@custom_field_four", task.custom_field_four, DbType.String),
                                                    New QueryParameter("@custom_field_five", task.custom_field_five, DbType.String),
                                                    New QueryParameter("@custom_field_six", task.custom_field_six, DbType.String),
                                                    New QueryParameter("@custom_field_seven", task.custom_field_seven, DbType.String),
                                                    New QueryParameter("@custom_field_eight", task.custom_field_eight, DbType.String),
                                                    New QueryParameter("@url",  $"{Cfg_Constant.Url}/{task.workspace_id}/prong/tasks/view/{task.id}", DbType.String),
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
End Namespace