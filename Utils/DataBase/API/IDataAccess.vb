Imports System.Data
Imports TapdCollect.Utils.DataBase.Model

Namespace Utils.DataBase.API
	Public Interface IDataAccess
		'打开数据库连接
		Sub Open()
		'关闭数据库连接
		Sub Close()
		'开始事务
		Sub BeginTran()
		'提交事务
		Sub CommitTran()
		'回滚事务
		Sub RollBackTran()
		'执行增删改
		Function ExecuteNonQuery(sql As String, ByVal ParamArray para As QueryParameter()) As Integer
		'返回单个值
		Function ExecuteScalar(sql As String, ByVal ParamArray para As QueryParameter()) As Object
		'返回查询结果表格
		Function GetTable(sql As String, ByVal ParamArray para As QueryParameter()) As DataTable
		'返回一个DataReader
		Function GetReader(sql As String, ByVal ParamArray para As QueryParameter()) As IDataReader
	End Interface
End Namespace