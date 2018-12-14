Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Utils.DataBase.Dict
Imports TapdCollect.Utils.DataBase.Model
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Files
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path
Imports TapdCollect.Utils.FileSystem.Impl.Security

Namespace Tapd.Global.Impl
    Public Class IM_Config
        
    Public Shared Sub LoadConfiguration()
        If File.Exists(ConfigFilePath)=False Then
            CreateConfiguration
        End If
        Dim fileStr As String=File.ReadAllText(ConfigFilePath,Encoding.UTF8)
        Dim cfg As New MD_Config
        Try
            Dim ConfigStr As String=IM_DES3.DecryptBase64Str(fileStr,Cfg_Constant.DKey,Cfg_Constant.DV)
            cfg=JsonConvert.DeserializeObject(Of MD_Config)(ConfigStr)
            AuthUserName=IM_DES3.DecryptBase64Str(cfg.Api_User,Cfg_Constant.DKey,Cfg_Constant.DV)
            AuthPassWord=IM_DES3.DecryptBase64Str(cfg.Api_Password,Cfg_Constant.DKey,Cfg_Constant.DV)
            CompanyId=IM_DES3.DecryptBase64Str(cfg.CompanyId,Cfg_Constant.DKey,Cfg_Constant.DV)
            PageLimit=CType(IM_DES3.DecryptBase64Str(cfg.PageLimit,Cfg_Constant.DKey,Cfg_Constant.DV),Integer)
            RetryLimit=CType(IM_DES3.DecryptBase64Str(cfg.RetryLimit,Cfg_Constant.DKey,Cfg_Constant.DV),Integer)
            IsKeepHistory=CType(IM_DES3.DecryptBase64Str(cfg.IsKeepHistory,Cfg_Constant.DKey,Cfg_Constant.DV),Boolean)
            Dim DataBaseConn As String=IM_DES3.DecryptBase64Str(cfg.DataBaseConn,Cfg_Constant.DKey,Cfg_Constant.DV)
            DbConnDict.TapdCollect=New DataCase(DbTypeEnum.Mysql,DataBaseConn)
        Catch ex As Exception
            IM_Log.Showlog($"配置文件无效！请确认！", MsgType.ErrorMsg)
            CreateConfiguration()
            LoadConfiguration
        End Try
    End Sub

    Public Shared Sub CreateConfiguration()
        If DeleteConfiguration(False)=False Then
            Return
        End If
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($"===================================     配置文件自动生成工具     ===================================", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 请按提示信息依次输入以下信息", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 1、Api_User  - API帐号", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 2、Api_Password  - API口令", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 3、CompanyId  - 公司ID", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 4、PageLimit  - 每次请求最大数量", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 5、RetryLimit  - 出错后重试次数", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 6、IsKeepHistory  - 是否保留历史记录", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 7、DataBaseConn  - 数据库连接配置", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($"DataBaseConn 请参考如下配置：", MsgType.InfoMsg)
        IM_Log.Showlog($"Server=xx;Database=Tapd;User=xx;Password=xx;pooling=False;port=xx;Charset=utf8;Allow Zero Datetime=True;", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        Dim cfg As New MD_Config 
        cfg.Api_User=IM_DES3.EncryptBase64Str(InputCheck("Api_User"),Cfg_Constant.DKey,Cfg_Constant.DV)
        cfg.Api_Password=IM_DES3.EncryptBase64Str(InputCheck("Api_Password"),Cfg_Constant.DKey,Cfg_Constant.DV)
        cfg.CompanyId=IM_DES3.EncryptBase64Str(InputCheck("CompanyId"),Cfg_Constant.DKey,Cfg_Constant.DV)
        cfg.PageLimit=IM_DES3.EncryptBase64Str(GetPageLimit("PageLimit","可选范围：1～200"),Cfg_Constant.DKey,Cfg_Constant.DV)
        cfg.RetryLimit=IM_DES3.EncryptBase64Str(GetRetryLimit("RetryLimit","可选范围：0～10"),Cfg_Constant.DKey,Cfg_Constant.DV)
        cfg.IsKeepHistory=IM_DES3.EncryptBase64Str(GetIsKeepHistory("IsKeepHistory"),Cfg_Constant.DKey,Cfg_Constant.DV)
        cfg.DataBaseConn=IM_DES3.EncryptBase64Str(InputCheck("DataBaseConn"),Cfg_Constant.DKey,Cfg_Constant.DV)
        Dim ConfigStr As String=IM_DES3.EncryptBase64Str(JsonConvert.SerializeObject(cfg),Cfg_Constant.DKey,Cfg_Constant.DV)
        IM_Export.SaveConfig(ConfigStr)
        IM_Log.Showlog($"{IM_AppPath.NewLine()}{IM_AppPath.NewLine()}",MsgType.InfoMsg)
        If IM_Log.ShowConfirm("初始化数据库（已存在的记录将被清除！）","是否要") = True Then
            IM_Log.Showlog($"{IM_AppPath.NewLine()}{IM_AppPath.NewLine()}",MsgType.InfoMsg)
            If IM_Log.ShowConfirm("初始化数据库（已存在的记录将被清除！）","再次确认是否要") = True Then
                Dim DataBaseConn As String=IM_DES3.DecryptBase64Str(cfg.DataBaseConn,Cfg_Constant.DKey,Cfg_Constant.DV)
                DbConnDict.TapdCollect=New DataCase(DbTypeEnum.Mysql,DataBaseConn)
                IM_InitializeDataBase.Init()
            End If
        End If
        IM_Log.WaitForDo()
    End Sub

    Public Shared Function DeleteConfiguration(ByVal Optional IsManual As Boolean=True) As Boolean
        Try
            If IsManual=True Then
                If IM_Log.ShowConfirm("删除配置文件","是否要") = False Then
                    Return False
                End If
            End If
            If File.Exists(ConfigFilePath)=True Then
                File.Delete(ConfigFilePath)
            End If
            Return True
        Catch ex As Exception
            IM_Log.Showlog(ex.ToString(),MsgType.ErrorMsg)
            Return False
        End Try
    End Function

    Private Shared Function InputCheck(ByVal inputValue As String, ByVal Optional remindInfo As String="") As String
        Dim logMsg As String=$"请输入 [ {inputValue} ]："
        If remindInfo<> String.Empty Then
            logMsg=$"请输入 [ {inputValue} ] （{remindInfo}）："
        End If
        IM_Log.Showlog($"{logMsg}",MsgType.InfoMsg)
        Dim InputStr As String = Console.ReadLine().Trim()
        If InputStr=String.Empty Then
            IM_Log.Showlog($"输入的内容无效，请重新输入！", MsgType.InfoMsg)
            Return InputCheck(inputValue,remindInfo)
        End If
        If IM_Log.ShowConfirm($"{InputStr}")=False Then
            Return InputCheck(inputValue,remindInfo)
        End If
        Return InputStr
    End Function

    Private Shared Function GetIsKeepHistory(ByVal inputValue As String) As String
        Dim IsKeep As String="True"
        Dim logMsg As String=$"请输入 [ {inputValue} ]（可选范围：0-不保留，1-保留）："
        IM_Log.Showlog($"{logMsg}",MsgType.InfoMsg)
        Dim InputStr As String = Console.ReadLine().Trim()
        Select Case InputStr.Trim().ToUpper()
            Case 0
                IsKeep="False"
            Case 1
                IsKeep="True"
            Case Else
                IM_Log.Showlog($"输入的内容无效，请重新输入！", MsgType.InfoMsg)
                Return InputCheck(inputValue)
        End Select
        If IM_Log.ShowConfirm($"{InputStr}")=False Then
            Return GetIsKeepHistory(InputStr)
        End If
        Return IsKeep
    End Function

    Private Shared Function GetPageLimit(ByVal inputValue As String, ByVal Optional remindInfo As String="") As String
        Dim limit As String=InputCheck(inputValue,remindInfo)
        Try
            If CInt(limit)<1 or CInt(limit)>200 Then
                IM_Log.Showlog($"输入的 [ PageLimit ] 范围无效，请重新输入！", MsgType.InfoMsg)
                GetPageLimit(inputValue,remindInfo)
            End If
        Catch ex As Exception
            IM_Log.Showlog($"输入的 [ PageLimit ] 范围无效，请重新输入！", MsgType.InfoMsg)
            IM_Log.Showlog(ex.ToString(),MsgType.ErrorMsg)
            GetPageLimit(inputValue,remindInfo)
        End Try
        Return limit
    End Function

    Private Shared Function GetRetryLimit(ByVal inputValue As String, ByVal Optional remindInfo As String="") As String
        Dim limit As String=InputCheck(inputValue,remindInfo)
        Try
            If CInt(limit)<0 or CInt(limit)>10 Then
                IM_Log.Showlog($"输入的 [ RetryLimit ] 范围无效，请重新输入！", MsgType.InfoMsg)
                GetRetryLimit(inputValue,remindInfo)
            End If
        Catch ex As Exception
            IM_Log.Showlog($"输入的 [ RetryLimit ] 范围无效，请重新输入！", MsgType.InfoMsg)
            IM_Log.Showlog(ex.ToString(),MsgType.ErrorMsg)
            GetRetryLimit(inputValue,remindInfo)
        End Try
        Return limit
    End Function
    End Class
End NameSpace