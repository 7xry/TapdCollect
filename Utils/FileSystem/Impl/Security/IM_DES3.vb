Imports System.IO
Imports System.Numerics
Imports System.Security.Cryptography
Imports System.Text
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.String

Namespace Utils.FileSystem.Impl.Security
	Public Class IM_DES3
		'默认密钥
		Public Shared Property DefaultKey As String = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDRwrqwknhOA44knSjvQ2NvaXzpDc3n607NAC6pfZdtIz1N06VZBPiVxzBtvQMSB+4H72GsS00NgzE7CGxWVfLx7c/4m3F5rDeaqAotdiAFcAdUWCerqN9175QvExgISsEbKERxly50ZHQUxJBuAdbozCbeeqB+SGQMpzeAqYyRFwIDAQAB"
		'默认向量
		Public Shared Property DefaultVector As String = "01234567"

		''' <summary>
		'''     DES加密
		''' </summary>
		''' <param name="data">要加密的数据</param>
		''' <param name="key">密钥</param>
		''' <param name="vector">向量</param>
		''' <returns>加密后的数据</returns>
		''' <remarks>DES加密</remarks>
		Private Shared Function Encrypt(data As Byte(), key As Byte(), vector As Byte()) As Byte()
			Dim cryptograph As Byte() = Nothing
			' 加密后的密文  
			Dim des As New TripleDESCryptoServiceProvider With {
				    .Mode = CipherMode.CBC,
				    .Padding = PaddingMode.PKCS7
				    }
			' 开辟一块内存流  
			Try
				Using memory As New MemoryStream()
					' 把内存流对象包装成加密流对象  
					Using encryptor As New CryptoStream(Memory, Des.CreateEncryptor(Key, Vector), CryptoStreamMode.Write)
						' 明文数据写入加密流  
						Encryptor.Write(Data, 0, Data.Length)
						Encryptor.FlushFinalBlock()
						Cryptograph = Memory.ToArray()
					End Using
				End Using
			Catch ex As Exception
				Cryptograph = Nothing
				Throw ex
			End Try
			Return Cryptograph
		End Function

		''' <summary>
		'''     DES解密
		''' </summary>
		''' <param name="encryptedData">要解密的数据</param>
		''' <param name="key">密钥</param>
		''' <param name="vector">向量</param>
		''' <returns>解密后的数据</returns>
		''' <remarks>DES解密</remarks>
		Private Shared Function Decrypt(encryptedData As Byte(), key As Byte(), vector As Byte()) As Byte()
			Dim original As Byte() = Nothing
			Dim des As New TripleDESCryptoServiceProvider With {
				    .Mode = CipherMode.CBC,
				    .Padding = PaddingMode.PKCS7
				    }
			'开辟一块内存流，存储密文
			Try
				Using memory As New MemoryStream(EncryptedData)
					'把内存流对象包装成加密流对象
					Using decryptor As New CryptoStream(memory, Des.CreateDecryptor(Key, Vector), CryptoStreamMode.Read)
						Using originalMemory As New MemoryStream
							Dim buffer = New Byte(1024) {}
							Dim readBytes As Integer = Decryptor.Read(Buffer, 0, Buffer.Length)
							While readBytes > 0
								originalMemory.Write(Buffer, 0, readBytes)
								readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)
							End While
							original = originalMemory.ToArray()
						End Using
					End Using
				End Using
			Catch ex As Exception
				original = Nothing
				Throw ex
			End Try
			Return original
		End Function

#Region "密钥/向量处理"

		''' <summary>
		'''     生成密钥
		''' </summary>
		''' <param name="key">密钥明文</param>
		''' <param name="encode">编码</param>
		''' <returns>密钥数组</returns>
		''' <remarks>生成密钥</remarks>
		Private Shared Function GeneralKey(key As String, encode As Encoding) As Byte()
			Dim md5Key As String = IM_Md5.MD5(key).ToUpper
			Dim keyBytes As Byte() = encode.GetBytes(Md5Key)
			Dim bytes = New Byte(23) {}
			For i = 0 To bytes.Length - 1
				If i < keyBytes.Length - 1 Then
					bytes(i) = keyBytes(i)
				End If
			Next
			Return bytes
		End Function

		''' <summary>
		'''     生成密钥
		''' </summary>
		''' <param name="key">密钥明文</param>
		''' <returns>密钥数组</returns>
		''' <remarks>生成密钥</remarks>
		Private Shared Function GeneralKey(key As String) As Byte()
			If key IsNot Nothing Then
				Return GeneralKey(key, Encoding.UTF8)
			Else
				Return GeneralKey(DefaultKey, Encoding.UTF8)
			End If
		End Function


		''' <summary>
		'''     生成向量
		''' </summary>
		''' <param name="vector">向量文本</param>
		''' <returns>向量数组</returns>
		''' <remarks>生成向量</remarks>
		Private Shared Function GeneralVector(vector As String) As Byte()
			If Vector IsNot Nothing Then
				Return IM_String.GetBytes(Vector, Encoding.UTF8)
			Else
				Return IM_String.GetBytes(DefaultVector, Encoding.UTF8)
			End If
		End Function

#End Region


		''' <summary>
		'''     DES加密（16进制）
		''' </summary>
		''' <param name="data">要加密的数据</param>
		''' <param name="key">密钥明文</param>
		''' <param name="vector">向量</param>
		''' <returns>16进制文本</returns>
		''' <remarks>DES加密（16进制）</remarks>
		Public Shared Function EncryptHexStr(data As String, Optional key As String = Nothing, Optional vector As String = Nothing) As String
			Dim sourceData As Byte() = IM_String.GetBytes(Data, Encoding.UTF8)
			Dim keyData As Byte() = GeneralKey(Key)
			Dim vectorData As Byte() = GeneralVector(Vector)
			Dim bytes As Byte() = Encrypt(SourceData, KeyData, VectorData)
			Return IM_String.ByteToHexString(bytes)
		End Function


		''' <summary>
		'''     DES加密（Base64）
		''' </summary>
		''' <param name="data">要加密的数据</param>
		''' <param name="key">密钥明文</param>
		''' <param name="vector">向量</param>
		''' <returns>Base64文本</returns>
		''' <remarks>DES加密（Base64）</remarks>
		Public Shared Function EncryptBase64Str(data As String, Optional key As String = Nothing, Optional vector As String = Nothing) As String
			Dim sourceData As Byte() = IM_String.GetBytes(Data, Encoding.UTF8)
			Dim keyData As Byte() = GeneralKey(Key)
			Dim vectorData As Byte() = GeneralVector(Vector)
			Dim bytes As Byte() = Encrypt(SourceData, KeyData, VectorData)
			Return IM_String.GetBase64String(bytes)
		End Function

		''' <summary>
		'''     DES解密（16进制）
		''' </summary>
		''' <param name="data">要解密密的数据</param>
		''' <param name="key">密钥明文</param>
		''' <param name="vector">向量</param>
		''' <returns>数据明文</returns>
		''' <remarks>DES解密（16进制）</remarks>
		Public Shared Function DecryptHexStr(data As String, Optional key As String = Nothing, Optional vector As String = Nothing) As String
			Dim sourceData As Byte() = IM_String.HexStringToHexByte(Data)
			Dim keyData As Byte() = GeneralKey(Key)
			Dim vectorData As Byte() = GeneralVector(Vector)
			Dim bytes As Byte() = Decrypt(SourceData, KeyData, VectorData)
			Return IM_String.GetBytesString(bytes, Encoding.UTF8)
		End Function

		''' <summary>
		'''     DES解密（Base64）
		''' </summary>
		''' <param name="data">要解密密的数据</param>
		''' <param name="key">密钥明文</param>
		''' <param name="vector">向量</param>
		''' <returns>数据明文</returns>
		''' <remarks>DES解密（Base64）</remarks>
		Public Shared Function DecryptBase64Str(data As String, Optional key As String = Nothing, Optional vector As String = Nothing) As String
			Dim sourceData As Byte() = IM_String.LoadBase64String(Data)
			Dim keyData As Byte() = GeneralKey(Key)
			Dim vectorData As Byte() = GeneralVector(Vector)
			Dim bytes As Byte() = Decrypt(SourceData, KeyData, VectorData)
			Return IM_String.GetBytesString(bytes, Encoding.UTF8)
		End Function


		Private Shared Function AddDesParity(paramArrayOfByte As Byte(), paramInt1 As Integer, paramInt2 As Integer) As Byte()
			If paramInt2 <> 7 Then
                IM_Log.Showlog($"Invalid length of DES Key Value:{paramInt2}",MsgType.ErrorMsg)
			    Return Nothing
			End If
			Dim arrayOfByte1 = New Byte(6) {}
			Array.Copy(paramArrayOfByte, paramInt1, arrayOfByte1, 0, paramInt2)
			Dim arrayOfByte2 = New Byte(7) {}
			Dim localBigInteger = New BigInteger(arrayOfByte1)
			For i As Integer = arrayOfByte2.Length - 1 To 0 Step - 1
				arrayOfByte2(i) = localBigInteger
			Next
			Return arrayOfByte1
		End Function
	End Class
End Namespace