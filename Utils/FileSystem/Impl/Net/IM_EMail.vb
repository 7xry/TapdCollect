Imports System.ComponentModel
Imports System.Net
Imports System.Net.Mail
Imports System.Text
Imports System.Threading
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Date
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path
Imports TapdCollect.Utils.FileSystem.Model

Namespace Utils.FileSystem.Impl.Net
	Public Class IM_EMail

#Region "邮件静态属性"
		'邮件服务器端口
		Private Shared ReadOnly Timeout As Integer = 60000
		'超时时间
		Private Shared IsRun As Boolean = False
		'发邮件线程是否启动
		Private Shared Mails As New List(Of MD_EMail)()
		'待发邮件列表

#End Region

#Region "方法"

		Public Shared Sub Send(ByVal EMail As MD_EMail)
			Mails.Add(EMail)
			If Not IsRun Then
				ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf StartSend))
				IsRun = True
			End If
		End Sub

		Public Shared Function SendMail(ByVal EMail As MD_EMail) As String
			Try
				Dim client As New SmtpClient(EMail.SmtpServer, EMail.SmtpServerPort) With {
					    .UseDefaultCredentials = True,
					    .Credentials = New NetworkCredential(EMail.UserName, EMail.Password),
					    .DeliveryMethod = SmtpDeliveryMethod.Network,
					    .EnableSsl = True,
					    .Timeout = Timeout
					    }

				Dim mailMessage As New MailMessage With {
					    .From = New MailAddress(EMail.UserName, EMail.FromName, Encoding.UTF8),
					    .Subject = EMail.Subject,
					    .SubjectEncoding = Encoding.UTF8,
					    .Body = EMail.Body,
					    .BodyEncoding = Encoding.UTF8,
					    .IsBodyHtml = EMail.IsBodyHtml,
					    .Priority = MailPriority.High
					    }
				For Each mailTo As Object In EMail.Tos
					mailMessage.To.Add(mailTo)
				Next
				For Each cc As Object In EMail.CCs
					mailMessage.CC.Add(cc)
				Next
				For Each file As Object In EMail.Attachments
					mailMessage.Attachments.Add(New Attachment(file))
				Next
				Dim userState As Object = mailMessage
				AddHandler client.SendCompleted, New SendCompletedEventHandler(AddressOf SendCompletedCallback)
				client.SendAsync(mailMessage, EMail)
                Return $"邮件发送完成！"
			Catch ex As Exception
				Return ex.Message
			End Try
		End Function

		Private Shared Sub SendCompletedCallback(ByVal sender As Object,ByVal e As AsyncCompletedEventArgs)
			Dim mail As MD_EMail = TryCast(e.UserState, MD_EMail)
			If e.Cancelled Then
				IM_Log.Showlog($"{IM_JsDate.GetNowStr("yyyy-MM-dd hh:mm:ss")} 邮件【{mail.Subject}】发送取消！",MsgType.InfoMsg)
				mail.Status = 4
			ElseIf e.Error IsNot Nothing Then
				'这里根据e.UserState的值可以判断多种返回状态
			    IM_Log.Showlog($"{IM_JsDate.GetNowStr("yyyy-MM-dd hh:mm:ss")} 邮件【{mail.Subject}】发送失败！{IM_AppPath.NewLine()}{e.Error.Message}" ,MsgType.ErrorMsg)
				mail.Status = 3
			Else
				Mails.Remove(mail)
			End If
		End Sub

		Private Shared Sub StartSend(ByVal obj As Object)
			While True
				Dim mail As MD_EMail = Mails.Where(Function(p) p.Status = 0 AndAlso p.SendCnt < 3).OrderByDescending(Function(p) p.Priority).FirstOrDefault()
				If mail IsNot Nothing Then
					If mail.Tos.Count > 0 Then
						SendMail(mail)
						mail.SendCnt += 1
						mail.Status = 1
					Else
						mail.Status = 3
					End If
				End If
				Thread.Sleep(500)
			End While
		End Sub

#End Region
	End Class
End Namespace