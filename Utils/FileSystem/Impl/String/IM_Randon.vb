Imports System.Security.Cryptography
Imports TapdCollect.Utils.FileSystem.Impl.Date

Namespace Utils.FileSystem.Impl.String
    Public Class IM_Randon
        '****************************************************************************
        '   产生一个6位的随机数，主要用于验证码
        '****************************************************************************
        Public Overloads Shared Function AttachCode() As String
            AttachCode = String.Empty
            Dim rnd As New Random
            Dim codeList() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
            For I = 0 To 5
                AttachCode += codeList(rnd.Next(0, codeList.Length - 1))
            Next
            Return AttachCode
        End Function

        '****************************************************************************
        '   产生一个指定长度（不为0）的随机数，主要用于验证码
        '****************************************************************************
        Public Overloads Shared Function AttachCode(len As Integer) As String
            AttachCode = String.Empty
            Dim rnd As New Random
            Dim codeList() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
            If len <= 0 Then
                Throw New Exception("长度不能小于等于0")
            End If
            For I = 0 To len - 1
                AttachCode += codeList(rnd.Next(0, codeList.Length - 1))
            Next
            Return AttachCode
        End Function

        '****************************************************************************
        '   产生一个指定长度（不为0）的随机数，主要用于验证码'****************************************************************************
        Public Overloads Shared Function AttachCode(len As Integer, isNumStr As Boolean) As String
            AttachCode = String.Empty
            Dim rnd As New Random
            Dim codeList() As String = Nothing
            If isNumStr = True Then
                codeList = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}
            Else
                codeList = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
            End If
            If len <= 0 Then
                Throw New Exception("长度不能小于等于0")
            End If
            For I = 0 To len - 1
                AttachCode += codeList(New Random(GetRandomSeed()).Next(0, codeList.Length - 1))
            Next
            Return AttachCode
        End Function

        Private Shared Function GetRandomSeed() As Integer
            Dim bytes As Byte() = New Byte(4) {}
            Dim rng As New RNGCryptoServiceProvider
            rng.GetBytes(bytes)
            Return BitConverter.ToInt32(bytes, 0)
        End Function

        Public Shared Function GetRandomStr() As String
			Dim generator As New Random
			Dim randomValue As Integer = generator.Next(10000, 99999)
			Return randomValue.ToString & IM_JsDate.GetNowStr("_ssmm")
		End Function
	End Class
End Namespace