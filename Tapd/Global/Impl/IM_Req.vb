﻿Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports RestSharp.Authenticators
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Global.Impl
    Public Class IM_Req
        'Public Shared Function DoPostRequest(ByVal ReqParm As MD_Request) As JObject
        '    '请求
        '    Dim rest As New RestClient(ReqParm.BaseUrl)
        '    rest.Authenticator = New HttpBasicAuthenticator(Cfg_Constant.AuthUserName, Cfg_Constant.AuthPassWord)
        '    Dim request As New RestRequest(ReqParm.RequestUrl, Method.POST)
        '    request.AddHeader("Content-Type", "application/json; charset=utf-8")
        '    request.AddParameter("Data", ReqParm.ParmStr,ParameterType.QueryString), ParameterType.RequestBody)
        '    Dim response As IRestResponse = rest.Execute(request)
        '    Return response.Content
        'End Function

        Public Shared Function DoGet(ByVal ReqParm As MD_Request) As MD_Tapd
            Try
                Dim rest As New RestClient(ReqParm.BaseUrl)
                rest.Authenticator = New HttpBasicAuthenticator(Cfg_Constant.AuthUserName, Cfg_Constant.AuthPassWord)
                Dim request As New RestRequest($"{ReqParm.RequestUrl}?{ReqParm.ParmStr}", Method.GET)
                request.AddHeader("Content-Type", "application/json; charset=utf-8")
                Dim response As IRestResponse = rest.Execute(request)
                IM_Log.Showlog($"RequestURL：{ReqParm.BaseUrl}/{ReqParm.RequestUrl}?{ReqParm.ParmStr}", MsgType.InfoMsg)
                Threading.Thread.Sleep(1000)
                Dim tapd As MD_Tapd = JsonConvert.DeserializeObject(Of MD_Tapd)(response.Content)
                If tapd.status <> "1" Then
                    IM_Log.Showlog($"{tapd.status} - {tapd.info}", MsgType.ErrorMsg)
                    Return Nothing
                End If
                Return tapd
            Catch ex As Exception
                IM_Log.Showlog($"调用接口失败！{IM_AppPath.NewLine()}{ex.ToString()}",MsgType.ErrorMsg)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace