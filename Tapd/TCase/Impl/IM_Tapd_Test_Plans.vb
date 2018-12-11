
Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Story.Model
Imports TapdCollect.Tapd.TCase.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.TCase.Impl
    Public Class IM_Tapd_Test_Plans
        Public Shared Function GetCount(ByVal workspace_id As String) As Integer
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.TestPlansCount,
                    .ParmStr=$"workspace_id={workspace_id}"
                    }
            Dim tapd As MD_Tapd=IM_Req.DoGet(ReqParm)
            Return CInt(tapd.data("count").ToString())
        End Function
        Public Shared Function Delete(workspace_id As String) As Boolean
            Dim StrBuff As New StringBuilder
            StrBuff.AppendLine($"Delete FROM tapd_test_plans where workspace_id='{workspace_id}' and collect_date='{IM_JsDate.GetNowStr("yyyy-MM-dd")}'")
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
                StrBuff.AppendLine($"INSERT INTO tapd_test_plans ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, workspace_id, name, description,")
                StrBuff.AppendLine($"       version, owner, status, type, start_date,")
                StrBuff.AppendLine($"       end_date, creator, created, modified, modifier,")
                StrBuff.AppendLine($"       custom_field_1, custom_field_2, custom_field_3, custom_field_4, custom_field_5,")
                StrBuff.AppendLine($"       custom_field_6, custom_field_7, custom_field_8, custom_field_9, custom_field_10,")
                StrBuff.AppendLine($"       custom_field_11, custom_field_12, custom_field_13, custom_field_14, custom_field_15,")
                StrBuff.AppendLine($"       custom_field_16, custom_field_17, custom_field_18, custom_field_19, custom_field_20,")
                StrBuff.AppendLine($"       custom_field_21, custom_field_22, custom_field_23, custom_field_24, custom_field_25,")
                StrBuff.AppendLine($"       custom_field_26, custom_field_27, custom_field_28, custom_field_29, custom_field_30,")
                StrBuff.AppendLine($"       custom_field_31, custom_field_32, custom_field_33, custom_field_34, custom_field_35,")
                StrBuff.AppendLine($"       custom_field_36, custom_field_37, custom_field_38, custom_field_39, custom_field_40,")
                StrBuff.AppendLine($"       custom_field_41, custom_field_42, custom_field_43, custom_field_44, custom_field_45,")
                StrBuff.AppendLine($"       custom_field_46, custom_field_47, custom_field_48, custom_field_49, custom_field_50,")
                StrBuff.AppendLine($"       url, collect_date")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @workspace_id, @name, @description,")
                StrBuff.AppendLine($"       @version, @owner, @status, @type, @start_date,")
                StrBuff.AppendLine($"       @end_date, @creator, @created, @modified, @modifier,")
                StrBuff.AppendLine($"       @custom_field_1, @custom_field_2, @custom_field_3, @custom_field_4, @custom_field_5,")
                StrBuff.AppendLine($"       @custom_field_6, @custom_field_7, @custom_field_8, @custom_field_9, @custom_field_10,")
                StrBuff.AppendLine($"       @custom_field_11, @custom_field_12, @custom_field_13, @custom_field_14, @custom_field_15,")
                StrBuff.AppendLine($"       @custom_field_16, @custom_field_17, @custom_field_18, @custom_field_19, @custom_field_20,")
                StrBuff.AppendLine($"       @custom_field_21, @custom_field_22, @custom_field_23, @custom_field_24, @custom_field_25,")
                StrBuff.AppendLine($"       @custom_field_26, @custom_field_27, @custom_field_28, @custom_field_29, @custom_field_30,")
                StrBuff.AppendLine($"       @custom_field_31, @custom_field_32, @custom_field_33, @custom_field_34, @custom_field_35,")
                StrBuff.AppendLine($"       @custom_field_36, @custom_field_37, @custom_field_38, @custom_field_39, @custom_field_40,")
                StrBuff.AppendLine($"       @custom_field_41, @custom_field_42, @custom_field_43, @custom_field_44, @custom_field_45,")
                StrBuff.AppendLine($"       @custom_field_46, @custom_field_47, @custom_field_48, @custom_field_49, @custom_field_50,")
                StrBuff.AppendLine($"       @url, @collect_date")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim testplan = JsonConvert.DeserializeObject(Of MD_Tapd_Test_Plans)(TData("TestPlan").ToString())
                    Dim par = New QueryParameter() {New QueryParameter("@id", testplan.id, DbType.String),
                                                    New QueryParameter("@workspace_id", testplan.workspace_id, DbType.String),
                                                    New QueryParameter("@name", testplan.name, DbType.String),
                                                    New QueryParameter("@description", Nothing, DbType.String),
                                                    New QueryParameter("@version", testplan.version, DbType.String),
                                                    New QueryParameter("@owner", testplan.owner, DbType.String),
                                                    New QueryParameter("@status", testplan.status, DbType.String),
                                                    New QueryParameter("@type", testplan.type, DbType.String),
                                                    New QueryParameter("@start_date", testplan.start_date, DbType.String),
                                                    New QueryParameter("@end_date", testplan.end_date, DbType.String),
                                                    New QueryParameter("@creator", testplan.creator, DbType.String),
                                                    New QueryParameter("@created", testplan.created, DbType.String),
                                                    New QueryParameter("@modified", testplan.modified, DbType.String),
                                                    New QueryParameter("@modifier", testplan.modifier, DbType.String),
                                                    New QueryParameter("@custom_field_1", testplan.custom_field_1, DbType.String),
                                                    New QueryParameter("@custom_field_2", testplan.custom_field_2, DbType.String),
                                                    New QueryParameter("@custom_field_3", testplan.custom_field_3, DbType.String),
                                                    New QueryParameter("@custom_field_4", testplan.custom_field_4, DbType.String),
                                                    New QueryParameter("@custom_field_5", testplan.custom_field_5, DbType.String),
                                                    New QueryParameter("@custom_field_6", testplan.custom_field_6, DbType.String),
                                                    New QueryParameter("@custom_field_7", testplan.custom_field_7, DbType.String),
                                                    New QueryParameter("@custom_field_8", testplan.custom_field_8, DbType.String),
                                                    New QueryParameter("@custom_field_9", testplan.custom_field_9, DbType.String),
                                                    New QueryParameter("@custom_field_10", testplan.custom_field_10, DbType.String),
                                                    New QueryParameter("@custom_field_11", testplan.custom_field_11, DbType.String),
                                                    New QueryParameter("@custom_field_12", testplan.custom_field_12, DbType.String),
                                                    New QueryParameter("@custom_field_13", testplan.custom_field_13, DbType.String),
                                                    New QueryParameter("@custom_field_14", testplan.custom_field_14, DbType.String),
                                                    New QueryParameter("@custom_field_15", testplan.custom_field_15, DbType.String),
                                                    New QueryParameter("@custom_field_16", testplan.custom_field_16, DbType.String),
                                                    New QueryParameter("@custom_field_17", testplan.custom_field_17, DbType.String),
                                                    New QueryParameter("@custom_field_18", testplan.custom_field_18, DbType.String),
                                                    New QueryParameter("@custom_field_19", testplan.custom_field_19, DbType.String),
                                                    New QueryParameter("@custom_field_20", testplan.custom_field_20, DbType.String),
                                                    New QueryParameter("@custom_field_21", testplan.custom_field_21, DbType.String),
                                                    New QueryParameter("@custom_field_22", testplan.custom_field_22, DbType.String),
                                                    New QueryParameter("@custom_field_23", testplan.custom_field_23, DbType.String),
                                                    New QueryParameter("@custom_field_24", testplan.custom_field_24, DbType.String),
                                                    New QueryParameter("@custom_field_25", testplan.custom_field_25, DbType.String),
                                                    New QueryParameter("@custom_field_26", testplan.custom_field_26, DbType.String),
                                                    New QueryParameter("@custom_field_27", testplan.custom_field_27, DbType.String),
                                                    New QueryParameter("@custom_field_28", testplan.custom_field_28, DbType.String),
                                                    New QueryParameter("@custom_field_29", testplan.custom_field_29, DbType.String),
                                                    New QueryParameter("@custom_field_30", testplan.custom_field_30, DbType.String),
                                                    New QueryParameter("@custom_field_31", testplan.custom_field_31, DbType.String),
                                                    New QueryParameter("@custom_field_32", testplan.custom_field_32, DbType.String),
                                                    New QueryParameter("@custom_field_33", testplan.custom_field_33, DbType.String),
                                                    New QueryParameter("@custom_field_34", testplan.custom_field_34, DbType.String),
                                                    New QueryParameter("@custom_field_35", testplan.custom_field_35, DbType.String),
                                                    New QueryParameter("@custom_field_36", testplan.custom_field_36, DbType.String),
                                                    New QueryParameter("@custom_field_37", testplan.custom_field_37, DbType.String),
                                                    New QueryParameter("@custom_field_38", testplan.custom_field_38, DbType.String),
                                                    New QueryParameter("@custom_field_39", testplan.custom_field_39, DbType.String),
                                                    New QueryParameter("@custom_field_40", testplan.custom_field_40, DbType.String),
                                                    New QueryParameter("@custom_field_41", testplan.custom_field_41, DbType.String),
                                                    New QueryParameter("@custom_field_42", testplan.custom_field_42, DbType.String),
                                                    New QueryParameter("@custom_field_43", testplan.custom_field_43, DbType.String),
                                                    New QueryParameter("@custom_field_44", testplan.custom_field_44, DbType.String),
                                                    New QueryParameter("@custom_field_45", testplan.custom_field_45, DbType.String),
                                                    New QueryParameter("@custom_field_46", testplan.custom_field_46, DbType.String),
                                                    New QueryParameter("@custom_field_47", testplan.custom_field_47, DbType.String),
                                                    New QueryParameter("@custom_field_48", testplan.custom_field_48, DbType.String),
                                                    New QueryParameter("@custom_field_49", testplan.custom_field_49, DbType.String),
                                                    New QueryParameter("@custom_field_50", testplan.custom_field_50, DbType.String),
                                                    New QueryParameter("@url", $"{Cfg_Constant.Url}/{testplan.workspace_id}/sparrow/test_plan/view/{testplan.id}", DbType.String),
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