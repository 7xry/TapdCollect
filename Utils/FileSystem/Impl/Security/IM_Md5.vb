Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Namespace Utils.FileSystem.Impl.Security
	Public Class IM_Md5
		''' <summary>
		'''     获取Md5字符串
		''' </summary>
		''' <param name="sourceString">原字符串</param>
		''' <returns>Md5字符串</returns>
		''' <remarks>获取Md5字符串</remarks>
		Public Shared Function Md5(ByVal sourceString As String) As String
			Dim md5Bytes As Byte() = GetMD5(SourceString)
			Return GetMD5Str(MD5bytes)
		End Function

		''' <summary>
		'''     获取MD5字节数组
		''' </summary>
		''' <param name="sourceString">原字符串</param>
		''' <returns>MD5字节数组</returns>
		''' <remarks>获取MD5字节数组</remarks>
		Public Shared Function GetMd5(ByVal sourceString As String) As Byte()
			Dim provider As New MD5CryptoServiceProvider
			Dim md5Bytes As Byte() = Encoding.UTF8.GetBytes(SourceString)
			MD5bytes = provider.ComputeHash(MD5bytes)
			Return MD5bytes
		End Function

		''' <summary>
		'''     获取Md5字符串
		''' </summary>
		''' <param name="md5Bytes">Md5字节数组</param>
		''' <returns>Md5字符串</returns>
		''' <remarks>获取Md5字符串</remarks>
		Public Shared Function GetMd5Str(ByVal md5Bytes As Byte()) As String
			Dim builder As New StringBuilder
			For Each b As Byte In MD5bytes
				builder.Append(b.ToString("x2").ToLower)
			Next
			Return builder.ToString
		End Function

		''' <summary>
		'''     获取文件Md5
		''' </summary>
		''' <param name="fileName">文件名</param>
		''' <returns>文件Md5</returns>
		''' <remarks>获取文件Md5</remarks>
		Public Shared Function GetMd5HashFromFile(ByVal fileName As String) As String
			Using file As New FileStream(FileName, FileMode.Open)
				Using md5 As New MD5CryptoServiceProvider
					Dim retVal As Byte() = md5.ComputeHash(File)
					Dim sbuff As New StringBuilder
					For i = 0 To retVal.Length - 1
						sbuff.Append(retVal(i).ToString("X2"))
					Next
					Return sbuff.ToString
				End Using
			End Using
		End Function
	End Class
End Namespace
