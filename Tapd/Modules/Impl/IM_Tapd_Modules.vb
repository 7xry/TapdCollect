Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.Modules.Model
Imports TapdCollect.Utils.DataBase
Imports TapdCollect.Utils.DataBase.API
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Modules.Impl
    Public Class IM_Tapd_Modules
        Public Shared Function GetCount(ByVal workspace_id As String) As Nullable(Of Integer)
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.ModulesCount,
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
            StrBuff.Append($"Delete FROM tapd_modules where workspace_id='{workspace_id}' ")
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
                StrBuff.AppendLine($"INSERT INTO tapd_modules ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       sysid, id, name, description, created, owner, xdefault, sort, workspace_id, collect_date ")
                StrBuff.AppendLine($"   )")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (")
                StrBuff.AppendLine($"       uuid(), @id, @name, @description, @created, @owner, @xdefault, @sort, @workspace_id, @collect_date ")
                StrBuff.AppendLine($"   )")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim Modules As MD_Tapd_Modules = JsonConvert.DeserializeObject(Of MD_Tapd_Modules)(TData("Module").ToString())
                    Dim par = New QueryParameter() {New QueryParameter("@id", Modules.id, DbType.String),
                                                    New QueryParameter("@name", Modules.name, DbType.String),
                                                    New QueryParameter("@description", Modules.description, DbType.String),
                                                    New QueryParameter("@created", Modules.created, DbType.String),
                                                    New QueryParameter("@owner", Modules.owner, DbType.String),
                                                    New QueryParameter("@xdefault", Modules.xdefault, DbType.String),
                                                    New QueryParameter("@sort", Modules.sort, DbType.String),
                                                    New QueryParameter("@workspace_id", Modules.workspace_id, DbType.String),
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