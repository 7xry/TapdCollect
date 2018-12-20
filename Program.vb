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
                    IM_Log.Showlog($"ȫ��ͬ������Ϊ��һ��������ͬ������", MsgType.InfoMsg)
                    Continue For
                End If
                If OperateList.Contains(Operate) = True Then
                    IM_Log.Showlog($"��ͬ��ͬ�������Ѿ���ִ�У�ͬ������", MsgType.InfoMsg)
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
                IM_Log.Showlog($"�ɼ�����ִ�гɹ������ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"�ɼ�����ִ��û��ȫ��ִ�гɹ�����ȷ�ϣ�", MsgType.InfoMsg)
                'IM_Log.WaitForDo()
            End If
            Return
        End If
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($"=========================     ��ӭʹ��Tapdͬ�����ߣ��������ʾ��ɲ���     =========================", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 01 ] , ��ʼ [ ͬ����Ŀ ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 02 ] , ��ʼ [ ͬ��������״̬��Ӣ������Ӧ��ϵ ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 03 ] , ��ʼ [ ͬ���Զ����ֶ����� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 04 ] , ��ʼ [ ͬ��������� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 05 ] , ��ʼ [ ͬ�������������� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 06 ] , ��ʼ [ ͬ������ ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 07 ] , ��ʼ [ ͬ��ȱ��]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 08 ] , ��ʼ [ ͬ������ ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 09 ] , ��ʼ [ ͬ�����Լƻ� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 10 ] , ��ʼ [ ͬ���������� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 11 ] , ��ʼ [ ͬ����������ʷ ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 12 ] , ��ʼ [ ͬ��ȱ�ݱ����ʷ ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 13 ] , ��ʼ [ ͬ���������� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 14 ] , ��ʼ [ ͬ�������ƻ� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 15 ] , ��ʼ [ ͬ��������ϵ ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 97 ] , ��ʼ [ ɾ�������ļ� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 98 ] , ��ʼ [ ���������ļ� ]", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ���� [ 99 ] , ��ʼ [ ��ʼ�����ݿ� ]", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($">>>>>>>>>> ��ʾ�� ����������� �ո� ����", MsgType.InfoMsg)
        IM_Log.Showlog($"====================================================================================================", MsgType.InfoMsg)
        IM_Log.Showlog($"�����룺", MsgType.InfoMsg)
        Dim ReadLine As String = Console.ReadLine().Trim().ToUpper()
        Dim OperateArgs() As String = ReadLine.Split(" ")
        Dim OperateLs As New List(Of String)
        Dim IsManual As Boolean = True
        If OperateArgs.Count() >1 Then
            If IM_Log.ShowConfirm("�������ȷ��","�Ƿ�Ҫ")=False Then
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
                IM_Log.Showlog($"ȫ��ͬ������Ϊ��һ��������ͬ������", MsgType.InfoMsg)
                Continue For
            End If
            If OperateLs.Contains(Operate) = True Then
                IM_Log.Showlog($"��ͬ��ͬ�������Ѿ���ִ�У�ͬ������", MsgType.InfoMsg)
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
            IM_Log.Showlog($"�ɼ�����ִ�гɹ������ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
        Else
            IM_Log.Showlog($"�ɼ�����ִ��û��ȫ��ִ�гɹ�����ȷ�ϣ�", MsgType.InfoMsg)
            IM_Log.WaitForDo()
        End If
    End Sub

    Private Function SelectFunction(FunType As String, IsManual As Boolean) As Boolean
        Dim result = True
        Select Case FunType.ToUpper()
            Case 1, "01", "��Ŀ"
                result = SyscProjects(IsManual)
            Case 2, "02", "������״̬��Ӣ������Ӧ��ϵ"
                result = SyscStatusMap(IsManual)
            Case 3, "03", "�Զ����ֶ�����"
                result = SyscCustomFieldsSettings(IsManual)
            Case 4, "04", "�������"
                result = SyscStoryCategories(IsManual)
            Case 5, "05", "������������"
                result = SyscTcaseCategories(IsManual)
            Case 6, "06", "����"
                result = SyscStories(IsManual)
            Case 7, "07", "ȱ��"
                result = SyscBugs(IsManual)
            Case 8, "08", "����"
                result = SyscTasks(IsManual)
            Case 9, "09", "���Լƻ�"
                result = SyscTestPlans(IsManual)
            Case 10, "10", "��������"
                result = SyscTCases(IsManual)
            Case 11, "11", "��������ʷ"
                result = SyscStoryChanges(IsManual)
            Case 12, "12", "ȱ�ݱ����ʷ"
                result = SyscBugChanges(IsManual)
            Case 13, "13", "��������"
                result = SyscLaunchForms(IsManual)
            Case 14, "14", "�����ƻ�"
                result = SyscReleases(IsManual)
            Case 15, "15", "������ϵ"
                result = SyscRelations(IsManual)
            Case 97, "97", "ɾ�������ļ�"
                IM_Config.DeleteConfiguration()
            Case 98, "98", "���������ļ�"
                IM_Config.CreateConfiguration()
            Case 99, "99", "��ʼ�����ݿ�"
                IM_Log.Showlog($"{IM_AppPath.NewLine()}{IM_AppPath.NewLine()}",MsgType.InfoMsg)
                If IM_Log.ShowConfirm("��ʼ�����ݿ⣨�Ѵ��ڵļ�¼�����������","�Ƿ�Ҫ") = True Then
                    IM_Log.Showlog($"{IM_AppPath.NewLine()}{IM_AppPath.NewLine()}",MsgType.InfoMsg)
                    If IM_Log.ShowConfirm("��ʼ�����ݿ⣨�Ѵ��ڵļ�¼�����������","�ٴ�ȷ���Ƿ�Ҫ") = True Then
                        IM_InitializeDataBase.Init()
                    End If
                End If
            Case Else
                IM_Log.Showlog($"������������������ִ�вɼ�����", MsgType.InfoMsg)
                Return False
        End Select
        Return result
    End Function

    ''' <summary>
    '''     ����ȷ��
    ''' </summary>
    ''' <param name="operate">��������</param>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    ''' <returns>����ȷ��</returns>
    Private Function CheckConfirm(operate As String, ByVal Optional ShowConfirm As Boolean = True) As Boolean
        If ShowConfirm = False Then
            IM_Log.Showlog($"��ʼ�Զ�ִ�� [ {operate} ] ��", MsgType.InfoMsg)
            Return True
        End If
        Dim result As Boolean = IM_Log.ShowConfirm($"{operate}", "�Ƿ�ִ�� ")
        If result = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    '''     01��ͬ����Ŀ�б�
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscProjects(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "��Ŀ�б�"
        If CheckConfirm("SyscProjects", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        IM_Log.Showlog($"ͬ��{operate}��ʼ��", MsgType.InfoMsg)
        Dim ReqParm As New MD_Request() With{
                .BaseUrl=BaseUrl,
                .RequestUrl=WorkspacesProjects,
                .ParmStr=$"company_id={CompanyId}" 
                }
        Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
        If tapd Is Nothing Then
            IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
            Return False
        End If
        If IM_Tapd_Project.Delete = False Then
            IM_Log.Showlog($"ɾ��{operate}ʧ�ܣ�", MsgType.InfoMsg)
            IM_Log.WaitForDo()
            Return False
        End If
        Dim ResultFlag = IM_Tapd_Project.Sync(tapd)
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ��{operate}�ɹ������ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     02��ͬ��������״̬��Ӣ������Ӧ��ϵ
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscStatusMap(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "������״̬��Ӣ������Ӧ��ϵ"
        If CheckConfirm("SyscStatusMap", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        Dim AllowSystem() As String = {"story", "bug"}
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            If IM_Tapd_StatusMap.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                Dim dic = JsonConvert.DeserializeObject (Of Dictionary(Of String,String))(tapd.data.ToString())
                ResultFlag = IM_Tapd_StatusMap.Sync(dic, workspace_id, System)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     03��ͬ���Զ����ֶ�����
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscCustomFieldsSettings(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "�Զ����ֶ�����"
        If CheckConfirm("SyscCustomFieldsSettings", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        Dim CustomFieldsSettingList() As String = {StoriesCustomFieldsSettings, BugsCustomFieldsSettings, TasksCustomFieldsSettings, IterationsCustomFieldsSettings}
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            If IM_Tapd_Custom_Fields_Settings.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Custom_Fields_Settings.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     04��ͬ���������
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscStoryCategories(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "�������"
        If CheckConfirm("SyscStoryCategories", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Story_Categories.GetCount(workspace_id)
            If MaxCount is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Story_Categories.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Story_Categories.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     05��ͬ��������������
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscTcaseCategories(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "������������"
        If CheckConfirm("SyscTcaseCategories", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_TCase_Categories.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_TCase_Categories.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_TCase_Categories.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     06��ͬ������
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscStories(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "����"
        If CheckConfirm("SyscStories", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Stories.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Stories.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Stories.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ�����ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     07��ͬ��ȱ��
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscBugs(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "ȱ��"
        If CheckConfirm("SyscBugs", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Bugs.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Bugs.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Bugs.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function


    ''' <summary>
    '''     08��ͬ������
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscTasks(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "����"
        If CheckConfirm("SyscTasks", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Tasks.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Tasks.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Tasks.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     09��ͬ�����Լƻ�
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscTestPlans(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "���Լƻ�"
        If CheckConfirm("SyscTestPlans", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Test_Plans.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Test_Plans.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Test_Plans.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ�����ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     10��ͬ����������
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscTCases(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "��������"
        If CheckConfirm("SyscTCases", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_TCases.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_TCases.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_TCases.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ�����ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     11��ͬ����������ʷ
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscStoryChanges(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "��������ʷ"
        If CheckConfirm("SyscStoryChanges", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Story_Changes.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Story_Changes.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Story_Changes.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     12��ͬ��ȱ�ݱ����ʷ
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscBugChanges(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "ȱ�ݱ����ʷ"
        If CheckConfirm("SyscBugChanges", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Bug_Changes.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Bug_Changes.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Bug_Changes.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     13��ͬ����������
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscLaunchForms(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "��������"
        If CheckConfirm("SyscLaunchForms", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Launch_Forms.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Launch_Forms.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Launch_Forms.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ�����ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     14��ͬ�������ƻ�
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscReleases(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "�����ƻ�"
        If CheckConfirm("SyscReleases", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            Dim MaxCount as Nullable(Of Integer) = IM_Tapd_Releases.GetCount(workspace_id)
            If MaxCount Is Nothing Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If 
            Dim MaxPage = CInt(MaxCount\PageLimit)
            If MaxCount Mod PageLimit > 0 Then
                MaxPage += 1
            End If
            If MaxCount <= 0 Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}����Ϊ{MaxCount}������ͬ����", MsgType.InfoMsg)
                Continue For
            End If
            If IM_Tapd_Releases.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
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
                    IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                    Exit For
                End If
                ResultFlag = IM_Tapd_Releases.Sync(tapd)
                If ResultFlag = False Then
                    Exit For
                End If
            Next
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ�����ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function

    ''' <summary>
    '''     15��ͬ��������ϵ
    ''' </summary>
    ''' <param name="ShowConfirm">�Ƿ���Ҫȷ��</param>
    Private Function SyscRelations(ByVal Optional ShowConfirm As Boolean = True) As Boolean
        Dim operate = "������ϵ"
        If CheckConfirm("SyscRelations", ShowConfirm) = False Then
            Return True
        End If
        Dim sw As New Stopwatch
        sw.Start()
        Dim ProjectList As List(Of MD_Tapd_Project) = IM_Tapd_Project.GetList()
        If ProjectList Is Nothing Then
            IM_Log.Showlog("��Ŀ�б�Ϊ�գ��ɼ�����ִ��ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
        Dim ResultFlag = False
        For projectIdx = 0 To ProjectList.Count - 1
            Dim project As MD_Tapd_Project = ProjectList(projectIdx)
            IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}��ʼ��", MsgType.InfoMsg)
            Dim workspace_id as String = project.id
            If IM_Tapd_Relations.Delete(workspace_id) = False Then
                IM_Log.Showlog($"ɾ��{workspace_id}{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Return False
            End If
            Dim ReqParm As New MD_Request() With{
                    .BaseUrl=BaseUrl,
                    .RequestUrl=Relations,
                    .ParmStr=$"workspace_id={workspace_id}"
                    }
            Dim tapd As MD_Tapd = IM_Req.DoGet(ReqParm)
            If tapd Is Nothing Then
                IM_Log.Showlog($"ͬ��{operate}ʧ�ܣ��ӿ����󷵻��쳣", MsgType.ErrorMsg)
                Exit For
            End If
            ResultFlag = IM_Tapd_Relations.Sync(tapd)
            If ResultFlag = False Then
                Exit For
            End If
            If ResultFlag = True Then
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}�ɹ���", MsgType.InfoMsg)
            Else
                IM_Log.Showlog($"ͬ�� {projectIdx + 1}/{ProjectList.Count} - [ {project.id} ] {project.name} ��{operate}ʧ�ܣ�", MsgType.ErrorMsg)
                Exit For
            End If
        Next
        sw.Stop()
        If ResultFlag = True Then
            IM_Log.Showlog($"ͬ����{operate}�ɹ�������ͬ�� {ProjectList.Count} ����Ŀ,���ƺ�ʱ {Math.Round(sw.Elapsed.TotalSeconds, 0)} ��", MsgType.InfoMsg)
            Return True
        Else
            IM_Log.Showlog($"ͬ����{operate}ʧ�ܣ�", MsgType.ErrorMsg)
            Return False
        End If
    End Function
End Module
