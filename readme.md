# 说明
该资源搜集自网络，下附作者说明和相关信息

GGTalk 网站： http://www.cnblogs.com/justnow/p/3382160.html （安卓端源码可从网站下载）


1.当前版本服务端默认配置为内存虚拟数据库版本，不需要安装数据库。

2.将GGTalk.Server文件夹拷贝到服务器上，运行GGTalk.Server.exe。

3.修改客户端配置文件GGTalk.exe.config中ServerIP配置项的值为服务器的IP。

4.运行客户端，注册帐号登录试用。

5.内置测试帐号为 10000，10001，10002，10003，10004；密码都是 1。



---------------------------------------------------------------------------------
如果需要使用真实的物理数据库，则需按下列步骤进行：

1. 在SqlServer 2000/2005/2008 中新建数据库GGTalk，然后在该库中执行 SqlServer.sql 文件中的脚本以创建所需表。
   (如果要使用MySQL数据库，则使用MySQL.sql脚本)

2. 打开服务端的配置文件GGTalk.Server.exe.config 

（1）修改 UseVirtualDB 配置项的值为false。

（2）修改 DBType 为 SqlServer 或 MySQL。

（3）修改 DBIP 配置项的值为数据库的IP地址。

（4）修改 SaPwd 配置项的值为数据库管理员sa的密码。

3.修改客户端配置文件GGTalk.exe.config中ServerIP配置项的值为服务器的IP。

4.运行客户端，注册帐号登录试用。