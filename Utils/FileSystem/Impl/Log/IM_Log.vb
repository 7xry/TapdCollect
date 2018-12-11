Imports NLog
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Utils.FileSystem.Impl.Log
    Public Class IM_Log
        Private Shared ReadOnly InfoLogger As Logger=LogManager.GetLogger("Tapd同步工具")
        Private Shared ReadOnly ErrLogger As Logger = LogManager.GetLogger("Tapd同步工具错误日志")
        Private Shared ReadOnly DebugLogger As Logger = LogManager.GetLogger("Tapd同步工具调试日志")
        Public Shared Function ShowConfirm(ByVal ConfirmText As String,Optional RemindText As String="请确认您的输入：") As Boolean
            Showlog($"{RemindText} [ {ConfirmText} ] ？{IM_AppPath.NewLine()}（输入 [ 1 ] 或 [ YES ] 或 [ 回车 ] 表示同意，输入其他任意值，表示否决）：",MsgType.InfoMsg)
            Dim ReadLine As String = Console.ReadLine().Trim().ToUpper()
            Select Case ReadLine
                Case "1", "YES","Y",""
                    Showlog($"[ {ConfirmText} ] 已被确认！",MsgType.InfoMsg)
                    Return True
                Case Else
                    Showlog($"[ {ConfirmText} ] 已被取消！",MsgType.InfoMsg)
                    Return False
            End Select
        End Function

        Public Shared Sub Showlog(ByVal LogMsg As String, ByVal LogType As MsgType)
            Select Case LogType
                Case MsgType.DebugMsg
                    DebugLogger.Debug(LogMsg)
                Case MsgType.ErrorMsg
                    ErrLogger.Error(LogMsg)
                Case MsgType.InfoMsg
                    InfoLogger.Info(LogMsg)
                Case Else
                    InfoLogger.Info(LogMsg)
            End Select
        End Sub

        Public Shared Sub WaitForDo() 
            Showlog($"请按 [ 回车 ] 继续...",MsgType.InfoMsg)
            Console.ReadLine()
        End Sub
    End Class
End Namespace