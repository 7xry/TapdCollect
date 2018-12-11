Imports TapdCollect.Utils.FileSystem.Impl.Date

Namespace Tapd.Bug.Model
    Public Class MD_Tapd_Bugs
        ''' <summary>
        ''' 系统编号
        ''' </summary>
        ''' <returns>sysid [ 系统编号 ] By String</returns>
        Property sysid As String

        ''' <summary>
        ''' ID
        ''' </summary>
        ''' <returns>id [ ID ] By String</returns>
        Property id As String
        ''' <summary>
        ''' 标题
        ''' </summary>
        ''' <returns>title [ 标题 ] By String</returns>
        Property title As String
        ''' <summary>
        ''' 详细描述
        ''' </summary>
        ''' <returns>description [ 详细描述 ] By String</returns>
        Property description As String
        ''' <summary>
        ''' 优先级
        ''' </summary>
        ''' <returns>priority [ 优先级 ] By String</returns>
        Property priority As String
        ''' <summary>
        ''' 严重程度
        ''' </summary>
        ''' <returns>severity [ 严重程度 ] By String</returns>
        Property severity As String
        ''' <summary>
        ''' 模块
        ''' </summary>
        ''' <returns>module [ 模块 ] By String</returns>
        Property [module] As String
        ''' <summary>
        ''' 状态
        ''' </summary>
        ''' <returns>status [ 状态 ] By String</returns>
        Property status As String
        ''' <summary>
        ''' 创建人
        ''' </summary>
        ''' <returns>reporter [ 创建人 ] By String</returns>
        Property reporter As String
        ''' <summary>
        ''' 解决期限
        ''' </summary>
        ''' <returns>deadline [ 解决期限 ] By String</returns>
        Property deadline As String
        ''' <summary>
        ''' 创建时间
        ''' </summary>
        ''' <returns>created [ 创建时间 ] By String</returns>
        Property created As String
        ''' <summary>
        ''' 缺陷类型
        ''' </summary>
        ''' <returns>bugtype [ 缺陷类型 ] By String</returns>
        Property bugtype As String
        ''' <summary>
        ''' 解决时间
        ''' </summary>
        ''' <returns>resolved [ 解决时间 ] By String</returns>
        Property resolved As String
        ''' <summary>
        ''' 关闭时间
        ''' </summary>
        ''' <returns>closed [ 关闭时间 ] By String</returns>
        Property closed As String
        ''' <summary>
        ''' 最后修改时间
        ''' </summary>
        ''' <returns>modified [ 最后修改时间 ] By String</returns>
        Property modified As String
        ''' <summary>
        ''' 最后修改人
        ''' </summary>
        ''' <returns>lastmodify [ 最后修改人 ] By String</returns>
        Property lastmodify As String
        ''' <summary>
        ''' 审核人
        ''' </summary>
        ''' <returns>auditer [ 审核人 ] By String</returns>
        Property auditer As String
        ''' <summary>
        ''' 开发人员
        ''' </summary>
        ''' <returns>de [ 开发人员 ] By String</returns>
        Property de As String
        ''' <summary>
        ''' 验证版本
        ''' </summary>
        ''' <returns>version_test [ 验证版本 ] By String</returns>
        Property version_test As String
        ''' <summary>
        ''' 发现版本
        ''' </summary>
        ''' <returns>version_report [ 发现版本 ] By String</returns>
        Property version_report As String
        ''' <summary>
        ''' 关闭版本
        ''' </summary>
        ''' <returns>version_close [ 关闭版本 ] By String</returns>
        Property version_close As String
        ''' <summary>
        ''' 合入版本
        ''' </summary>
        ''' <returns>version_fix [ 合入版本 ] By String</returns>
        Property version_fix As String
        ''' <summary>
        ''' 发现基线
        ''' </summary>
        ''' <returns>baseline_find [ 发现基线 ] By String</returns>
        Property baseline_find As String
        ''' <summary>
        ''' 合入基线
        ''' </summary>
        ''' <returns>baseline_join [ 合入基线 ] By String</returns>
        Property baseline_join As String
        ''' <summary>
        ''' 关闭基线
        ''' </summary>
        ''' <returns>baseline_close [ 关闭基线 ] By String</returns>
        Property baseline_close As String
        ''' <summary>
        ''' 验证基线
        ''' </summary>
        ''' <returns>baseline_test [ 验证基线 ] By String</returns>
        Property baseline_test As String
        ''' <summary>
        ''' 引入阶段
        ''' </summary>
        ''' <returns>sourcephase [ 引入阶段 ] By String</returns>
        Property sourcephase As String
        ''' <summary>
        ''' 测试人员
        ''' </summary>
        ''' <returns>te [ 测试人员 ] By String</returns>
        Property te As String
        ''' <summary>
        ''' 当前处理人
        ''' </summary>
        ''' <returns>current_owner [ 当前处理人 ] By String</returns>
        Property current_owner As String
        ''' <summary>
        ''' 迭代
        ''' </summary>
        ''' <returns>iteration_id [ 迭代 ] By String</returns>
        Property iteration_id As String
        ''' <summary>
        ''' 解决方法
        ''' </summary>
        ''' <returns>resolution [ 解决方法 ] By String</returns>
        Property resolution As String
        ''' <summary>
        ''' 缺陷根源
        ''' </summary>
        ''' <returns>source [ 缺陷根源 ] By String</returns>
        Property source As String
        ''' <summary>
        ''' 发现阶段
        ''' </summary>
        ''' <returns>originphase [ 发现阶段 ] By String</returns>
        Property originphase As String
        ''' <summary>
        ''' 验证人
        ''' </summary>
        ''' <returns>confirmer [ 验证人 ] By String</returns>
        Property confirmer As String
        ''' <summary>
        ''' 里程碑
        ''' </summary>
        ''' <returns>milestone [ 里程碑 ] By String</returns>
        Property milestone As String
        ''' <summary>
        ''' 参与人
        ''' </summary>
        ''' <returns>participator [ 参与人 ] By String</returns>
        Property participator As String
        ''' <summary>
        ''' 关闭人
        ''' </summary>
        ''' <returns>closer [ 关闭人 ] By String</returns>
        Property closer As String
        ''' <summary>
        ''' 软件平台
        ''' </summary>
        ''' <returns>platform [ 软件平台 ] By String</returns>
        Property platform As String
        ''' <summary>
        ''' 操作系统
        ''' </summary>
        ''' <returns>os [ 操作系统 ] By String</returns>
        Property os As String
        ''' <summary>
        ''' 测试类型
        ''' </summary>
        ''' <returns>testtype [ 测试类型 ] By String</returns>
        Property testtype As String
        ''' <summary>
        ''' 测试阶段
        ''' </summary>
        ''' <returns>testphase [ 测试阶段 ] By String</returns>
        Property testphase As String
        ''' <summary>
        ''' 重现规律
        ''' </summary>
        ''' <returns>frequency [ 重现规律 ] By String</returns>
        Property frequency As String
        ''' <summary>
        ''' 抄送人
        ''' </summary>
        ''' <returns>cc [ 抄送人 ] By String</returns>
        Property cc As String
        ''' <summary>
        ''' 修订编号
        ''' </summary>
        ''' <returns>regression_number [ 修订编号 ] By String</returns>
        Property regression_number As String
        ''' <summary>
        ''' 流动
        ''' </summary>
        ''' <returns>flows [ 流动 ] By String</returns>
        Property flows As String
        ''' <summary>
        ''' 特征
        ''' </summary>
        ''' <returns>feature [ 特征 ] By String</returns>
        Property feature As String
        ''' <summary>
        ''' 测试方式
        ''' </summary>
        ''' <returns>testmode [ 测试方式 ] By String</returns>
        Property testmode As String
        ''' <summary>
        ''' 预估工时
        ''' </summary>
        ''' <returns>estimate [ 预估工时 ] By String</returns>
        Property estimate As String
        ''' <summary>
        ''' 确认者ID
        ''' </summary>
        ''' <returns>issue_id [ 确认者ID ] By String</returns>
        Property issue_id As String
        ''' <summary>
        ''' 从xx中创建
        ''' </summary>
        ''' <returns>created_from [ 从xx中创建 ] By String</returns>
        Property created_from As String
        ''' <summary>
        ''' 接受处理时间
        ''' </summary>
        ''' <returns>in_progress_time [ 接受处理时间 ] By String</returns>
        Property in_progress_time As String
        ''' <summary>
        ''' 验证时间
        ''' </summary>
        ''' <returns>verify_time [ 验证时间 ] By String</returns>
        Property verify_time As String
        ''' <summary>
        ''' 拒绝时间
        ''' </summary>
        ''' <returns>reject_time [ 拒绝时间 ] By String</returns>
        Property reject_time As String
        ''' <summary>
        ''' 重新打开时间
        ''' </summary>
        ''' <returns>reopen_time [ 重新打开时间 ] By String</returns>
        Property reopen_time As String
        ''' <summary>
        ''' 审核时间
        ''' </summary>
        ''' <returns>audit_time [ 审核时间 ] By String</returns>
        Property audit_time As String
        ''' <summary>
        ''' 挂起时间
        ''' </summary>
        ''' <returns>suspend_time [ 挂起时间 ] By String</returns>
        Property suspend_time As String
        ''' <summary>
        ''' 预计结束
        ''' </summary>
        ''' <returns>due [ 预计结束 ] By String</returns>
        Property due As String
        ''' <summary>
        ''' 预计开始
        ''' </summary>
        ''' <returns>begin [ 预计开始 ] By String</returns>
        Property begin As String
        ''' <summary>
        ''' 发布编号
        ''' </summary>
        ''' <returns>release_id [ 发布编号 ] By String</returns>
        Property release_id As String
        ''' <summary>
        ''' 自定义字段1
        ''' </summary>
        ''' <returns>custom_field_one [ 自定义字段1 ] By String</returns>
        Property custom_field_one As String
        ''' <summary>
        ''' 自定义字段2
        ''' </summary>
        ''' <returns>custom_field_two [ 自定义字段2 ] By String</returns>
        Property custom_field_two As String
        ''' <summary>
        ''' 自定义字段3
        ''' </summary>
        ''' <returns>custom_field_three [ 自定义字段3 ] By String</returns>
        Property custom_field_three As String
        ''' <summary>
        ''' 自定义字段4
        ''' </summary>
        ''' <returns>custom_field_four [ 自定义字段4 ] By String</returns>
        Property custom_field_four As String
        ''' <summary>
        ''' 自定义字段5
        ''' </summary>
        ''' <returns>custom_field_five [ 自定义字段5 ] By String</returns>
        Property custom_field_five As String
        ''' <summary>
        ''' 自定义字段6
        ''' </summary>
        ''' <returns>custom_field_6 [ 自定义字段6 ] By String</returns>
        Property custom_field_6 As String
        ''' <summary>
        ''' 自定义字段7
        ''' </summary>
        ''' <returns>custom_field_7 [ 自定义字段7 ] By String</returns>
        Property custom_field_7 As String
        ''' <summary>
        ''' 自定义字段8
        ''' </summary>
        ''' <returns>custom_field_8 [ 自定义字段8 ] By String</returns>
        Property custom_field_8 As String
        ''' <summary>
        ''' 自定义字段9
        ''' </summary>
        ''' <returns>custom_field_9 [ 自定义字段9 ] By String</returns>
        Property custom_field_9 As String
        ''' <summary>
        ''' 自定义字段10
        ''' </summary>
        ''' <returns>custom_field_10 [ 自定义字段10 ] By String</returns>
        Property custom_field_10 As String
        ''' <summary>
        ''' 自定义字段11
        ''' </summary>
        ''' <returns>custom_field_11 [ 自定义字段11 ] By String</returns>
        Property custom_field_11 As String
        ''' <summary>
        ''' 自定义字段12
        ''' </summary>
        ''' <returns>custom_field_12 [ 自定义字段12 ] By String</returns>
        Property custom_field_12 As String
        ''' <summary>
        ''' 自定义字段13
        ''' </summary>
        ''' <returns>custom_field_13 [ 自定义字段13 ] By String</returns>
        Property custom_field_13 As String
        ''' <summary>
        ''' 自定义字段14
        ''' </summary>
        ''' <returns>custom_field_14 [ 自定义字段14 ] By String</returns>
        Property custom_field_14 As String
        ''' <summary>
        ''' 自定义字段15
        ''' </summary>
        ''' <returns>custom_field_15 [ 自定义字段15 ] By String</returns>
        Property custom_field_15 As String
        ''' <summary>
        ''' 自定义字段16
        ''' </summary>
        ''' <returns>custom_field_16 [ 自定义字段16 ] By String</returns>
        Property custom_field_16 As String
        ''' <summary>
        ''' 自定义字段17
        ''' </summary>
        ''' <returns>custom_field_17 [ 自定义字段17 ] By String</returns>
        Property custom_field_17 As String
        ''' <summary>
        ''' 自定义字段18
        ''' </summary>
        ''' <returns>custom_field_18 [ 自定义字段18 ] By String</returns>
        Property custom_field_18 As String
        ''' <summary>
        ''' 自定义字段19
        ''' </summary>
        ''' <returns>custom_field_19 [ 自定义字段19 ] By String</returns>
        Property custom_field_19 As String
        ''' <summary>
        ''' 自定义字段20
        ''' </summary>
        ''' <returns>custom_field_20 [ 自定义字段20 ] By String</returns>
        Property custom_field_20 As String
        ''' <summary>
        ''' 自定义字段21
        ''' </summary>
        ''' <returns>custom_field_21 [ 自定义字段21 ] By String</returns>
        Property custom_field_21 As String
        ''' <summary>
        ''' 自定义字段22
        ''' </summary>
        ''' <returns>custom_field_22 [ 自定义字段22 ] By String</returns>
        Property custom_field_22 As String
        ''' <summary>
        ''' 自定义字段23
        ''' </summary>
        ''' <returns>custom_field_23 [ 自定义字段23 ] By String</returns>
        Property custom_field_23 As String
        ''' <summary>
        ''' 自定义字段24
        ''' </summary>
        ''' <returns>custom_field_24 [ 自定义字段24 ] By String</returns>
        Property custom_field_24 As String
        ''' <summary>
        ''' 自定义字段25
        ''' </summary>
        ''' <returns>custom_field_25 [ 自定义字段25 ] By String</returns>
        Property custom_field_25 As String
        ''' <summary>
        ''' 自定义字段26
        ''' </summary>
        ''' <returns>custom_field_26 [ 自定义字段26 ] By String</returns>
        Property custom_field_26 As String
        ''' <summary>
        ''' 自定义字段27
        ''' </summary>
        ''' <returns>custom_field_27 [ 自定义字段27 ] By String</returns>
        Property custom_field_27 As String
        ''' <summary>
        ''' 自定义字段28
        ''' </summary>
        ''' <returns>custom_field_28 [ 自定义字段28 ] By String</returns>
        Property custom_field_28 As String
        ''' <summary>
        ''' 自定义字段29
        ''' </summary>
        ''' <returns>custom_field_29 [ 自定义字段29 ] By String</returns>
        Property custom_field_29 As String
        ''' <summary>
        ''' 自定义字段30
        ''' </summary>
        ''' <returns>custom_field_30 [ 自定义字段30 ] By String</returns>
        Property custom_field_30 As String
        ''' <summary>
        ''' 自定义字段31
        ''' </summary>
        ''' <returns>custom_field_31 [ 自定义字段31 ] By String</returns>
        Property custom_field_31 As String
        ''' <summary>
        ''' 自定义字段32
        ''' </summary>
        ''' <returns>custom_field_32 [ 自定义字段32 ] By String</returns>
        Property custom_field_32 As String
        ''' <summary>
        ''' 自定义字段33
        ''' </summary>
        ''' <returns>custom_field_33 [ 自定义字段33 ] By String</returns>
        Property custom_field_33 As String
        ''' <summary>
        ''' 自定义字段34
        ''' </summary>
        ''' <returns>custom_field_34 [ 自定义字段34 ] By String</returns>
        Property custom_field_34 As String
        ''' <summary>
        ''' 自定义字段35
        ''' </summary>
        ''' <returns>custom_field_35 [ 自定义字段35 ] By String</returns>
        Property custom_field_35 As String
        ''' <summary>
        ''' 自定义字段36
        ''' </summary>
        ''' <returns>custom_field_36 [ 自定义字段36 ] By String</returns>
        Property custom_field_36 As String
        ''' <summary>
        ''' 自定义字段37
        ''' </summary>
        ''' <returns>custom_field_37 [ 自定义字段37 ] By String</returns>
        Property custom_field_37 As String
        ''' <summary>
        ''' 自定义字段38
        ''' </summary>
        ''' <returns>custom_field_38 [ 自定义字段38 ] By String</returns>
        Property custom_field_38 As String
        ''' <summary>
        ''' 自定义字段39
        ''' </summary>
        ''' <returns>custom_field_39 [ 自定义字段39 ] By String</returns>
        Property custom_field_39 As String
        ''' <summary>
        ''' 自定义字段40
        ''' </summary>
        ''' <returns>custom_field_40 [ 自定义字段40 ] By String</returns>
        Property custom_field_40 As String
        ''' <summary>
        ''' 自定义字段41
        ''' </summary>
        ''' <returns>custom_field_41 [ 自定义字段41 ] By String</returns>
        Property custom_field_41 As String
        ''' <summary>
        ''' 自定义字段42
        ''' </summary>
        ''' <returns>custom_field_42 [ 自定义字段42 ] By String</returns>
        Property custom_field_42 As String
        ''' <summary>
        ''' 自定义字段43
        ''' </summary>
        ''' <returns>custom_field_43 [ 自定义字段43 ] By String</returns>
        Property custom_field_43 As String
        ''' <summary>
        ''' 自定义字段44
        ''' </summary>
        ''' <returns>custom_field_44 [ 自定义字段44 ] By String</returns>
        Property custom_field_44 As String
        ''' <summary>
        ''' 自定义字段45
        ''' </summary>
        ''' <returns>custom_field_45 [ 自定义字段45 ] By String</returns>
        Property custom_field_45 As String
        ''' <summary>
        ''' 自定义字段46
        ''' </summary>
        ''' <returns>custom_field_46 [ 自定义字段46 ] By String</returns>
        Property custom_field_46 As String
        ''' <summary>
        ''' 自定义字段47
        ''' </summary>
        ''' <returns>custom_field_47 [ 自定义字段47 ] By String</returns>
        Property custom_field_47 As String
        ''' <summary>
        ''' 自定义字段48
        ''' </summary>
        ''' <returns>custom_field_48 [ 自定义字段48 ] By String</returns>
        Property custom_field_48 As String
        ''' <summary>
        ''' 自定义字段49
        ''' </summary>
        ''' <returns>custom_field_49 [ 自定义字段49 ] By String</returns>
        Property custom_field_49 As String
        ''' <summary>
        ''' 自定义字段50
        ''' </summary>
        ''' <returns>custom_field_50 [ 自定义字段50 ] By String</returns>
        Property custom_field_50 As String
        ''' <summary>
        ''' 项目ID
        ''' </summary>
        ''' <returns>workspace_id [ 项目ID ] By String</returns>
        Property workspace_id As String
        ''' <summary>
        ''' 缺陷地址
        ''' </summary>
        ''' <returns>collect_date [ 缺陷地址 ] By String</returns>
        Property url As String
        ''' <summary>
        ''' 采集日期
        ''' </summary>
        ''' <returns>collect_date [ 采集日期 ] By String</returns>
        Property collect_date As String


    End Class
End NameSpace