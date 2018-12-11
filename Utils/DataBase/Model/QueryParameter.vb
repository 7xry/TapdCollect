Imports System.Data

Namespace Utils.DataBase.Model
	Public Class QueryParameter
		Property Name As String

		Property Value As Object

		Property DbType As DbType

		Property Direction As ParameterDirection = ParameterDirection.Input

		Sub New(qName As String, qValue As Object, qType As DbType)
			Name = QName
			Value = QValue
			DbType = QType
		End Sub
	End Class
End Namespace