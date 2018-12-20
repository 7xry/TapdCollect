Imports Newtonsoft.Json
Imports TapdCollect.Tapd.Bug.Impl
Imports TapdCollect.Tapd.Global.Config
Imports TapdCollect.Tapd.Global.Impl
Imports TapdCollect.Tapd.Global.Model
Imports TapdCollect.Tapd.LaunchForm.Impl
Imports TapdCollect.Tapd.Project.Impl
Imports TapdCollect.Tapd.Project.Model
Imports TapdCollect.Tapd.Relation.Impl
Imports TapdCollect.Tapd.Release.Impl
Imports TapdCollect.Tapd.Story.Impl
Imports TapdCollect.Tapd.Task.Impl
Imports TapdCollect.Tapd.TCase.Impl
Imports TapdCollect.Tapd.Workflow.Impl
Imports TapdCollect.Utils.FileSystem.Dict
Imports TapdCollect.Utils.FileSystem.Impl.Log
Imports TapdCollect.Utils.FileSystem.Impl.Path

Module Program
    Sub Main(args As String())
        Dim sw As New Stopwatch
        sw.Start()
        Dim result = True
        IM_Config.LoadConfiguration()
        If args.Any() Then
            Dim OperateList As New List(Of String)
            For CurIdx = 0 To args.Count() - 1
                Dim Operate As String = args(CurIdx)
                If CurIdx = 0 And Operate = 0 Then
                    For opIdx = 1 To 12
                        result = SelectFunction(opIdx.ToString(), False)
                        If result = False Then
                            Exit For
                        End If
                    Next
                    Exit For
                End If
                If Operate = 0 Then
                    IM_Log.Showlog($"全量同步必须为第一个参数，同步跳过", MsgType.InfoMsg)
                    Continue For
                End If
                If OperateList.Contains(Operate) = True Then
                    IM_Log.Showlog($"相同的同步任务已经被执行，同步跳过", MsgType.InfoMsg)
                    Continue For
                End If
                OperateList.Add(Operate)
                result = SelectFunction(Operate, False)
                If result = False Then
                    Exit For
                End If
            Next
            sw.Stop()
            If result = True Then
                IM_Log.Showlog($"采集任务执行成功！共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"采集任务执行没有全部执行成功！请确认！", MsgType.InfoMsg)
                'IM_Log.WaitForDo()
            End If
            Return
        End If
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($"=========================     欢迎使用Tapd同步工具，请根据提示完成操作     =========================", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 01 ] , 开始 [ 同步项目 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 02 ] , 开始 [ 同步工作流状态中英文名对应关系 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 03 ] , 开始 [ 同步自定义字段配置 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 04 ] , 开始 [ 同步需求分类 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 05 ] , 开始 [ 同步测试用例分类 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 06 ] , 开始 [ 同步需求 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 07 ] , 开始 [ 同步缺陷]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 08 ] , 开始 [ 同步任务 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 09 ] , 开始 [ 同步测试计划 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 10 ] , 开始 [ 同步测试用例 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 11 ] , 开始 [ 同步需求变更历史 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 12 ] , 开始 [ 同步缺陷变更历史 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 13 ] , 开始 [ 同步发布评审 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 14 ] , 开始 [ 同步发布计划 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 15 ] , 开始 [ 同步关联关系 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 97 ] , 开始 [ 删除配置文件 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 98 ] , 开始 [ 重置配置文件 ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 输入 [ 99 ] , 开始 [ 初始化数据库 ]", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> 提示： 多项参数请以 空格 隔开", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($"请输入：", MsgType.InfoMsg)
        Dim ReadLine As String = Console.ReadLine().Trim().ToUpper()
        Dim OperateArgs() As String = ReadLine.Split(" ")
        Dim OperateLs As New List(Of String)
        Dim IsManual As Boolean = True
        If OperateArgs.Count() >1 Then
            If IM_Log.ShowConfirm("逐项进行确认","是否要")=False Then
                IsManual=False
            End If
        End If
        For CurIdx = 0 To OperateArgs.Count() - 1
            Dim Operate As String = OperateArgs(CurIdx)
            If CurIdx = 0 And Operate = 0 Then
                For opIdx = 1 To 15
                    result = SelectFunction(opIdx.ToString(), IsManual)
                    If result = False Then
                        Exit For
                    End If
                Next
                Exit For
            End If
            If Operate = 0 Then
                IM_Log.Showlog($"全量同步必须为第一个参数，同步跳过", MsgType.InfoMsg)
                Continue For
            End If
            If OperateLs.Contains(Operate) = True Then
                IM_Log.Showlog($"相同的同步任务已经被执行，同步跳过", MsgType.InfoMsg)
                Continue For
            End If
            OperateLs.Add(Operate)
            result = SelectFunction(Operate, IsManual)
            If result = False Then
                Exit For
            End If
        Next
        sw.Stop()
        If result = True Then
            IM_Log.Showlog($"采集任务执行成功！共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
        Else
            IM_Log.Showlog($"采集任务执行没有全部执行成功！请确认！", MsgType.InfoMsg)
            IM_Log.WaitForDo()
        End If
    End Sub

    Private Function SelectFunction(FunType As String, IsManual As Boolean) As Boolean
        Dim result = True
        Select Case FunType.ToUpper()
            Case 1, "01", "项目"
                result = SyscProjects(IsManual)
            Case 2, "02", "工作流状态中英文名对应关系"
                result = SyscStatusMap(IsManual)
            Case 3, "03", "自定义字段配置"
                result = SyscCustomFieldsSettings(IsManual)
            Case 4, "04", "需求分类"
                result = SyscStoryCategories(IsManual)
            Case 5, "05", "测试用例分类"
                result = SyscTcaseCategories(IsManual)
            Case 6, "06", "需求"
                result = SyscStories(IsManual)
            Case 7, "07", "缺陷"
                result = SyscBugs(IsManual)
            Case 8, "08", "任务"
                result = SyscTasks(IsManual)
            Case 9, "09", "测试计划"
                result = SyscTestPlans(IsManual)
            Case 10, "10", "测试用例"
                result = SyscTCases(IsManual)
            Case 11, "11", "需求变更历史"
                result = SyscStoryChanges(IsManual)
            Case 12, "12", "缺陷变更历史"
                result = SyscBugChanges(IsManual)
            Case 13, "13", "发布评审"
                result = SyscLaunchForms(IsManual)
            Case 14, "14", "发布计划"
                result = SyscReleases(IsManual)
            Case 15, "15", "关联关系"
                result = SyscRelations(IsManual)
            Case 97, "97", "删除配置文件"
                IM_Config.DeleteConfiguration()
            Case 98, "98", "重置配置文件"
                IM_Config.CreateConfiguration()
            Case 99, "99", "初始化数据库"
                IM_Log.Showlog($"{IM_AppPath.NewLine()}{IM_AppPath.NewLine()}",MsgType.InfoMsg)
                If IM_Log.ShowConfirm("初始化数据库（已存在的记录将被清除！）","是否要") = True Then
                    IM_Log.Showlog($"{IM_AppPath.NewLine()}{IM_AppPath.NewLine()}",MsgType.InfoMsg)
                    If IM_Log.ShowConfirm("初始化数据库（已存在的记录将被清除！）","再次确认是否要") = True Then
                        IM_InitializeDataBase.Init()
                    End If
                End If
            Case Else
                IM_Log.Showlog($"您的输入有误，请重新执行采集任务！", MsgType.InfoMsg)
                Return False
        End Select
        Return result
    End Function

    ''' <summary>
    '''     操作确认
    ''' </summary>
    ''' <param name="operate">操作类型</param>
    ''' <param name="ShowConfirm">是否需要确认</param>
    ''' <returns>操作确认</returns>
    Private Function CheckConfirm(operate As String, ByVal Optional ShowConfirm As Boolean = True) As Boolean
        If ShowConfirm = False Then
            IM_Log.Showlog($"开始自动执行 [ {operate} ] ！", MsgType.InfoMsg)
            Return True
        End If
        Dim result As Boolean = IM_Log.ShowConfirm($"{operate}", "是否执行 ")
        If result = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    '''     01、同步项目列表
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscProjects(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "项目列表"
        If CheckConfirm("SyscProjects", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        IM_Log.Showlog($"同步{operate}开始！", MsgType.InfoMsg)
        Dim ReqParm As New MD_Request() With{
                .BaseUrl=BaseUrl,
                .RequestUrl=WorkspacesProjects,
                .ParmStr=$"company_id={CompanyId}" 
                }
        Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
        If tapd Is Nothing Then
            IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
            Return False
        End If
        If IM_Tapd_Project.Delete = False Then
            IM_Log.Showlog($"删除{operate}失败！", MsgType.InfoMsg)
            IM_Log.WaitForDo()
            Return False
        End If
        Dim ResultFlag = IM_Tapd_Project.Sync(tapd)
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步{operate}成功！共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     02、同步工作流状态中英文名对应关系
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscStatusMap(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "工作流状态中英文名对应关系"
        If CheckConfirm("SyscStatusMap", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        Dim AllowSystem() As String = {"story", "bug"}
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            If IM_Tapd_StatusMap.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 0 To AllowSystem.Count() - 1
                Dim System As String = AllowSystem(CurIdx)
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=StatusMap,
                        .ParmStr=$"workspace_id={workspace_id}&system={System}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                Dim dic = JsonConvert.DeserializeObject (Of Dictionary(Of String,String))(tapd.data.ToString())
                ResultFlag = IM_Tapd_StatusMap.Sync(dic, workspace_id, System)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     03、同步自定义字段配置
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscCustomFieldsSettings(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "自定义字段配置"
        If CheckConfirm("SyscCustomFieldsSettings", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        Dim CustomFieldsSettingList() As String = {StoriesCustomFieldsSettings, BugsCustomFieldsSettings, TasksCustomFieldsSettings, IterationsCustomFieldsSettings}
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            If IM_Tapd_Custom_Fields_Settings.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 0 To CustomFieldsSettingList.Count() - 1
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=CustomFieldsSettingList(CurIdx),
                        .ParmStr=$"workspace_id={workspace_id}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Custom_Fields_Settings.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     04、同步需求分类
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscStoryCategories(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "需求分类"
        If CheckConfirm("SyscStoryCategories", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Story_Categories.GetCount(workspace_id)
            If MaxCount is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Story_Categories.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=StoryCategories,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Story_Categories.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     05、同步测试用例分类
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscTcaseCategories(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "测试用例分类"
        If CheckConfirm("SyscTcaseCategories", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_TCase_Categories.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_TCase_Categories.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=TCasesCategories,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_TCase_Categories.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     06、同步需求
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscStories(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "需求"
        If CheckConfirm("SyscStories", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Stories.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Stories.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=Stories,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Stories.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目，共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     07、同步缺陷
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscBugs(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "缺陷"
        If CheckConfirm("SyscBugs", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Bugs.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Bugs.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=Bugs,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Bugs.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function


    ''' <summary>
    '''     08、同步任务
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscTasks(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "任务"
        If CheckConfirm("SyscTasks", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Tasks.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Tasks.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=Tasks,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Tasks.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     09、同步测试计划
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscTestPlans(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "测试计划"
        If CheckConfirm("SyscTestPlans", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Test_Plans.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Test_Plans.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=TestPlans,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Test_Plans.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目，共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     10、同步测试用例
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscTCases(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "测试用例"
        If CheckConfirm("SyscTCases", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_TCases.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_TCases.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=TCases,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_TCases.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目，共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     11、同步需求变更历史
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscStoryChanges(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "需求变更历史"
        If CheckConfirm("SyscStoryChanges", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Story_Changes.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Story_Changes.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=StoriesChanges,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Story_Changes.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     12、同步缺陷变更历史
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscBugChanges(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "缺陷变更历史"
        If CheckConfirm("SyscBugChanges", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Bug_Changes.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Bug_Changes.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=BugChanges,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Bug_Changes.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     13、同步发布评审
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscLaunchForms(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "发布评审"
        If CheckConfirm("SyscLaunchForms", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Launch_Forms.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Launch_Forms.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=LaunchForms,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Launch_Forms.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目，共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     14、同步发布计划
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscReleases(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "发布计划"
        If CheckConfirm("SyscReleases", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Releases.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}数量为{MaxCount}，跳过同步！", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Releases.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            For CurIdx = 1 To MaxPage
                Dim ReqParm As New MD_Request() With{
                        .BaseUrl=BaseUrl,
                        .RequestUrl=Releases,
                        .ParmStr=$"workspace_id={workspace_id}&limit={PageLimit}&page={CurIdx}"
                        }
                Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
                If tapd Is Nothing Then
                    IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Releases.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目，共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     15、同步关联关系
    ''' </summary>
    ''' <param name="ShowConfirm">是否需要确认</param>
    Private Function SyscRelations(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "关联关系"
        If CheckConfirm("SyscRelations", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("项目列表为空，采集任务执行失败！", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}开始！", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            If IM_Tapd_Relations.Delete(workspace_id) = False Then
                IM_Log.Showlog($"删除{workspace_id}{operate}失败！", MsgType.ErrorMsg)
                Return False
            End If
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=BaseUrl,
                    .RequestUrl=Relations,
                    .ParmStr=$"workspace_id={workspace_id}"
                    }
            Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
            If tapd Is Nothing Then
                IM_Log.Showlog($"同步{operate}失败！接口请求返回异常", MsgType.ErrorMsg)
                Exit For
            End If
            ResultFlag = IM_Tapd_Relations.Sync(tapd)
            If ResultFlag = False Then
                Exit For
            End If
            If ResultFlag = True Then
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}成功！", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"同步 {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} 的{operate}失败！", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"同步的{operate}成功！共计同步 {ProjectList.Count} 个项目,共计耗时 {Math.Round(sw.Elapsed.TotalSeconds, 0)} 秒", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"同步的{operate}失败！", MsgType.ErrorMsg)
            Return False
        End If
    End Function
End Module
