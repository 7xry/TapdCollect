Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Iteration.Model
Imports TapdCollect.Tapd.TimeSheet.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Iteration.Impl
    Public Class IM_Tapd_Iterations
        Public Shared Function GetCount(ByVal workspace_id As String) As Nullable(Of Integer)
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.IterationsCount,
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
            StrBuff.Append($"Delete FROM tapd_iterations where workspace_id='{workspace_id}' ")
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
                StrBuff.AppendLine($"INSERT INTO tapd_iterations ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, name, workspace_id, startdate, ")
                StrBuff.AppendLine($"       enddate, status, release_id, description, creator, ")
                StrBuff.AppendLine($"       created, modified, completed, custom_field_1, custom_field_2, ")
                StrBuff.AppendLine($"       custom_field_3, custom_field_4, custom_field_5, custom_field_6, custom_field_7, ")
                StrBuff.AppendLine($"       custom_field_8, custom_field_9, custom_field_10, custom_field_11, custom_field_12, ")
                StrBuff.AppendLine($"       custom_field_13, custom_field_14, custom_field_15, custom_field_16, custom_field_17, ")
                StrBuff.AppendLine($"       custom_field_18, custom_field_19, custom_field_20, custom_field_21, custom_field_22, ")
                StrBuff.AppendLine($"       custom_field_23, custom_field_24, custom_field_25, custom_field_26, custom_field_27, ")
                StrBuff.AppendLine($"       custom_field_28, custom_field_29, custom_field_30, custom_field_31, custom_field_32, ")
                StrBuff.AppendLine($"       custom_field_33, custom_field_34, custom_field_35, custom_field_36, custom_field_37, ")
                StrBuff.AppendLine($"       custom_field_38, custom_field_39, custom_field_40, custom_field_41, custom_field_42, ")
                StrBuff.AppendLine($"       custom_field_43, custom_field_44, custom_field_45, custom_field_46, custom_field_47, ")
                StrBuff.AppendLine($"       custom_field_48, custom_field_49, custom_field_50, url, collect_date")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @name, @workspace_id, @startdate,")
                StrBuff.AppendLine($"       @enddate, @status, @release_id, @description, @creator,")
                StrBuff.AppendLine($"       @created, @modified, @completed, @custom_field_1, @custom_field_2,")
                StrBuff.AppendLine($"       @custom_field_3, @custom_field_4, @custom_field_5, @custom_field_6, @custom_field_7,")
                StrBuff.AppendLine($"       @custom_field_8, @custom_field_9, @custom_field_10, @custom_field_11, @custom_field_12,")
                StrBuff.AppendLine($"       @custom_field_13, @custom_field_14, @custom_field_15, @custom_field_16, @custom_field_17,")
                StrBuff.AppendLine($"       @custom_field_18, @custom_field_19, @custom_field_20, @custom_field_21, @custom_field_22,")
                StrBuff.AppendLine($"       @custom_field_23, @custom_field_24, @custom_field_25, @custom_field_26, @custom_field_27,")
                StrBuff.AppendLine($"       @custom_field_28, @custom_field_29, @custom_field_30, @custom_field_31, @custom_field_32,")
                StrBuff.AppendLine($"       @custom_field_33, @custom_field_34, @custom_field_35, @custom_field_36, @custom_field_37,")
                StrBuff.AppendLine($"       @custom_field_38, @custom_field_39, @custom_field_40, @custom_field_41, @custom_field_42,")
                StrBuff.AppendLine($"       @custom_field_43, @custom_field_44, @custom_field_45, @custom_field_46, @custom_field_47,")
                StrBuff.AppendLine($"       @custom_field_48, @custom_field_49, @custom_field_50, @url, @collect_date")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim Iteration As MD_Tapd_Iterations = JsonConvert.DeserializeObject(Of MD_Tapd_Iterations)(TData("Iteration").ToString())
                    Dim par = New QueryParameter() {New QueryParameter("@id", Iteration.id, DbType.String),
                                                    New QueryParameter("@name", Iteration.name, DbType.String),
                                                    New QueryParameter("@workspace_id", Iteration.workspace_id, DbType.String),
                                                    New QueryParameter("@startdate", Iteration.startdate, DbType.String),
                                                    New QueryParameter("@enddate", Iteration.enddate, DbType.String),
                                                    New QueryParameter("@status", Iteration.status, DbType.String),
                                                    New QueryParameter("@release_id", Iteration.release_id, DbType.String),
                                                    New QueryParameter("@description", Iteration.description, DbType.String),
                                                    New QueryParameter("@creator", Iteration.creator, DbType.String),
                                                    New QueryParameter("@created", Iteration.created, DbType.String),
                                                    New QueryParameter("@modified", Iteration.modified, DbType.String),
                                                    New QueryParameter("@completed", Iteration.completed, DbType.String),
                                                    New QueryParameter("@custom_field_1", Iteration.custom_field_1, DbType.String),
                                                    New QueryParameter("@custom_field_2", Iteration.custom_field_2, DbType.String),
                                                    New QueryParameter("@custom_field_3", Iteration.custom_field_3, DbType.String),
                                                    New QueryParameter("@custom_field_4", Iteration.custom_field_4, DbType.String),
                                                    New QueryParameter("@custom_field_5", Iteration.custom_field_5, DbType.String),
                                                    New QueryParameter("@custom_field_6", Iteration.custom_field_6, DbType.String),
                                                    New QueryParameter("@custom_field_7", Iteration.custom_field_7, DbType.String),
                                                    New QueryParameter("@custom_field_8", Iteration.custom_field_8, DbType.String),
                                                    New QueryParameter("@custom_field_9", Iteration.custom_field_9, DbType.String),
                                                    New QueryParameter("@custom_field_10", Iteration.custom_field_10, DbType.String),
                                                    New QueryParameter("@custom_field_11", Iteration.custom_field_11, DbType.String),
                                                    New QueryParameter("@custom_field_12", Iteration.custom_field_12, DbType.String),
                                                    New QueryParameter("@custom_field_13", Iteration.custom_field_13, DbType.String),
                                                    New QueryParameter("@custom_field_14", Iteration.custom_field_14, DbType.String),
                                                    New QueryParameter("@custom_field_15", Iteration.custom_field_15, DbType.String),
                                                    New QueryParameter("@custom_field_16", Iteration.custom_field_16, DbType.String),
                                                    New QueryParameter("@custom_field_17", Iteration.custom_field_17, DbType.String),
                                                    New QueryParameter("@custom_field_18", Iteration.custom_field_18, DbType.String),
                                                    New QueryParameter("@custom_field_19", Iteration.custom_field_19, DbType.String),
                                                    New QueryParameter("@custom_field_20", Iteration.custom_field_20, DbType.String),
                                                    New QueryParameter("@custom_field_21", Iteration.custom_field_21, DbType.String),
                                                    New QueryParameter("@custom_field_22", Iteration.custom_field_22, DbType.String),
                                                    New QueryParameter("@custom_field_23", Iteration.custom_field_23, DbType.String),
                                                    New QueryParameter("@custom_field_24", Iteration.custom_field_24, DbType.String),
                                                    New QueryParameter("@custom_field_25", Iteration.custom_field_25, DbType.String),
                                                    New QueryParameter("@custom_field_26", Iteration.custom_field_26, DbType.String),
                                                    New QueryParameter("@custom_field_27", Iteration.custom_field_27, DbType.String),
                                                    New QueryParameter("@custom_field_28", Iteration.custom_field_28, DbType.String),
                                                    New QueryParameter("@custom_field_29", Iteration.custom_field_29, DbType.String),
                                                    New QueryParameter("@custom_field_30", Iteration.custom_field_30, DbType.String),
                                                    New QueryParameter("@custom_field_31", Iteration.custom_field_31, DbType.String),
                                                    New QueryParameter("@custom_field_32", Iteration.custom_field_32, DbType.String),
                                                    New QueryParameter("@custom_field_33", Iteration.custom_field_33, DbType.String),
                                                    New QueryParameter("@custom_field_34", Iteration.custom_field_34, DbType.String),
                                                    New QueryParameter("@custom_field_35", Iteration.custom_field_35, DbType.String),
                                                    New QueryParameter("@custom_field_36", Iteration.custom_field_36, DbType.String),
                                                    New QueryParameter("@custom_field_37", Iteration.custom_field_37, DbType.String),
                                                    New QueryParameter("@custom_field_38", Iteration.custom_field_38, DbType.String),
                                                    New QueryParameter("@custom_field_39", Iteration.custom_field_39, DbType.String),
                                                    New QueryParameter("@custom_field_40", Iteration.custom_field_40, DbType.String),
                                                    New QueryParameter("@custom_field_41", Iteration.custom_field_41, DbType.String),
                                                    New QueryParameter("@custom_field_42", Iteration.custom_field_42, DbType.String),
                                                    New QueryParameter("@custom_field_43", Iteration.custom_field_43, DbType.String),
                                                    New QueryParameter("@custom_field_44", Iteration.custom_field_44, DbType.String),
                                                    New QueryParameter("@custom_field_45", Iteration.custom_field_45, DbType.String),
                                                    New QueryParameter("@custom_field_46", Iteration.custom_field_46, DbType.String),
                                                    New QueryParameter("@custom_field_47", Iteration.custom_field_47, DbType.String),
                                                    New QueryParameter("@custom_field_48", Iteration.custom_field_48, DbType.String),
                                                    New QueryParameter("@custom_field_49", Iteration.custom_field_49, DbType.String),
                                                    New QueryParameter("@custom_field_50", Iteration.custom_field_50, DbType.String),
                                                    New QueryParameter("@url", $"{Cfg_Constant.Url}/{Iteration.workspace_id}/prong/Iterations/view/{Iteration.id}", DbType.String),
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