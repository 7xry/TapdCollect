Imports System.IO
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Log

Namespace Utils.FileSystem.Impl.Path
    Public Class IM_AppPath
        Public Shared Function GetPath() As String
            Return Environment.CurrentDirectory
        End Function
        Public Shared Function NewLine() As String
            Return Constants.vbCrLf
        End Function

        Public Shared Function IsExistPath(path As String) As Boolean
            If Directory.Exists(path) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function IsExistFile(FilePath As String) As Boolean
            If File.Exists(FilePath) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function RemovePath(path As String) As Boolean
            Try
                If IsExistPath(path) = True Then
                    Directory.Delete(path, True)
                End If
                Return True
            Catch ex As Exception
                IM_Log.Showlog(ex.ToString, MsgType.ErrorMsg)
                Return False
            End Try
        End Function

        Public Shared Sub IsExistAndCreate(path As String)
            If Directory.Exists(path) = False Then
                Directory.CreateDirectory(path)
            End If
        End Sub

        ''' <summary>
        '''     判定文件是否被占用
        ''' </summary>
        ''' <param name="fileName">文件路径</param>
        ''' <returns>True Or False</returns>
        ''' <remarks>true表示正在使用,false没有使用</remarks>
        Public Shared Function IsFileInUse(fileName As String) As Boolean
            Dim inUse = True
            Dim fs As FileStream = Nothing
            Try
                fs = New FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None)
                inUse = False
            Catch
            Finally
                If fs IsNot Nothing Then
                    fs.Close()
                End If
            End Try
            Return inUse
        End Function

        ''' <summary>
        '''     删除指定目录下所有内容：方法二--找到所有文件和子文件夹删除  //转载请注明来自 http://www.shang11.com
        ''' </summary>
        ''' <param name="dirPath"></param>
        Public Shared Sub DeleteFolder(dirPath As String)
            Try
                If Directory.Exists(dirPath) Then
                    For Each content As String In Directory.GetFileSystemEntries(dirPath)
                        If Directory.Exists(content) Then
                            Directory.Delete(content, True)
                        ElseIf File.Exists(content) Then
                            File.Delete(content)
                        End If
                    Next
                End If
            Catch ex As Exception
                IM_Log.Showlog(ex.ToString, MsgType.ErrorMsg)
            End Try
        End Sub

        Public Shared Sub DeleteFolder(dirPath As String, Optional ByVal IsDelOwner As Boolean = False)
            Try
                If Directory.Exists(dirPath) Then
                    For Each content As String In Directory.GetFileSystemEntries(dirPath)
                        If Directory.Exists(content) Then
                            Directory.Delete(content, True)
                        ElseIf File.Exists(content) Then
                            File.Delete(content)
                        End If
                    Next
                    If IsDelOwner = True Then
                        Directory.Delete(dirPath, True)
                    End If
                End If
            Catch ex As Exception
                IM_Log.Showlog(ex.ToString, MsgType.ErrorMsg)
            End Try
        End Sub
    End Class
End Namespace
