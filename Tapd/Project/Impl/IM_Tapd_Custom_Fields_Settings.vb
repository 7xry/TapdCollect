
Imports System.Data
Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Project.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Project.Impl
    Public Class IM_Tapd_Custom_Fields_Settings
        Public Shared Function Delete(ByVal workspace_id As String) As Boolean
            Dim StrBuff As New StringBuilder
            StrBuff.AppendLine($"Delete FROM tapd_custom_fields_settings where workspace_id='{workspace_id}' and collect_date='{IM_JsDate.GetNowStr("yyyy-MM-dd")}'")
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
                StrBuff.AppendLine($"INSERT INTO tapd_custom_fields_settings ")
                StrBuff.AppendLine($"   (sysid, id, workspace_id, entry_type, custom_field, type, name, options, enabled, sort, memo, collect_date)")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (uuid(), @id, @workspace_id, @entry_type, @custom_field, @type, @name, @options, @enabled, @sort, @memo, @collect_date)")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim CustomFieldConfig = JsonConvert.DeserializeObject(Of MD_Tapd_Custom_Fields_Settings)(TData("CustomFieldConfig").ToString())
                    Dim par = New QueryParameter() {New QueryParameter("@id", CustomFieldConfig.id, DbType.String),
                                                    New QueryParameter("@workspace_id", CustomFieldConfig.workspace_id, DbType.String),
                                                    New QueryParameter("@entry_type", CustomFieldConfig.entry_type, DbType.String),
                                                    New QueryParameter("@custom_field", CustomFieldConfig.custom_field, DbType.String),
                                                    New QueryParameter("@type", CustomFieldConfig.type, DbType.String),
                                                    New QueryParameter("@name", CustomFieldConfig.name, DbType.String),
                                                    New QueryParameter("@options", CustomFieldConfig.options, DbType.String),
                                                    New QueryParameter("@enabled", CustomFieldConfig.enabled, DbType.String),
                                                    New QueryParameter("@sort", CustomFieldConfig.sort, DbType.String),
                                                    New QueryParameter("@memo", CustomFieldConfig.memo, DbType.String),
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