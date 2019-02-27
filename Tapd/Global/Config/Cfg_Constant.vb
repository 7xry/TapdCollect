Imports TapdCollect.Utils.FileSystem.Impl.Path

Namespace Tapd.Global.Config
    Module Cfg_Constant
        Public Property AuthUserName as String
        Public Property AuthPassWord as String
        Public Property CompanyId as String
        Public Property PageLimit as Integer
        Public Property RetryLimit As Integer
        Public Property IsKeepHistory As Boolean
        Public Property ConfigFilePath As String =$"{IM_AppPath.GetPath }\TapdCollect.config"


        Public Const DKey As String="TapdCollect"
        Public Const DV As String="Pw345&#%"

        Public Const Url as String="https://www.tapd.cn"
        Public Const BaseUrl as String="https://api.tapd.cn"
        '项目
        Public Const WorkspacesProjects as String="workspaces/projects"
        Public Const WorkspacesUsers as String="workspaces/users"
        '需求
        Public Const Stories as String="stories"
        Public Const StoriesCount as String="stories/count"
        Public Const StoriesCustomFieldsSettings as String="stories/custom_fields_settings"
        Public Const StoriesChanges as String="story_changes"
        Public Const StoriesChangesCount as String="story_changes/count"
        Public Const StoryCategories as String="story_categories"
        Public Const StoryCategoriesCount as String="story_categories/count"
        '任务
        Public Const Tasks as String="tasks"
        Public Const TasksCount as String="tasks/count"
        Public Const TasksCustomFieldsSettings as String="tasks/custom_fields_settings"
        '测试用例
        Public Const TCases as String="tcases"
        Public Const TCasesCount as String="tcases/count"
        Public Const TCasesCategories as String="tcase_categories"
        Public Const TCasesCategoriesCount as String="tcase_categories/count"
        '测试计划
        Public Const TestPlans as String="test_plans"
        Public Const TestPlansCount as String="test_plans/count"
        '缺陷
        Public Const Bugs as String="bugs"
        Public Const BugsCount as String="bugs/count"
        Public Const BugsGroupCount as String="bugs/group_count"
        Public Const BugsCustomFieldsSettings as String="bugs/custom_fields_settings"
        Public Const BugChanges as String="bug_changes"
        Public Const BugChangesCount as String="bug_changes/count"
        '迭代
        Public Const Iterations as String="iterations"
        Public Const IterationsCount as String="iterations/count"
        Public Const IterationsCustomFieldsSettings as String="iterations/custom_fields_settings"
        '关联关系
        Public Const Relations as String="relations"
        '评论
        Public Const Comments as String="comments"
        Public Const CommentsCount as String="comments/count"
        '中英文对照关系
        Public Const StatusMap as String="workflows/status_map"
        '评审
        Public Const LaunchForms as String="launch_forms"
        Public Const LaunchFormsCount as String="launch_forms/count"
        '发布计划
        Public Const Releases as String="releases"
        Public Const ReleasesCount as String="releases/count"
        '模块
        Public Const Modules as String="modules"
        Public Const ModulesCount as String="modules/count"
        '花费
        Public Const TimeSheets as String="timesheets"
        Public Const TimeSheetsCount as String="timesheets/count"
    End Module
End NameSpace