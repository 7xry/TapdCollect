Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Modules.Model
Imports TapdCollect.Tapd.TimeSheet.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.TimeSheet.Impl
    Public Class IM_Tapd_TimeSheets
        Public Shared Function GetCount(ByVal workspace_id As String) As Nullable(Of Integer)
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.TimeSheetsCount,
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
            StrBuff.Append($"Delete FROM tapd_timesheets where workspace_id='{workspace_id}' ")
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
                StrBuff.AppendLine($"INSERT INTO tapd_timesheets ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, entity_type, entity_id, timespent, spentdate, owner, created, workspace_id, memo, collect_date ")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @entity_type, @entity_id, @timespent, @spentdate, @owner, @created, @workspace_id, @memo, @collect_date ")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim TimeSheet As MD_Tapd_TimeSheets = JsonConvert.DeserializeObject(Of MD_Tapd_TimeSheets)(TData("Timesheet").ToString())
                    Dim par = New QueryParameter() {New QueryParameter("@id", TimeSheet.id, DbType.String),
                                                    New QueryParameter("@entity_type", TimeSheet.entity_type, DbType.String),
                                                    New QueryParameter("@entity_id", TimeSheet.entity_id, DbType.String),
                                                    New QueryParameter("@timespent", TimeSheet.timespent, DbType.String),
                                                    New QueryParameter("@spentdate", TimeSheet.spentdate, DbType.String),
                                                    New QueryParameter("@owner", TimeSheet.owner, DbType.String),
                                                    New QueryParameter("@created", TimeSheet.created, DbType.String),
                                                    New QueryParameter("@workspace_id", TimeSheet.workspace_id, DbType.String),
                                                    New QueryParameter("@memo", TimeSheet.memo, DbType.String),
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