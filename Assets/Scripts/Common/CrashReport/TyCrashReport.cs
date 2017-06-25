
using UnityEngine;
using System.Collections;
/// <summary>
/// 崩溃异常捕获
/// </summary>
public class TyCrashReport
{
    static TyCrashReportAgent agent_ = null;
    /// <summary>
    /// 平台
    /// </summary>
    public enum CrashReportPlatform
    {
        /// <summary>
        /// 腾讯的Bugly
        /// </summary>
        Bugly,
        /// <summary>
        /// Testin的崩溃日志分析平台
        /// </summary>
        TestinAPM
    }

    static CrashReportPlatform _crashReportPlatform = CrashReportPlatform.Bugly;
    /// <summary>
    /// 初始化信息
    /// </summary>
    public class initInfo
    {
        public CrashReportPlatform _crashReportPlatform;
        public bool _debuglogprint = false;
        public string _ver;
        public string _appId_IOS = "";
        public string _appId_Android = "";
        public string _channel = null;
        public string _user = null;
    }
    /// <summary>
    /// 对崩溃异常捕获进行初始化
    /// </summary>
    /// <param name="info">初始化信息</param>
    static public void init(initInfo info)
    {
        _crashReportPlatform = info._crashReportPlatform;
        switch (_crashReportPlatform)
        {
            case CrashReportPlatform.Bugly:
                initBugly(info);
                break;
        }
    }

    /// <summary>
    /// 初始化Bugly平台
    /// </summary>
    /// <param name="info">初始化信息</param>
    static void initBugly(initInfo info)
    {
        if (agent_ == null)
        {
            agent_ = new TyBuglyAgent();
            agent_.init(info);
        }
        else
        {
            TyLogger.LogError("SmCrashReport have already init");
        }
    }
    /// <summary>
    /// 设置用户ID
    /// </summary>
    /// <param name="userId">用户ID</param>
    static public void setUserId(string userId)
    {
        if (agent_ != null)
        {
            agent_.setUserId(userId);
        }
    }
    /// <summary>
    /// 上报异常
    /// </summary>
    /// <param name="e">异常</param>
    /// <param name="desc">描述</param>
    static public void reportException(System.Exception e, string desc)
    {
        if (agent_ != null)
        {
            agent_.reportException(e, desc);
        }
    }
}
