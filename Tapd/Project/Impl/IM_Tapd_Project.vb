
Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
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
    Public Class IM_Tapd_Project
        Public Shared Function GetList() As List(Of MD_Tapd_Project)
            Dim ProjectList As New List(Of MD_Tapd_Project)
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=Cfg_Constant.BaseUrl,
                    .RequestUrl=Cfg_Constant.WorkspacesProjects,
                    .ParmStr=$"company_id={Cfg_Constant.CompanyId}" 
                    }
            Dim tapd As MD_Tapd=IM_Req.DoGet(ReqParm)
            If tapd Is Nothing Then
                IM_Log.Showlog($"接口 [ {ReqParm.BaseUrl}/{ReqParm.RequestUrl}?{ReqParm.ParmStr} ] 请求返回异常", MsgType.ErrorMsg)
                Return ProjectList
            End If
            For dataIdx as Integer=0 To tapd.data.Count-1
                Dim project As MD_Tapd_Project=JsonConvert.DeserializeObject(Of MD_Tapd_Project)(tapd.data(dataIdx)("Workspace").ToString())
                If project.status="closed" Then
                    Continue For
                End If
                ProjectList.Add(project)
            Next
            Return ProjectList
        End Function

        Public Shared Function Delete() As Boolean
            Dim StrBuff As New StringBuilder
            StrBuff.AppendLine($"Delete FROM tapd_project")
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
                StrBuff.AppendLine($"INSERT INTO tapd_project ")
                StrBuff.AppendLine($"   (sysid, id, name, pretty_name, status, secrecy, created, creator_id, member_count, creator, collect_date)")
                StrBuff.AppendLine($"VALUES ")
                StrBuff.AppendLine($"   (uuid(), @id, @name, @pretty_name, @status, @secrecy, @created, @creator_id, @member_count, @creator, @collect_date)")
                IM_Log.Showlog($"执行Sql语句：{IM_AppPath.NewLine()}{StrBuff.ToString()}", MsgType.DebugMsg)
                For Each TData As Object In tapd.data
                    If TData Is Nothing Then
                        Return False
                    End If
                    Dim project = JsonConvert.DeserializeObject(Of MD_Tapd_Project)(TData("Workspace").ToString().Replace("'", "\'"))
                    Dim par = New QueryParameter() {New QueryParameter("@id", project.id, DbType.String),
                                                    New QueryParameter("@name", project.name, DbType.String),
                                                    New QueryParameter("@pretty_name", project.pretty_name, DbType.String),
                                                    New QueryParameter("@status", project.status, DbType.String),
                                                    New QueryParameter("@secrecy", project.secrecy, DbType.String),
                                                    New QueryParameter("@created", project.created, DbType.String),
                                                    New QueryParameter("@creator_id", project.creator_id, DbType.String),
                                                    New QueryParameter("@member_count", project.member_count, DbType.String),
                                                    New QueryParameter("@creator", project.creator, DbType.String),
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