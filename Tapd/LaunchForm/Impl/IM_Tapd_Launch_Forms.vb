
Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.LaunchForm.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.LaunchForm.Impl
    Public Class IM_Tapd_Launch_Forms
        Public Shared Function GetCount(ByVal workspace_id As String) As Nullable(Of Integer)
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.LaunchFormsCount,
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
            StrBuff.Append($"Delete FROM tapd_launch_forms where workspace_id='{workspace_id}' ")
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
                StrBuff.AppendLine($"INSERT INTO tapd_launch_forms ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, title, name, creator,")
                StrBuff.AppendLine($"       created, workspace_id, status, version_type, baseline,")
                StrBuff.AppendLine($"       release_model, roadmap_version, release_type, change_type, signed_by,")
                StrBuff.AppendLine($"       archived_by, cc, change_notifier, signed, archived,")
                StrBuff.AppendLine($"       signer_result, signer_comment, release_result, release_comment, test_path,")
                StrBuff.AppendLine($"       created_path, remark, participator, template_id, custom_field_one,")
                StrBuff.AppendLine($"       custom_field_two, custom_field_three, custom_field_four, custom_field_five, custom_field_six,")
                StrBuff.AppendLine($"       custom_field_seven, custom_field_eight, custom_field_9, custom_field_10, custom_field_11,")
                StrBuff.AppendLine($"       custom_field_12, custom_field_13, custom_field_14, custom_field_15, custom_field_16,")
                StrBuff.AppendLine($"       custom_field_17, custom_field_18, custom_field_19, custom_field_20, url, collect_date")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @title, @name, @creator,")
                StrBuff.AppendLine($"       @created, @workspace_id, @status, @version_type, @baseline,")
                StrBuff.AppendLine($"       @release_model, @roadmap_version, @release_type, @change_type, @signed_by,")
                StrBuff.AppendLine($"       @archived_by, @cc, @change_notifier, @signed, @archived,")
                StrBuff.AppendLine($"       @signer_result, @signer_comment, @release_result, @release_comment, @test_path,")
                StrBuff.AppendLine($"       @created_path, @remark, @participator, @template_id, @custom_field_one,")
                StrBuff.AppendLine($"       @custom_field_two, @custom_field_three, @custom_field_four, @custom_field_five, @custom_field_six,")
                StrBuff.AppendLine($"       @custom_field_seven, @custom_field_eight, @custom_field_9, @custom_field_10, @custom_field_11,")
                StrBuff.AppendLine($"       @custom_field_12, @custom_field_13, @custom_field_14, @custom_field_15, @custom_field_16,")
                StrBuff.AppendLine($"       @custom_field_17, @custom_field_18, @custom_field_19, @custom_field_20, @url, @collect_date")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim LaunchForm = JsonConvert.DeserializeObject(Of MD_Tapd_Launch_Forms)(TData("LaunchForm").ToString())
                    Dim par = New QueryParameter() {New QueryParameter("@id", LaunchForm.id, DbType.String),
                                                    New QueryParameter("@title", LaunchForm.title, DbType.String),
                                                    New QueryParameter("@name", LaunchForm.name, DbType.String),
                                                    New QueryParameter("@creator", LaunchForm.creator, DbType.String),
                                                    New QueryParameter("@created", LaunchForm.created, DbType.String),
                                                    New QueryParameter("@workspace_id", LaunchForm.workspace_id, DbType.String),
                                                    New QueryParameter("@status", LaunchForm.status, DbType.String),
                                                    New QueryParameter("@version_type", LaunchForm.version_type, DbType.String),
                                                    New QueryParameter("@baseline", LaunchForm.baseline, DbType.String),
                                                    New QueryParameter("@release_model", LaunchForm.release_model, DbType.String),
                                                    New QueryParameter("@roadmap_version", LaunchForm.roadmap_version, DbType.String),
                                                    New QueryParameter("@release_type", LaunchForm.release_type, DbType.String),
                                                    New QueryParameter("@change_type", LaunchForm.change_type, DbType.String),
                                                    New QueryParameter("@signed_by", LaunchForm.signed_by, DbType.String),
                                                    New QueryParameter("@archived_by", LaunchForm.archived_by, DbType.String),
                                                    New QueryParameter("@cc", LaunchForm.cc, DbType.String),
                                                    New QueryParameter("@change_notifier", LaunchForm.change_notifier, DbType.String),
                                                    New QueryParameter("@signed", LaunchForm.signed, DbType.String),
                                                    New QueryParameter("@archived", LaunchForm.archived, DbType.String),
                                                    New QueryParameter("@signer_result", LaunchForm.signer_result, DbType.String),
                                                    New QueryParameter("@signer_comment", LaunchForm.signer_comment, DbType.String),
                                                    New QueryParameter("@release_result", LaunchForm.release_result, DbType.String),
                                                    New QueryParameter("@release_comment", LaunchForm.release_comment, DbType.String),
                                                    New QueryParameter("@test_path", LaunchForm.test_path, DbType.String),
                                                    New QueryParameter("@created_path", LaunchForm.created_path, DbType.String),
                                                    New QueryParameter("@remark", LaunchForm.remark, DbType.String),
                                                    New QueryParameter("@participator", LaunchForm.participator, DbType.String),
                                                    New QueryParameter("@template_id", LaunchForm.template_id, DbType.String),
                                                    New QueryParameter("@custom_field_one", LaunchForm.custom_field_one, DbType.String),
                                                    New QueryParameter("@custom_field_two", LaunchForm.custom_field_two, DbType.String),
                                                    New QueryParameter("@custom_field_three", LaunchForm.custom_field_three, DbType.String),
                                                    New QueryParameter("@custom_field_four", LaunchForm.custom_field_four, DbType.String),
                                                    New QueryParameter("@custom_field_five", LaunchForm.custom_field_five, DbType.String),
                                                    New QueryParameter("@custom_field_six", LaunchForm.custom_field_six, DbType.String),
                                                    New QueryParameter("@custom_field_seven", LaunchForm.custom_field_seven, DbType.String),
                                                    New QueryParameter("@custom_field_eight", LaunchForm.custom_field_eight, DbType.String),
                                                    New QueryParameter("@custom_field_9", LaunchForm.custom_field_9, DbType.String),
                                                    New QueryParameter("@custom_field_10", LaunchForm.custom_field_10, DbType.String),
                                                    New QueryParameter("@custom_field_11", LaunchForm.custom_field_11, DbType.String),
                                                    New QueryParameter("@custom_field_12", LaunchForm.custom_field_12, DbType.String),
                                                    New QueryParameter("@custom_field_13", LaunchForm.custom_field_13, DbType.String),
                                                    New QueryParameter("@custom_field_14", LaunchForm.custom_field_14, DbType.String),
                                                    New QueryParameter("@custom_field_15", LaunchForm.custom_field_15, DbType.String),
                                                    New QueryParameter("@custom_field_16", LaunchForm.custom_field_16, DbType.String),
                                                    New QueryParameter("@custom_field_17", LaunchForm.custom_field_17, DbType.String),
                                                    New QueryParameter("@custom_field_18", LaunchForm.custom_field_18, DbType.String),
                                                    New QueryParameter("@custom_field_19", LaunchForm.custom_field_19, DbType.String),
                                                    New QueryParameter("@custom_field_20", LaunchForm.custom_field_20, DbType.String),
                                                    New QueryParameter("@url", $"{Cfg_Constant.Url}/{LaunchForm.workspace_id}/launch/launch_form/view/{LaunchForm.id}", DbType.String),
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