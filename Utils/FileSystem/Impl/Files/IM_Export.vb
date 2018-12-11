Imports System.Text
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Utils.FileSystem.Impl.Files
	Public Class IM_Export
		Public Shared Sub ExportFile(fileTxt As String, defaultName As String, showDialog As Boolean)
			Dim defaultExportPath As String = $"{IM_AppPath.GetPath }\Export"
			IM_AppPath.IsExistAndCreate(DefaultExportPath)
			Dim fileName As String = $"{defaultExportPath }\{defaultName }"
			IO.File.WriteAllText(fileName, FileTxt, Encoding.UTF8)
			Dim showTxt As New StringBuilder
			showTxt.AppendLine("导出完成！")
			showTxt.AppendLine($"文件保存在：{fileName }")
			If showDialog = False Then
	            IM_Log.Showlog(showTxt.ToString, MsgType.InfoMsg)
			Else
			    IM_Log.Showlog(showTxt.ToString, MsgType.InfoMsg)
			End If
		End Sub

		Public Shared Sub SaveConfig(ByVal ConfigTxt As String)
		    Dim defaultExportPath As String = $"{IM_AppPath.GetPath }"
		    IM_AppPath.IsExistAndCreate(DefaultExportPath)
		    Dim fileName As String = $"{defaultExportPath }\TapdCollect.config"
		    IO.File.WriteAllText(fileName, ConfigTxt, Encoding.UTF8)
		    IM_Log.Showlog($"配置文件生成成功！", MsgType.InfoMsg)
		End Sub
	End Class
End Namespace