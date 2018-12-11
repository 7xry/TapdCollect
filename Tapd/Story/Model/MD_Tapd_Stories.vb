Imports TapdCollect.Utils.FileSystem.Impl.Date

Namespace Tapd.Story.Model
    Public Class MD_Tapd_Stories
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
        ''' <returns>name [ 标题 ] By String</returns>
        Property name As String
        ''' <summary>
        ''' 详细描述
        ''' </summary>
        ''' <returns>description [ 详细描述 ] By String</returns>
        Property description As String
        ''' <summary>
        ''' 项目ID
        ''' </summary>
        ''' <returns>workspace_id [ 项目ID ] By String</returns>
        Property workspace_id As String
        ''' <summary>
        ''' 创建人
        ''' </summary>
        ''' <returns>creator [ 创建人 ] By String</returns>
        Property creator As String
        ''' <summary>
        ''' 创建时间
        ''' </summary>
        ''' <returns>created [ 创建时间 ] By String</returns>
        Property created As String
        ''' <summary>
        ''' 最后修改时间
        ''' </summary>
        ''' <returns>modified [ 最后修改时间 ] By String</returns>
        Property modified As String
        ''' <summary>
        ''' 状态
        ''' </summary>
        ''' <returns>status [ 状态 ] By String</returns>
        Property status As String
        ''' <summary>
        ''' 当前处理人
        ''' </summary>
        ''' <returns>owner [ 当前处理人 ] By String</returns>
        Property owner As String
        ''' <summary>
        ''' 抄送人
        ''' </summary>
        ''' <returns>cc [ 抄送人 ] By String</returns>
        Property cc As String
        ''' <summary>
        ''' 预计开始
        ''' </summary>
        ''' <returns>begin [ 预计开始 ] By String</returns>
        Property begin As String
        ''' <summary>
        ''' 预计结束
        ''' </summary>
        ''' <returns>due [ 预计结束 ] By String</returns>
        Property due As String
        ''' <summary>
        ''' 规模
        ''' </summary>
        ''' <returns>size [ 规模 ] By String</returns>
        Property size As String
        ''' <summary>
        ''' 优先级
        ''' </summary>
        ''' <returns>priority [ 优先级 ] By String</returns>
        Property priority As String
        ''' <summary>
        ''' 开发人员
        ''' </summary>
        ''' <returns>developer [ 开发人员 ] By String</returns>
        Property developer As String
        ''' <summary>
        ''' 迭代
        ''' </summary>
        ''' <returns>iteration_id [ 迭代 ] By String</returns>
        Property iteration_id As String
        ''' <summary>
        ''' 测试重点
        ''' </summary>
        ''' <returns>test_focus [ 测试重点 ] By String</returns>
        Property test_focus As String
        ''' <summary>
        ''' 类型
        ''' </summary>
        ''' <returns>type [ 类型 ] By String</returns>
        Property type As String
        ''' <summary>
        ''' 来源
        ''' </summary>
        ''' <returns>source [ 来源 ] By String</returns>
        Property source As String
        ''' <summary>
        ''' 模块
        ''' </summary>
        ''' <returns>module [ 模块 ] By String</returns>
        Property [module] As String
        ''' <summary>
        ''' 版本
        ''' </summary>
        ''' <returns>version [ 版本 ] By String</returns>
        Property version As String
        ''' <summary>
        ''' 完成时间
        ''' </summary>
        ''' <returns>completed [ 完成时间 ] By String</returns>
        Property completed As String
        ''' <summary>
        ''' 需求分类
        ''' </summary>
        ''' <returns>category_id [ 需求分类 ] By String</returns>
        Property category_id As String
        ''' <summary>
        ''' 父需求
        ''' </summary>
        ''' <returns>parent_id [ 父需求 ] By String</returns>
        Property parent_id As String
        ''' <summary>
        ''' 子需求
        ''' </summary>
        ''' <returns>children_id [ 子需求 ] By String</returns>
        Property children_id As String
        ''' <summary>
        ''' 祖先id
        ''' </summary>
        ''' <returns>ancestor_id [ 祖先id ] By String</returns>
        Property ancestor_id As String
        ''' <summary>
        ''' 业务价值
        ''' </summary>
        ''' <returns>business_value [ 业务价值 ] By String</returns>
        Property business_value As String
        ''' <summary>
        ''' 预估工时
        ''' </summary>
        ''' <returns>effort [ 预估工时 ] By String</returns>
        Property effort As String
        ''' <summary>
        ''' 完成工时
        ''' </summary>
        ''' <returns>effort_completed [ 完成工时 ] By String</returns>
        Property effort_completed As String
        ''' <summary>
        ''' 超出工时
        ''' </summary>
        ''' <returns>exceed [ 超出工时 ] By String</returns>
        Property exceed As String
        ''' <summary>
        ''' 剩余工时
        ''' </summary>
        ''' <returns>remain [ 剩余工时 ] By String</returns>
        Property remain As String
        ''' <summary>
        ''' 发布计划
        ''' </summary>
        ''' <returns>release_id [ 发布计划 ] By String</returns>
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
        ''' <returns>custom_field_six [ 自定义字段6 ] By String</returns>
        Property custom_field_six As String
        ''' <summary>
        ''' 自定义字段7
        ''' </summary>
        ''' <returns>custom_field_seven [ 自定义字段7 ] By String</returns>
        Property custom_field_seven As String
        ''' <summary>
        ''' 自定义字段8
        ''' </summary>
        ''' <returns>custom_field_eight [ 自定义字段8 ] By String</returns>
        Property custom_field_eight As String
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
        ''' 自定义字段51
        ''' </summary>
        ''' <returns>custom_field_51 [ 自定义字段51 ] By String</returns>
        Property custom_field_51 As String
        ''' <summary>
        ''' 自定义字段52
        ''' </summary>
        ''' <returns>custom_field_52 [ 自定义字段52 ] By String</returns>
        Property custom_field_52 As String
        ''' <summary>
        ''' 自定义字段53
        ''' </summary>
        ''' <returns>custom_field_53 [ 自定义字段53 ] By String</returns>
        Property custom_field_53 As String
        ''' <summary>
        ''' 自定义字段54
        ''' </summary>
        ''' <returns>custom_field_54 [ 自定义字段54 ] By String</returns>
        Property custom_field_54 As String
        ''' <summary>
        ''' 自定义字段55
        ''' </summary>
        ''' <returns>custom_field_55 [ 自定义字段55 ] By String</returns>
        Property custom_field_55 As String
        ''' <summary>
        ''' 自定义字段56
        ''' </summary>
        ''' <returns>custom_field_56 [ 自定义字段56 ] By String</returns>
        Property custom_field_56 As String
        ''' <summary>
        ''' 自定义字段57
        ''' </summary>
        ''' <returns>custom_field_57 [ 自定义字段57 ] By String</returns>
        Property custom_field_57 As String
        ''' <summary>
        ''' 自定义字段58
        ''' </summary>
        ''' <returns>custom_field_58 [ 自定义字段58 ] By String</returns>
        Property custom_field_58 As String
        ''' <summary>
        ''' 自定义字段59
        ''' </summary>
        ''' <returns>custom_field_59 [ 自定义字段59 ] By String</returns>
        Property custom_field_59 As String
        ''' <summary>
        ''' 自定义字段60
        ''' </summary>
        ''' <returns>custom_field_60 [ 自定义字段60 ] By String</returns>
        Property custom_field_60 As String
        ''' <summary>
        ''' 自定义字段61
        ''' </summary>
        ''' <returns>custom_field_61 [ 自定义字段61 ] By String</returns>
        Property custom_field_61 As String
        ''' <summary>
        ''' 自定义字段62
        ''' </summary>
        ''' <returns>custom_field_62 [ 自定义字段62 ] By String</returns>
        Property custom_field_62 As String
        ''' <summary>
        ''' 自定义字段63
        ''' </summary>
        ''' <returns>custom_field_63 [ 自定义字段63 ] By String</returns>
        Property custom_field_63 As String
        ''' <summary>
        ''' 自定义字段64
        ''' </summary>
        ''' <returns>custom_field_64 [ 自定义字段64 ] By String</returns>
        Property custom_field_64 As String
        ''' <summary>
        ''' 自定义字段65
        ''' </summary>
        ''' <returns>custom_field_65 [ 自定义字段65 ] By String</returns>
        Property custom_field_65 As String
        ''' <summary>
        ''' 自定义字段66
        ''' </summary>
        ''' <returns>custom_field_66 [ 自定义字段66 ] By String</returns>
        Property custom_field_66 As String
        ''' <summary>
        ''' 自定义字段67
        ''' </summary>
        ''' <returns>custom_field_67 [ 自定义字段67 ] By String</returns>
        Property custom_field_67 As String
        ''' <summary>
        ''' 自定义字段68
        ''' </summary>
        ''' <returns>custom_field_68 [ 自定义字段68 ] By String</returns>
        Property custom_field_68 As String
        ''' <summary>
        ''' 自定义字段69
        ''' </summary>
        ''' <returns>custom_field_69 [ 自定义字段69 ] By String</returns>
        Property custom_field_69 As String
        ''' <summary>
        ''' 自定义字段70
        ''' </summary>
        ''' <returns>custom_field_70 [ 自定义字段70 ] By String</returns>
        Property custom_field_70 As String
        ''' <summary>
        ''' 自定义字段71
        ''' </summary>
        ''' <returns>custom_field_71 [ 自定义字段71 ] By String</returns>
        Property custom_field_71 As String
        ''' <summary>
        ''' 自定义字段72
        ''' </summary>
        ''' <returns>custom_field_72 [ 自定义字段72 ] By String</returns>
        Property custom_field_72 As String
        ''' <summary>
        ''' 自定义字段73
        ''' </summary>
        ''' <returns>custom_field_73 [ 自定义字段73 ] By String</returns>
        Property custom_field_73 As String
        ''' <summary>
        ''' 自定义字段74
        ''' </summary>
        ''' <returns>custom_field_74 [ 自定义字段74 ] By String</returns>
        Property custom_field_74 As String
        ''' <summary>
        ''' 自定义字段75
        ''' </summary>
        ''' <returns>custom_field_75 [ 自定义字段75 ] By String</returns>
        Property custom_field_75 As String
        ''' <summary>
        ''' 自定义字段76
        ''' </summary>
        ''' <returns>custom_field_76 [ 自定义字段76 ] By String</returns>
        Property custom_field_76 As String
        ''' <summary>
        ''' 自定义字段77
        ''' </summary>
        ''' <returns>custom_field_77 [ 自定义字段77 ] By String</returns>
        Property custom_field_77 As String
        ''' <summary>
        ''' 自定义字段78
        ''' </summary>
        ''' <returns>custom_field_78 [ 自定义字段78 ] By String</returns>
        Property custom_field_78 As String
        ''' <summary>
        ''' 自定义字段79
        ''' </summary>
        ''' <returns>custom_field_79 [ 自定义字段79 ] By String</returns>
        Property custom_field_79 As String
        ''' <summary>
        ''' 自定义字段80
        ''' </summary>
        ''' <returns>custom_field_80 [ 自定义字段80 ] By String</returns>
        Property custom_field_80 As String
        ''' <summary>
        ''' 自定义字段81
        ''' </summary>
        ''' <returns>custom_field_81 [ 自定义字段81 ] By String</returns>
        Property custom_field_81 As String
        ''' <summary>
        ''' 自定义字段82
        ''' </summary>
        ''' <returns>custom_field_82 [ 自定义字段82 ] By String</returns>
        Property custom_field_82 As String
        ''' <summary>
        ''' 自定义字段83
        ''' </summary>
        ''' <returns>custom_field_83 [ 自定义字段83 ] By String</returns>
        Property custom_field_83 As String
        ''' <summary>
        ''' 自定义字段84
        ''' </summary>
        ''' <returns>custom_field_84 [ 自定义字段84 ] By String</returns>
        Property custom_field_84 As String
        ''' <summary>
        ''' 自定义字段85
        ''' </summary>
        ''' <returns>custom_field_85 [ 自定义字段85 ] By String</returns>
        Property custom_field_85 As String
        ''' <summary>
        ''' 自定义字段86
        ''' </summary>
        ''' <returns>custom_field_86 [ 自定义字段86 ] By String</returns>
        Property custom_field_86 As String
        ''' <summary>
        ''' 自定义字段87
        ''' </summary>
        ''' <returns>custom_field_87 [ 自定义字段87 ] By String</returns>
        Property custom_field_87 As String
        ''' <summary>
        ''' 自定义字段88
        ''' </summary>
        ''' <returns>custom_field_88 [ 自定义字段88 ] By String</returns>
        Property custom_field_88 As String
        ''' <summary>
        ''' 自定义字段89
        ''' </summary>
        ''' <returns>custom_field_89 [ 自定义字段89 ] By String</returns>
        Property custom_field_89 As String
        ''' <summary>
        ''' 自定义字段90
        ''' </summary>
        ''' <returns>custom_field_90 [ 自定义字段90 ] By String</returns>
        Property custom_field_90 As String
        ''' <summary>
        ''' 自定义字段91
        ''' </summary>
        ''' <returns>custom_field_91 [ 自定义字段91 ] By String</returns>
        Property custom_field_91 As String
        ''' <summary>
        ''' 自定义字段92
        ''' </summary>
        ''' <returns>custom_field_92 [ 自定义字段92 ] By String</returns>
        Property custom_field_92 As String
        ''' <summary>
        ''' 自定义字段93
        ''' </summary>
        ''' <returns>custom_field_93 [ 自定义字段93 ] By String</returns>
        Property custom_field_93 As String
        ''' <summary>
        ''' 自定义字段94
        ''' </summary>
        ''' <returns>custom_field_94 [ 自定义字段94 ] By String</returns>
        Property custom_field_94 As String
        ''' <summary>
        ''' 自定义字段95
        ''' </summary>
        ''' <returns>custom_field_95 [ 自定义字段95 ] By String</returns>
        Property custom_field_95 As String
        ''' <summary>
        ''' 自定义字段96
        ''' </summary>
        ''' <returns>custom_field_96 [ 自定义字段96 ] By String</returns>
        Property custom_field_96 As String
        ''' <summary>
        ''' 自定义字段97
        ''' </summary>
        ''' <returns>custom_field_97 [ 自定义字段97 ] By String</returns>
        Property custom_field_97 As String
        ''' <summary>
        ''' 自定义字段98
        ''' </summary>
        ''' <returns>custom_field_98 [ 自定义字段98 ] By String</returns>
        Property custom_field_98 As String
        ''' <summary>
        ''' 自定义字段99
        ''' </summary>
        ''' <returns>custom_field_99 [ 自定义字段99 ] By String</returns>
        Property custom_field_99 As String
        ''' <summary>
        ''' 自定义字段100
        ''' </summary>
        ''' <returns>custom_field_100 [ 自定义字段100 ] By String</returns>
        Property custom_field_100 As String
        ''' <summary>
        ''' 需求地址
        ''' </summary>
        ''' <returns>collect_date [ 需求地址 ] By String</returns>
        Property url As String
        ''' <summary>
        ''' 采集日期
        ''' </summary>
        ''' <returns>collect_date [ 采集日期 ] By String</returns>
        Property collect_date As String
    End Class
End NameSpace