

Namespace Tapd.Comment.Model
    Public Class MD_Tapd_Comments
        ''' <summary>
        ''' 系统编号
        ''' </summary>
        ''' <returns>sysid [ 系统编号 ] By String</returns>
        Property sysid As String
        ''' <summary>
        ''' 评论ID
        ''' </summary>
        ''' <returns>id [ 评论ID ] By String</returns>
        Property id As String
        ''' <summary>
        ''' 标题
        ''' </summary>
        ''' <returns>title [ 标题 ] By String</returns>
        Property title As String
        ''' <summary>
        ''' 内容
        ''' </summary>
        ''' <returns>description [ 内容 ] By String</returns>
        Property description As String
        ''' <summary>
        ''' 评论人
        ''' </summary>
        ''' <returns>author [ 评论人 ] By String</returns>
        Property author As String
        ''' <summary>
        ''' 评论类型（取值： bug、 bug_remark （流转缺陷时候的评论）、 stories、 tasks 。多个类型间以竖线隔开）
        ''' </summary>
        ''' <returns>entry_type [ 评论类型（取值： bug、 bug_remark （流转缺陷时候的评论）、 stories、 tasks 。多个类型间以竖线隔开） ] By String</returns>
        Property entry_type As String
        ''' <summary>
        ''' 评论所依附的业务对象实体id
        ''' </summary>
        ''' <returns>entry_id [ 评论所依附的业务对象实体id ] By String</returns>
        Property entry_id As String
        ''' <summary>
        ''' 创建时间
        ''' </summary>
        ''' <returns>created [ 创建时间 ] By String</returns>
        Property created As String
        ''' <summary>
        ''' 最后更改时间
        ''' </summary>
        ''' <returns>modified [ 最后更改时间 ] By String</returns>
        Property modified As String
        ''' <summary>
        ''' 项目ID
        ''' </summary>
        ''' <returns>workspace_id [ 项目ID ] By String</returns>
        Property workspace_id As String
        ''' <summary>
        ''' 评论地址
        ''' </summary>
        ''' <returns>url [ 评论地址 ] By String</returns>
        Property url As String
        ''' <summary>
        ''' 采集日期
        ''' </summary>
        ''' <returns>collect_date [ 采集日期 ] By String</returns>
        Property collect_date As String

    End Class
End NameSpace