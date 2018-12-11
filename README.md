# TapdCollect
第一次使用注意事项

1、安装.Net Croe Runtime
windows版本下载地址：
https://download.visualstudio.microsoft.com/download/pr/48adfc75-bce7-4621-ae7a-5f3c4cf4fc1f/9a8e07173697581a6ada4bf04c845a05/dotnet-hosting-2.2.0-win.exe

2、运行SetUp(手动运行).bat，进行参数配置
需要配置，配置过程中需要确认输入，请按回车确认

1、Api_User				由Tapd官方提供
2、Api_Password			由Tapd官方提供
3、CompanyId				由Tapd官方提供，公司管理中可以看到
4、PageLimit				每次采集多少数据，可选范围1-200
5、DataBaseConn			数据库连接地址，只支持Mysql数据库

！！配置文件文件设置完成后，会提示是否初始化数据库，如果已存在，请选择否，此处会二次确认！

3、选择要执行的任务 
功能1~14为正常的业务采集功能，请按需使用，输入对应数字后，按回车即代表调用
如需同时调用多个时，请在每个参数之间用空格分割，如输入1 2 3

 
二、自动执行采集任务
1、修改SetUp(自动运行).bat，将dll后面的数字，按照需要配置，数字对应含义，请参照手动执行。

 
2、设置定时任务（需在服务器上部署.Net Core环境）
其实路径请务必设置成bat所在目录，不填则配置文件会保存在%windows%\system32\下

 
 
3、设置触发条件
请根据需要进行设置，根据数据量不同，采集时间不一致，请先手动执行一次，确定时间再进行配置

 
 
