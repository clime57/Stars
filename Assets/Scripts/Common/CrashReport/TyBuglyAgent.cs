using UnityEngine;
using System.Collections;
/// <summary>
/// Bugly平台代理
/// </summary>
public class TyBuglyAgent : TyCrashReportAgent
{
    public const string _appId_Android = "900009700";
    public const string _appId_IOS = "900009253";
    public void init(TyCrashReport.initInfo info)
    {
        // enable debug log print
        BuglyAgent.ConfigDebugMode(info._debuglogprint);
        // Register log callback with 'BuglyAgent.LogCallbackDelegate' to replace the 'Application.RegisterLogCallback(Application.LogCallback)'
        //BuglyAgent.RegisterLogCallback(CallbackDelegate.Instance.OnApplicationLogCallbackHandler);
        BuglyAgent.RegisterLogCallback(null);
        BuglyAgent.ConfigDefault(info._channel, info._ver, info._user, 0);

#if UNITY_IPHONE || UNITY_IOS
		BuglyAgent.InitWithAppId (info._appId_IOS);
#elif UNITY_ANDROID
        BuglyAgent.InitWithAppId(info._appId_Android);
#endif

        // If you do not need call 'InitWithAppId(string)' to initialize the sdk(may be you has initialized the sdk it associated Android or iOS project),
        // please call this method to enable c# exception handler only.
        BuglyAgent.EnableExceptionHandler();
    }

    public void reportException(System.Exception e, string desc)
    {
        BuglyAgent.ReportException(e,desc);
    }

    public void setUserId(string userId)
    {
        BuglyAgent.SetUserId(userId);
    }

}
