
Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
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
    Public Class IM_Tapd_Stories
        Public Shared Function GetCount(ByVal workspace_id As String) As Nullable(Of Integer)
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.StoriesCount,
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
            StrBuff.Append($"Delete FROM tapd_stories where workspace_id='{workspace_id}' ")
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
                StrBuff.AppendLine($"INSERT INTO tapd_stories ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, name, description, workspace_id, creator, ")
                StrBuff.AppendLine($"       created, modified, status, owner, cc, ")
                StrBuff.AppendLine($"       begin, due, size, priority, developer, ")
                StrBuff.AppendLine($"       iteration_id, test_focus, type, source, module, ")
                StrBuff.AppendLine($"       version, completed, category_id, parent_id, children_id, ")
                StrBuff.AppendLine($"       ancestor_id, business_value, effort, effort_completed, ")
                StrBuff.AppendLine($"       exceed, remain, release_id, custom_field_one, custom_field_two, ")
                StrBuff.AppendLine($"       custom_field_three, custom_field_four, custom_field_five, custom_field_six, custom_field_seven, ")
                StrBuff.AppendLine($"       custom_field_eight, custom_field_9, custom_field_10, custom_field_11, custom_field_12, ")
                StrBuff.AppendLine($"       custom_field_13, custom_field_14, custom_field_15, custom_field_16, custom_field_17, ")
                StrBuff.AppendLine($"       custom_field_18, custom_field_19, custom_field_20, custom_field_21, custom_field_22, ")
                StrBuff.AppendLine($"       custom_field_23, custom_field_24, custom_field_25, custom_field_26, custom_field_27, ")
                StrBuff.AppendLine($"       custom_field_28, custom_field_29, custom_field_30, custom_field_31, custom_field_32, ")
                StrBuff.AppendLine($"       custom_field_33, custom_field_34, custom_field_35, custom_field_36, custom_field_37, ")
                StrBuff.AppendLine($"       custom_field_38, custom_field_39, custom_field_40, custom_field_41, custom_field_42, ")
                StrBuff.AppendLine($"       custom_field_43, custom_field_44, custom_field_45, custom_field_46, custom_field_47, ")
                StrBuff.AppendLine($"       custom_field_48, custom_field_49, custom_field_50, custom_field_51, custom_field_52, ")
                StrBuff.AppendLine($"       custom_field_53, custom_field_54, custom_field_55, custom_field_56, custom_field_57, ")
                StrBuff.AppendLine($"       custom_field_58, custom_field_59, custom_field_60, custom_field_61, custom_field_62, ")
                StrBuff.AppendLine($"       custom_field_63, custom_field_64, custom_field_65, custom_field_66, custom_field_67, ")
                StrBuff.AppendLine($"       custom_field_68, custom_field_69, custom_field_70, custom_field_71, custom_field_72, ")
                StrBuff.AppendLine($"       custom_field_73, custom_field_74, custom_field_75, custom_field_76, custom_field_77, ")
                StrBuff.AppendLine($"       custom_field_78, custom_field_79, custom_field_80, custom_field_81, custom_field_82, ")
                StrBuff.AppendLine($"       custom_field_83, custom_field_84, custom_field_85, custom_field_86, custom_field_87, ")
                StrBuff.AppendLine($"       custom_field_88, custom_field_89, custom_field_90, custom_field_91, custom_field_92, ")
                StrBuff.AppendLine($"       custom_field_93, custom_field_94, custom_field_95, custom_field_96, custom_field_97, ")
                StrBuff.AppendLine($"       custom_field_98, custom_field_99, custom_field_100, url, collect_date  ")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @name, @description, @workspace_id, @creator,")
                StrBuff.AppendLine($"       @created, @modified, @status, @owner, @cc,")
                StrBuff.AppendLine($"       @begin, @due, @size, @priority, @developer,")
                StrBuff.AppendLine($"       @iteration_id, @test_focus, @type, @source, @module,")
                StrBuff.AppendLine($"       @version, @completed, @category_id, @parent_id, @children_id,")
                StrBuff.AppendLine($"       @ancestor_id, @business_value, @effort, @effort_completed,")
                StrBuff.AppendLine($"       @exceed, @remain, @release_id, @custom_field_one, @custom_field_two,")
                StrBuff.AppendLine($"       @custom_field_three, @custom_field_four, @custom_field_five, @custom_field_six, @custom_field_seven,")
                StrBuff.AppendLine($"       @custom_field_eight, @custom_field_9, @custom_field_10, @custom_field_11, @custom_field_12,")
                StrBuff.AppendLine($"       @custom_field_13, @custom_field_14, @custom_field_15, @custom_field_16, @custom_field_17,")
                StrBuff.AppendLine($"       @custom_field_18, @custom_field_19, @custom_field_20, @custom_field_21, @custom_field_22,")
                StrBuff.AppendLine($"       @custom_field_23, @custom_field_24, @custom_field_25, @custom_field_26, @custom_field_27,")
                StrBuff.AppendLine($"       @custom_field_28, @custom_field_29, @custom_field_30, @custom_field_31, @custom_field_32,")
                StrBuff.AppendLine($"       @custom_field_33, @custom_field_34, @custom_field_35, @custom_field_36, @custom_field_37,")
                StrBuff.AppendLine($"       @custom_field_38, @custom_field_39, @custom_field_40, @custom_field_41, @custom_field_42,")
                StrBuff.AppendLine($"       @custom_field_43, @custom_field_44, @custom_field_45, @custom_field_46, @custom_field_47,")
                StrBuff.AppendLine($"       @custom_field_48, @custom_field_49, @custom_field_50, @custom_field_51, @custom_field_52,")
                StrBuff.AppendLine($"       @custom_field_53, @custom_field_54, @custom_field_55, @custom_field_56, @custom_field_57,")
                StrBuff.AppendLine($"       @custom_field_58, @custom_field_59, @custom_field_60, @custom_field_61, @custom_field_62,")
                StrBuff.AppendLine($"       @custom_field_63, @custom_field_64, @custom_field_65, @custom_field_66, @custom_field_67,")
                StrBuff.AppendLine($"       @custom_field_68, @custom_field_69, @custom_field_70, @custom_field_71, @custom_field_72,")
                StrBuff.AppendLine($"       @custom_field_73, @custom_field_74, @custom_field_75, @custom_field_76, @custom_field_77,")
                StrBuff.AppendLine($"       @custom_field_78, @custom_field_79, @custom_field_80, @custom_field_81, @custom_field_82,")
                StrBuff.AppendLine($"       @custom_field_83, @custom_field_84, @custom_field_85, @custom_field_86, @custom_field_87,")
                StrBuff.AppendLine($"       @custom_field_88, @custom_field_89, @custom_field_90, @custom_field_91, @custom_field_92,")
                StrBuff.AppendLine($"       @custom_field_93, @custom_field_94, @custom_field_95, @custom_field_96, @custom_field_97,")
                StrBuff.AppendLine($"       @custom_field_98, @custom_field_99, @custom_field_100, @url, @collect_date ")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim story = JsonConvert.DeserializeObject(Of MD_Tapd_Stories)(TData("Story").ToString())
                    If story.owner IsNot Nothing And story.owner<>"" Then
                        If story.owner.EndsWith(";")=False Then
                            story.owner+=";"
                        End If
                    Else 
                        story.owner="TapdSystem;"
                    End If
                    Dim par = New QueryParameter() {New QueryParameter("@id", story.id, DbType.String),
                                                    New QueryParameter("@name", story.name, DbType.String),
                                                    New QueryParameter("@description", Nothing, DbType.String),
                                                    New QueryParameter("@workspace_id", story.workspace_id, DbType.String),
                                                    New QueryParameter("@creator", story.creator, DbType.String),
                                                    New QueryParameter("@created", story.created, DbType.String),
                                                    New QueryParameter("@modified", story.modified, DbType.String),
                                                    New QueryParameter("@status", story.status, DbType.String),
                                                    New QueryParameter("@owner", story.owner, DbType.String),
                                                    New QueryParameter("@cc", story.cc, DbType.String),
                                                    New QueryParameter("@begin", story.begin, DbType.String),
                                                    New QueryParameter("@due", story.due, DbType.String),
                                                    New QueryParameter("@size", story.size, DbType.String),
                                                    New QueryParameter("@priority", story.priority, DbType.String),
                                                    New QueryParameter("@developer", story.developer, DbType.String),
                                                    New QueryParameter("@iteration_id", story.iteration_id, DbType.String),
                                                    New QueryParameter("@test_focus", story.test_focus, DbType.String),
                                                    New QueryParameter("@type", story.type, DbType.String),
                                                    New QueryParameter("@source", story.source, DbType.String),
                                                    New QueryParameter("@module", story.module, DbType.String),
                                                    New QueryParameter("@version", story.version, DbType.String),
                                                    New QueryParameter("@completed", story.completed, DbType.String),
                                                    New QueryParameter("@category_id", story.category_id, DbType.String),
                                                    New QueryParameter("@parent_id", story.parent_id, DbType.String),
                                                    New QueryParameter("@children_id", story.children_id, DbType.String),
                                                    New QueryParameter("@ancestor_id", story.ancestor_id, DbType.String),
                                                    New QueryParameter("@business_value", story.business_value, DbType.String),
                                                    New QueryParameter("@effort", story.effort, DbType.String),
                                                    New QueryParameter("@effort_completed", story.effort_completed, DbType.String),
                                                    New QueryParameter("@exceed", story.exceed, DbType.String),
                                                    New QueryParameter("@remain", story.remain, DbType.String),
                                                    New QueryParameter("@release_id", story.release_id, DbType.String),
                                                    New QueryParameter("@custom_field_one", story.custom_field_one, DbType.String),
                                                    New QueryParameter("@custom_field_two", story.custom_field_two, DbType.String),
                                                    New QueryParameter("@custom_field_three", story.custom_field_three, DbType.String),
                                                    New QueryParameter("@custom_field_four", story.custom_field_four, DbType.String),
                                                    New QueryParameter("@custom_field_five", story.custom_field_five, DbType.String),
                                                    New QueryParameter("@custom_field_six", story.custom_field_six, DbType.String),
                                                    New QueryParameter("@custom_field_seven", story.custom_field_seven, DbType.String),
                                                    New QueryParameter("@custom_field_eight", story.custom_field_eight, DbType.String),
                                                    New QueryParameter("@custom_field_9", story.custom_field_9, DbType.String),
                                                    New QueryParameter("@custom_field_10", story.custom_field_10, DbType.String),
                                                    New QueryParameter("@custom_field_11", story.custom_field_11, DbType.String),
                                                    New QueryParameter("@custom_field_12", story.custom_field_12, DbType.String),
                                                    New QueryParameter("@custom_field_13", story.custom_field_13, DbType.String),
                                                    New QueryParameter("@custom_field_14", story.custom_field_14, DbType.String),
                                                    New QueryParameter("@custom_field_15", story.custom_field_15, DbType.String),
                                                    New QueryParameter("@custom_field_16", story.custom_field_16, DbType.String),
                                                    New QueryParameter("@custom_field_17", story.custom_field_17, DbType.String),
                                                    New QueryParameter("@custom_field_18", story.custom_field_18, DbType.String),
                                                    New QueryParameter("@custom_field_19", story.custom_field_19, DbType.String),
                                                    New QueryParameter("@custom_field_20", story.custom_field_20, DbType.String),
                                                    New QueryParameter("@custom_field_21", story.custom_field_21, DbType.String),
                                                    New QueryParameter("@custom_field_22", story.custom_field_22, DbType.String),
                                                    New QueryParameter("@custom_field_23", story.custom_field_23, DbType.String),
                                                    New QueryParameter("@custom_field_24", story.custom_field_24, DbType.String),
                                                    New QueryParameter("@custom_field_25", story.custom_field_25, DbType.String),
                                                    New QueryParameter("@custom_field_26", story.custom_field_26, DbType.String),
                                                    New QueryParameter("@custom_field_27", story.custom_field_27, DbType.String),
                                                    New QueryParameter("@custom_field_28", story.custom_field_28, DbType.String),
                                                    New QueryParameter("@custom_field_29", story.custom_field_29, DbType.String),
                                                    New QueryParameter("@custom_field_30", story.custom_field_30, DbType.String),
                                                    New QueryParameter("@custom_field_31", story.custom_field_31, DbType.String),
                                                    New QueryParameter("@custom_field_32", story.custom_field_32, DbType.String),
                                                    New QueryParameter("@custom_field_33", story.custom_field_33, DbType.String),
                                                    New QueryParameter("@custom_field_34", story.custom_field_34, DbType.String),
                                                    New QueryParameter("@custom_field_35", story.custom_field_35, DbType.String),
                                                    New QueryParameter("@custom_field_36", story.custom_field_36, DbType.String),
                                                    New QueryParameter("@custom_field_37", story.custom_field_37, DbType.String),
                                                    New QueryParameter("@custom_field_38", story.custom_field_38, DbType.String),
                                                    New QueryParameter("@custom_field_39", story.custom_field_39, DbType.String),
                                                    New QueryParameter("@custom_field_40", story.custom_field_40, DbType.String),
                                                    New QueryParameter("@custom_field_41", story.custom_field_41, DbType.String),
                                                    New QueryParameter("@custom_field_42", story.custom_field_42, DbType.String),
                                                    New QueryParameter("@custom_field_43", story.custom_field_43, DbType.String),
                                                    New QueryParameter("@custom_field_44", story.custom_field_44, DbType.String),
                                                    New QueryParameter("@custom_field_45", story.custom_field_45, DbType.String),
                                                    New QueryParameter("@custom_field_46", story.custom_field_46, DbType.String),
                                                    New QueryParameter("@custom_field_47", story.custom_field_47, DbType.String),
                                                    New QueryParameter("@custom_field_48", story.custom_field_48, DbType.String),
                                                    New QueryParameter("@custom_field_49", story.custom_field_49, DbType.String),
                                                    New QueryParameter("@custom_field_50", story.custom_field_50, DbType.String),
                                                    New QueryParameter("@custom_field_51", story.custom_field_51, DbType.String),
                                                    New QueryParameter("@custom_field_52", story.custom_field_52, DbType.String),
                                                    New QueryParameter("@custom_field_53", story.custom_field_53, DbType.String),
                                                    New QueryParameter("@custom_field_54", story.custom_field_54, DbType.String),
                                                    New QueryParameter("@custom_field_55", story.custom_field_55, DbType.String),
                                                    New QueryParameter("@custom_field_56", story.custom_field_56, DbType.String),
                                                    New QueryParameter("@custom_field_57", story.custom_field_57, DbType.String),
                                                    New QueryParameter("@custom_field_58", story.custom_field_58, DbType.String),
                                                    New QueryParameter("@custom_field_59", story.custom_field_59, DbType.String),
                                                    New QueryParameter("@custom_field_60", story.custom_field_60, DbType.String),
                                                    New QueryParameter("@custom_field_61", story.custom_field_61, DbType.String),
                                                    New QueryParameter("@custom_field_62", story.custom_field_62, DbType.String),
                                                    New QueryParameter("@custom_field_63", story.custom_field_63, DbType.String),
                                                    New QueryParameter("@custom_field_64", story.custom_field_64, DbType.String),
                                                    New QueryParameter("@custom_field_65", story.custom_field_65, DbType.String),
                                                    New QueryParameter("@custom_field_66", story.custom_field_66, DbType.String),
                                                    New QueryParameter("@custom_field_67", story.custom_field_67, DbType.String),
                                                    New QueryParameter("@custom_field_68", story.custom_field_68, DbType.String),
                                                    New QueryParameter("@custom_field_69", story.custom_field_69, DbType.String),
                                                    New QueryParameter("@custom_field_70", story.custom_field_70, DbType.String),
                                                    New QueryParameter("@custom_field_71", story.custom_field_71, DbType.String),
                                                    New QueryParameter("@custom_field_72", story.custom_field_72, DbType.String),
                                                    New QueryParameter("@custom_field_73", story.custom_field_73, DbType.String),
                                                    New QueryParameter("@custom_field_74", story.custom_field_74, DbType.String),
                                                    New QueryParameter("@custom_field_75", story.custom_field_75, DbType.String),
                                                    New QueryParameter("@custom_field_76", story.custom_field_76, DbType.String),
                                                    New QueryParameter("@custom_field_77", story.custom_field_77, DbType.String),
                                                    New QueryParameter("@custom_field_78", story.custom_field_78, DbType.String),
                                                    New QueryParameter("@custom_field_79", story.custom_field_79, DbType.String),
                                                    New QueryParameter("@custom_field_80", story.custom_field_80, DbType.String),
                                                    New QueryParameter("@custom_field_81", story.custom_field_81, DbType.String),
                                                    New QueryParameter("@custom_field_82", story.custom_field_82, DbType.String),
                                                    New QueryParameter("@custom_field_83", story.custom_field_83, DbType.String),
                                                    New QueryParameter("@custom_field_84", story.custom_field_84, DbType.String),
                                                    New QueryParameter("@custom_field_85", story.custom_field_85, DbType.String),
                                                    New QueryParameter("@custom_field_86", story.custom_field_86, DbType.String),
                                                    New QueryParameter("@custom_field_87", story.custom_field_87, DbType.String),
                                                    New QueryParameter("@custom_field_88", story.custom_field_88, DbType.String),
                                                    New QueryParameter("@custom_field_89", story.custom_field_89, DbType.String),
                                                    New QueryParameter("@custom_field_90", story.custom_field_90, DbType.String),
                                                    New QueryParameter("@custom_field_91", story.custom_field_91, DbType.String),
                                                    New QueryParameter("@custom_field_92", story.custom_field_92, DbType.String),
                                                    New QueryParameter("@custom_field_93", story.custom_field_93, DbType.String),
                                                    New QueryParameter("@custom_field_94", story.custom_field_94, DbType.String),
                                                    New QueryParameter("@custom_field_95", story.custom_field_95, DbType.String),
                                                    New QueryParameter("@custom_field_96", story.custom_field_96, DbType.String),
                                                    New QueryParameter("@custom_field_97", story.custom_field_97, DbType.String),
                                                    New QueryParameter("@custom_field_98", story.custom_field_98, DbType.String),
                                                    New QueryParameter("@custom_field_99", story.custom_field_99, DbType.String),
                                                    New QueryParameter("@custom_field_100", story.custom_field_100, DbType.String),
                                                    New QueryParameter("@url", $"{Cfg_Constant.Url}/{story.workspace_id}/prong/stories/view/{story.id}", DbType.String),
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