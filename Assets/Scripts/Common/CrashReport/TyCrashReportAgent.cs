#region HeadComments
/* ========================================================================
* Copyright (C) 2015 SinceMe
*
* 作    者：Clime
* 文件名称：SmCrashReportAgent
* 功    能：崩溃异常捕获代理接口
* 创建时间：2015/09/24 14:51:12
* 版    本：V1.0.0
*
* 修改日志：修改者： 时间： 修改内容：
* 
* =========================================================================
*/
#endregion
using UnityEngine;
using System.Collections;
/// <summary>
/// 崩溃异常捕获代理接口
/// </summary>
public interface TyCrashReportAgent
{
    /// <summary>
    /// 对崩溃异常捕获进行初始化
    /// </summary>
    /// <param name="info">初始化信息</param>
    void init(TyCrashReport.initInfo info);
    /// <summary>
    /// 上报异常
    /// </summary>
    /// <param name="e">异常</param>
    /// <param name="desc">描述</param>
    void reportException(System.Exception e, string desc);
    /// <summary>
    /// 设置用户ID
    /// </summary>
    /// <param name="userId">用户ID</param>
    void setUserId(string userId);
}
