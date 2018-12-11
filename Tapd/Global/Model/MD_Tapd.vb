Imports Newtonsoft.Json.Linq

Namespace Tapd.Global.Model
    Public Class MD_Tapd
        ''' <summary>
        ''' 状态
        ''' </summary>
        ''' <returns>status [ 状态 ] By Jobject</returns>
        Property status As JValue
        ''' <summary>
        ''' 数据
        ''' </summary>
        ''' <returns>data [ 数据 ] By Jobject</returns>
        Property data As Object
        ''' <summary>
        ''' 消息
        ''' </summary>
        ''' <returns>info [ 消息 ] By Jobject</returns>
        Property info As JValue
    End Class
End Namespace