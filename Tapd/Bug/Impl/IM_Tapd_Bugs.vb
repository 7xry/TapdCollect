Imports System.Data
Imports System.Security.Policy
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Bug.Model
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Bug.Impl
    Public Class IM_Tapd_Bugs

        Public Shared Function GetCount(ByVal workspace_id As String) As Nullable(Of Integer)
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.BugsCount,
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
            StrBuff.Append($"Delete FROM tapd_bugs where workspace_id='{workspace_id}' ")
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
                StrBuff.AppendLine($"INSERT INTO tapd_bugs ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, title, description, priority, severity, ")
                StrBuff.AppendLine($"       module, status, reporter, deadline, created, ")
                StrBuff.AppendLine($"       bugtype, resolved, closed, modified, lastmodify, ")
                StrBuff.AppendLine($"       auditer, de, version_test, version_report, version_close, ")
                StrBuff.AppendLine($"       version_fix, baseline_find, baseline_join, baseline_close, baseline_test, ")
                StrBuff.AppendLine($"       sourcephase, te, current_owner, iteration_id, resolution, ")
                StrBuff.AppendLine($"       source, originphase, confirmer, milestone, participator, ")
                StrBuff.AppendLine($"       closer, platform, os, testtype, testphase, ")
                StrBuff.AppendLine($"       frequency, cc, regression_number, flows, feature, ")
                StrBuff.AppendLine($"       testmode, estimate, issue_id, created_from, in_progress_time, ")
                StrBuff.AppendLine($"       verify_time, reject_time, reopen_time, audit_time, suspend_time, ")
                StrBuff.AppendLine($"       due, begin, release_id, custom_field_one, custom_field_two, ")
                StrBuff.AppendLine($"       custom_field_three, custom_field_four, custom_field_five, custom_field_6, custom_field_7, ")
                StrBuff.AppendLine($"       custom_field_8, custom_field_9, custom_field_10, custom_field_11, custom_field_12, ")
                StrBuff.AppendLine($"       custom_field_13, custom_field_14, custom_field_15, custom_field_16, custom_field_17, ")
                StrBuff.AppendLine($"       custom_field_18, custom_field_19, custom_field_20, custom_field_21, custom_field_22, ")
                StrBuff.AppendLine($"       custom_field_23, custom_field_24, custom_field_25, custom_field_26, custom_field_27, ")
                StrBuff.AppendLine($"       custom_field_28, custom_field_29, custom_field_30, custom_field_31, custom_field_32, ")
                StrBuff.AppendLine($"       custom_field_33, custom_field_34, custom_field_35, custom_field_36, custom_field_37, ")
                StrBuff.AppendLine($"       custom_field_38, custom_field_39, custom_field_40, custom_field_41, custom_field_42, ")
                StrBuff.AppendLine($"       custom_field_43, custom_field_44, custom_field_45, custom_field_46, custom_field_47, ")
                StrBuff.AppendLine($"       custom_field_48, custom_field_49, custom_field_50, workspace_id, url, collect_date ")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @title, @description, @priority, @severity, ")
                StrBuff.AppendLine($"       @module, @status, @reporter, @deadline, @created, ")
                StrBuff.AppendLine($"       @bugtype, @resolved, @closed, @modified, @lastmodify, ")
                StrBuff.AppendLine($"       @auditer, @de, @version_test, @version_report, @version_close, ")
                StrBuff.AppendLine($"       @version_fix, @baseline_find, @baseline_join, @baseline_close, @baseline_test, ")
                StrBuff.AppendLine($"       @sourcephase, @te, @current_owner, @iteration_id, @resolution, ")
                StrBuff.AppendLine($"       @source, @originphase, @confirmer, @milestone, @participator, ")
                StrBuff.AppendLine($"       @closer, @platform, @os, @testtype, @testphase, ")
                StrBuff.AppendLine($"       @frequency, @cc, @regression_number, @flows, @feature, ")
                StrBuff.AppendLine($"       @testmode, @estimate, @issue_id, @created_from, @in_progress_time, ")
                StrBuff.AppendLine($"       @verify_time, @reject_time, @reopen_time, @audit_time, @suspend_time, ")
                StrBuff.AppendLine($"       @due, @begin, @release_id, @custom_field_one, @custom_field_two, ")
                StrBuff.AppendLine($"       @custom_field_three, @custom_field_four, @custom_field_five, @custom_field_6, @custom_field_7, ")
                StrBuff.AppendLine($"       @custom_field_8, @custom_field_9, @custom_field_10, @custom_field_11, @custom_field_12, ")
                StrBuff.AppendLine($"       @custom_field_13, @custom_field_14, @custom_field_15, @custom_field_16, @custom_field_17, ")
                StrBuff.AppendLine($"       @custom_field_18, @custom_field_19, @custom_field_20, @custom_field_21, @custom_field_22, ")
                StrBuff.AppendLine($"       @custom_field_23, @custom_field_24, @custom_field_25, @custom_field_26, @custom_field_27, ")
                StrBuff.AppendLine($"       @custom_field_28, @custom_field_29, @custom_field_30, @custom_field_31, @custom_field_32, ")
                StrBuff.AppendLine($"       @custom_field_33, @custom_field_34, @custom_field_35, @custom_field_36, @custom_field_37, ")
                StrBuff.AppendLine($"       @custom_field_38, @custom_field_39, @custom_field_40, @custom_field_41, @custom_field_42, ")
                StrBuff.AppendLine($"       @custom_field_43, @custom_field_44, @custom_field_45, @custom_field_46, @custom_field_47, ")
                StrBuff.AppendLine($"       @custom_field_48, @custom_field_49, @custom_field_50, @workspace_id, @url, @collect_date ")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim bug = JsonConvert.DeserializeObject(Of MD_Tapd_Bugs)(TData("Bug").ToString())
                    If bug.current_owner IsNot Nothing And bug.current_owner<>"" Then
                        If bug.current_owner.EndsWith(";")=False Then
                            bug.current_owner+=";"
                        End If
                    Else 
                        bug.current_owner="TapdSystem;"
                    End If
                    
                    Dim par = New QueryParameter() {New QueryParameter("@id", bug.id, DbType.String),
                                                    New QueryParameter("@title", bug.title, DbType.String),
                                                    New QueryParameter("@description", Nothing, DbType.String),
                                                    New QueryParameter("@priority", bug.priority, DbType.String),
                                                    New QueryParameter("@severity", bug.severity, DbType.String),
                                                    New QueryParameter("@module", bug.module, DbType.String),
                                                    New QueryParameter("@status", bug.status, DbType.String),
                                                    New QueryParameter("@reporter", bug.reporter, DbType.String),
                                                    New QueryParameter("@deadline", bug.deadline, DbType.String),
                                                    New QueryParameter("@created", bug.created, DbType.String),
                                                    New QueryParameter("@bugtype", bug.bugtype, DbType.String),
                                                    New QueryParameter("@resolved", bug.resolved, DbType.String),
                                                    New QueryParameter("@closed", bug.closed, DbType.String),
                                                    New QueryParameter("@modified", bug.modified, DbType.String),
                                                    New QueryParameter("@lastmodify", bug.lastmodify, DbType.String),
                                                    New QueryParameter("@auditer", bug.auditer, DbType.String),
                                                    New QueryParameter("@de", bug.de, DbType.String),
                                                    New QueryParameter("@version_test", bug.version_test, DbType.String),
                                                    New QueryParameter("@version_report", bug.version_report, DbType.String),
                                                    New QueryParameter("@version_close", bug.version_close, DbType.String),
                                                    New QueryParameter("@version_fix", bug.version_fix, DbType.String),
                                                    New QueryParameter("@baseline_find", bug.baseline_find, DbType.String),
                                                    New QueryParameter("@baseline_join", bug.baseline_join, DbType.String),
                                                    New QueryParameter("@baseline_close", bug.baseline_close, DbType.String),
                                                    New QueryParameter("@baseline_test", bug.baseline_test, DbType.String),
                                                    New QueryParameter("@sourcephase", bug.sourcephase, DbType.String),
                                                    New QueryParameter("@te", bug.te, DbType.String),
                                                    New QueryParameter("@current_owner", bug.current_owner, DbType.String),
                                                    New QueryParameter("@iteration_id", bug.iteration_id, DbType.String),
                                                    New QueryParameter("@resolution", bug.resolution, DbType.String),
                                                    New QueryParameter("@source", bug.source, DbType.String),
                                                    New QueryParameter("@originphase", bug.originphase, DbType.String),
                                                    New QueryParameter("@confirmer", bug.confirmer, DbType.String),
                                                    New QueryParameter("@milestone", bug.milestone, DbType.String),
                                                    New QueryParameter("@participator", bug.participator, DbType.String),
                                                    New QueryParameter("@closer", bug.closer, DbType.String),
                                                    New QueryParameter("@platform", bug.platform, DbType.String),
                                                    New QueryParameter("@os", bug.os, DbType.String),
                                                    New QueryParameter("@testtype", bug.testtype, DbType.String),
                                                    New QueryParameter("@testphase", bug.testphase, DbType.String),
                                                    New QueryParameter("@frequency", bug.frequency, DbType.String),
                                                    New QueryParameter("@cc", bug.cc, DbType.String),
                                                    New QueryParameter("@regression_number", bug.regression_number, DbType.String),
                                                    New QueryParameter("@flows", bug.flows, DbType.String),
                                                    New QueryParameter("@feature", bug.feature, DbType.String),
                                                    New QueryParameter("@testmode", bug.testmode, DbType.String),
                                                    New QueryParameter("@estimate", bug.estimate, DbType.String),
                                                    New QueryParameter("@issue_id", bug.issue_id, DbType.String),
                                                    New QueryParameter("@created_from", bug.created_from, DbType.String),
                                                    New QueryParameter("@in_progress_time", bug.in_progress_time, DbType.String),
                                                    New QueryParameter("@verify_time", bug.verify_time, DbType.String),
                                                    New QueryParameter("@reject_time", bug.reject_time, DbType.String),
                                                    New QueryParameter("@reopen_time", bug.reopen_time, DbType.String),
                                                    New QueryParameter("@audit_time", bug.audit_time, DbType.String),
                                                    New QueryParameter("@suspend_time", bug.suspend_time, DbType.String),
                                                    New QueryParameter("@due", bug.due, DbType.String),
                                                    New QueryParameter("@begin", bug.begin, DbType.String),
                                                    New QueryParameter("@release_id", bug.release_id, DbType.String),
                                                    New QueryParameter("@custom_field_one", bug.custom_field_one, DbType.String),
                                                    New QueryParameter("@custom_field_two", bug.custom_field_two, DbType.String),
                                                    New QueryParameter("@custom_field_three", bug.custom_field_three, DbType.String),
                                                    New QueryParameter("@custom_field_four", bug.custom_field_four, DbType.String),
                                                    New QueryParameter("@custom_field_five", bug.custom_field_five, DbType.String),
                                                    New QueryParameter("@custom_field_6", bug.custom_field_6, DbType.String),
                                                    New QueryParameter("@custom_field_7", bug.custom_field_7, DbType.String),
                                                    New QueryParameter("@custom_field_8", bug.custom_field_8, DbType.String),
                                                    New QueryParameter("@custom_field_9", bug.custom_field_9, DbType.String),
                                                    New QueryParameter("@custom_field_10", bug.custom_field_10, DbType.String),
                                                    New QueryParameter("@custom_field_11", bug.custom_field_11, DbType.String),
                                                    New QueryParameter("@custom_field_12", bug.custom_field_12, DbType.String),
                                                    New QueryParameter("@custom_field_13", bug.custom_field_13, DbType.String),
                                                    New QueryParameter("@custom_field_14", bug.custom_field_14, DbType.String),
                                                    New QueryParameter("@custom_field_15", bug.custom_field_15, DbType.String),
                                                    New QueryParameter("@custom_field_16", bug.custom_field_16, DbType.String),
                                                    New QueryParameter("@custom_field_17", bug.custom_field_17, DbType.String),
                                                    New QueryParameter("@custom_field_18", bug.custom_field_18, DbType.String),
                                                    New QueryParameter("@custom_field_19", bug.custom_field_19, DbType.String),
                                                    New QueryParameter("@custom_field_20", bug.custom_field_20, DbType.String),
                                                    New QueryParameter("@custom_field_21", bug.custom_field_21, DbType.String),
                                                    New QueryParameter("@custom_field_22", bug.custom_field_22, DbType.String),
                                                    New QueryParameter("@custom_field_23", bug.custom_field_23, DbType.String),
                                                    New QueryParameter("@custom_field_24", bug.custom_field_24, DbType.String),
                                                    New QueryParameter("@custom_field_25", bug.custom_field_25, DbType.String),
                                                    New QueryParameter("@custom_field_26", bug.custom_field_26, DbType.String),
                                                    New QueryParameter("@custom_field_27", bug.custom_field_27, DbType.String),
                                                    New QueryParameter("@custom_field_28", bug.custom_field_28, DbType.String),
                                                    New QueryParameter("@custom_field_29", bug.custom_field_29, DbType.String),
                                                    New QueryParameter("@custom_field_30", bug.custom_field_30, DbType.String),
                                                    New QueryParameter("@custom_field_31", bug.custom_field_31, DbType.String),
                                                    New QueryParameter("@custom_field_32", bug.custom_field_32, DbType.String),
                                                    New QueryParameter("@custom_field_33", bug.custom_field_33, DbType.String),
                                                    New QueryParameter("@custom_field_34", bug.custom_field_34, DbType.String),
                                                    New QueryParameter("@custom_field_35", bug.custom_field_35, DbType.String),
                                                    New QueryParameter("@custom_field_36", bug.custom_field_36, DbType.String),
                                                    New QueryParameter("@custom_field_37", bug.custom_field_37, DbType.String),
                                                    New QueryParameter("@custom_field_38", bug.custom_field_38, DbType.String),
                                                    New QueryParameter("@custom_field_39", bug.custom_field_39, DbType.String),
                                                    New QueryParameter("@custom_field_40", bug.custom_field_40, DbType.String),
                                                    New QueryParameter("@custom_field_41", bug.custom_field_41, DbType.String),
                                                    New QueryParameter("@custom_field_42", bug.custom_field_42, DbType.String),
                                                    New QueryParameter("@custom_field_43", bug.custom_field_43, DbType.String),
                                                    New QueryParameter("@custom_field_44", bug.custom_field_44, DbType.String),
                                                    New QueryParameter("@custom_field_45", bug.custom_field_45, DbType.String),
                                                    New QueryParameter("@custom_field_46", bug.custom_field_46, DbType.String),
                                                    New QueryParameter("@custom_field_47", bug.custom_field_47, DbType.String),
                                                    New QueryParameter("@custom_field_48", bug.custom_field_48, DbType.String),
                                                    New QueryParameter("@custom_field_49", bug.custom_field_49, DbType.String),
                                                    New QueryParameter("@custom_field_50", bug.custom_field_50, DbType.String),
                                                    New QueryParameter("@workspace_id", bug.workspace_id, DbType.String),
                                                    New QueryParameter("@url", $"{Cfg_Constant.Url}/{bug.workspace_id}/bugtrace/bugs/view/{bug.id}", DbType.String),
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