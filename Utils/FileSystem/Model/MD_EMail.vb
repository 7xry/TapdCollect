Imports System.Net.Mail

Namespace Utils.FileSystem.Model
	Public Class MD_EMail

#Region "邮件属性"

		Public Property SmtpServer As String = "smtp.exmail.qq.com"		'smtp.163.com
		Public Property SmtpServerPort As Integer = 25

		Public Property UserName As String = "hotwind@7xry.com"			'reywongcc@163.com
		Public Property Password As String = "Hw321#@!"					'reywong123

		Public Property Subject As String

		Public Property Body As String
		'默认发件箱
		Public Property FromName As String

		Public Property Priority As Integer =MailPriority.Normal

		Public Property IsBodyHtml As Boolean =True

		Public Property Status As Integer =0
		'0:未发送，1:发送中，2:已发送，3：发送失败,4：发送取消
		Public Property SendCnt As Integer =0

		Public Property Attachments As new List(Of String)
		'附件
		Public Property Tos As new List(Of String)
		'接收人
		Public Property CCs As new List(Of String)
		'抄送人

#End Region
	End Class
End NameSpace