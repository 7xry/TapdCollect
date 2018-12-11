Imports System.Globalization
Imports System.Text

Namespace Utils.FileSystem.Impl.String
	Public Class IM_String
		''' <summary>
		'''     字符串转16进制字符串
		''' </summary>
		''' <param name="sourceString">原字符串</param>
		''' <param name="encode">编码</param>
		''' <returns>16进制字符串</returns>
		''' <remarks>字符串转16进制字符串</remarks>
		Public Shared Function StringToHexString(sourceString As String, encode As Encoding) As String
			'按照指定编码将string编程字节数组
			Dim b As Byte() = encode.GetBytes(SourceString)
			Dim result As String = String.Empty
			For i = 0 To b.Length - 1
				result += $"%{Convert.ToString(b(i), 16) }"
			Next
			Return result
		End Function

		''' <summary>
		'''     16进制字符串转字符串
		''' </summary>
		''' <param name="hexString">16进制字符串</param>
		''' <param name="encode">编码</param>
		''' <returns>字符串</returns>
		''' <remarks>16进制字符串转字符串</remarks>
		Public Shared Function HexStringToString(hexString As String, encode As Encoding) As String
			'以%分割字符串，并去掉空字符
			Dim chars As String() = HexString.Split(New Char() {"%"}, StringSplitOptions.RemoveEmptyEntries)
			Dim b = New Byte(chars.Length) {}
			'逐个字符变为16进制字节数据
			For i = 0 To chars.Length - 1
				b(i) = Convert.ToByte(chars(i), 16)
			Next
			'按照指定编码将字节数组变为字符串
			Return encode.GetString(b)
		End Function

		''' <summary>
		'''     16进制字符串转16进制字节数组
		''' </summary>
		''' <param name="hexString">16进制字符串</param>
		''' <returns>16进制字节数组</returns>
		''' <remarks>字符串转16进制字节数组</remarks>
		Public Shared Function HexStringToHexByte(hexString As String) As Byte()
			HexString = HexString.Replace(" ", "")
			If HexString.Length Mod 2 <> 0 Then
				HexString += " "
			End If
			Dim numberChars As Integer = HexString.Length
			Dim returnBytes = New Byte(NumberChars / 2 - 1) {}
			For i = 0 To NumberChars - 2 Step 2
				returnBytes(i / 2) = Convert.ToByte(HexString.Substring(i, 2), 16)
			Next
			Return returnBytes
		End Function

		''' <summary>
		'''     字节数组转16进制字符串
		''' </summary>
		''' <param name="bytes">字节数组</param>
		''' <returns>16进制字符串</returns>
		''' <remarks>字节数组转16进制字符串</remarks>
		Public Shared Function ByteToHexString(bytes As Byte()) As String
			Dim returnStr As String = String.Empty
			If bytes IsNot Nothing Then
				For i = 0 To bytes.Length - 1
					returnStr += bytes(i).ToString("X2")
				Next
			End If
			Return returnStr
		End Function


		''' <summary>
		'''     字符串转16进制字符串
		''' </summary>
		''' <param name="sourceString">原字符串</param>
		''' <param name="encode">编码</param>
		''' <param name="split">是否分割</param>
		''' <returns>16进制字符串</returns>
		''' <remarks>字符串转16进制字符串</remarks>
		Public Shared Function ToHex(sourceString As String, encode As Encoding, split As Boolean) As String
			If sourceString Mod 2 <> 0 Then
				sourceString += " "
			End If
			Dim bytes As Byte() = encode.GetBytes(SourceString)
			Dim str As String = String.Empty
			For i = 0 To bytes.Length - 1
				str += $"{bytes(i):X}"
				If split = True And i <> bytes.Length - 1 Then
					str += $"{"," }"
				End If
			Next
			Return Str.ToLower
		End Function

		''' <summary>
		'''     将字节数组转换成字符串输出
		''' </summary>
		''' <param name="sourceDate">字节数组</param>
		''' <param name="split">是否分割</param>
		''' <returns>字符串</returns>
		''' <remarks>将字节数组转换成字符串输出</remarks>
		Public Shared Function ToListString(sourceDate As Byte(), split As Boolean) As String
			Dim str As String = String.Empty
			For i = 0 To SourceDate.Length - 1
				str += $"{sourceDate(i) }"
				If Split = True And i <> SourceDate.Length - 1 Then
					str += $"{"," }"
				End If
			Next
			Return Str.ToLower
		End Function

		''' <summary>
		'''     16进制字符串转字符串
		''' </summary>
		''' <param name="hexString">16进制字符串</param>
		''' <param name="encode">编码</param>
		''' <returns>字符串</returns>
		''' <remarks>16进制字符串转字符串</remarks>
		Public Shared Function UnHex(hexString As String, encode As Encoding) As String
			If HexString Is Nothing Then
				Throw New ArgumentException("Hex")
			End If
			HexString = HexString.Replace(",", "")
			HexString = HexString.Replace("\n", "")
			HexString = HexString.Replace("\\", "")
			HexString = HexString.Replace(" ", "")
			If HexString Mod 2 <> 0 Then
				HexString += "20"
			End If
			'需要将 hex 转换成 byte 数组
			Dim bytes = New Byte(HexString.Length) {}
			For i = 0 To bytes.Length - 1
				Try
					'每两个字符是一个 byte
					bytes(i) = Byte.Parse(HexString.Substring(i * 2, 2), NumberStyles.HexNumber)
				Catch ex As Exception
					Throw New ArgumentException("hex is not a valid hex number!", "hex")
				End Try
			Next
			Return encode.GetString(bytes)
		End Function

		''' <summary>
		'''     获取字节数组
		''' </summary>
		''' <param name="sourceString">原字符串</param>
		''' <param name="encode">编码</param>
		''' <param name="spliteLength">截取长度</param>
		''' <returns>字节数组</returns>
		''' <remarks>获取字节数组</remarks>
		Public Shared Function GetBytes(sourceString As String, encode As Encoding, Optional spliteLength As Integer = 0) As Byte()
			Dim sourceByte As Byte() = encode.GetBytes(SourceString)
			If spliteLength = 0 Then
				Return SourceByte
			End If
			Dim bytes = New Byte(spliteLength - 1) {}
			For i = 0 To bytes.Length - 1
				If i < SourceByte.Length Then
					bytes(i) = SourceByte(i)
				End If
			Next
			Return bytes
		End Function


		''' <summary>
		'''     从字节数组获取字符串
		''' </summary>
		''' <param name="sourceData">数据源</param>
		''' <param name="encode">编码</param>
		''' <returns>字符串</returns>
		''' <remarks>从字节数组获取字符串</remarks>
		Public Shared Function GetBytesString(sourceData As Byte(), encode As Encoding) As String
			Return encode.GetString(SourceData)
		End Function

		Public Shared Function GetBase64Str(sourceStr As string) As String
		    Dim b as Byte()=Encoding.Default.GetBytes(sourceStr)
		    Return Convert.ToBase64String(b)
		End Function
        
		''' <summary>
		'''     获取Base64字符串
		''' </summary>
		''' <param name="sourceData">数据源</param>
		''' <returns>Base64字符串</returns>
		''' <remarks>获取Base64字符串</remarks>
		Public Shared Function GetBase64String(sourceData As Byte()) As String
			Return Convert.ToBase64String(SourceData)
		End Function

		''' <summary>
		'''     加载Base64字符串
		''' </summary>
		''' <param name="base64String">Base64字符串</param>
		''' <returns>字节数组</returns>
		''' <remarks>加载Base64字符串</remarks>
		Public Shared Function LoadBase64String(base64String As String) As Byte()
			Return Convert.FromBase64String(Base64String)
		End Function

		Public Shared Function PrintByte(sourceData As Byte()) As String
			Dim strBuff As String = String.Empty
			For Each b As Byte In sourceData
				strBuff += $"{b.ToString } "
			Next
			Return strBuff
		End Function
	End Class
End Namespace
