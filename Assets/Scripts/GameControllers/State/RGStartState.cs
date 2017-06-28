using UnityEngine;
using System.Collections;
using Stars;
public class RGStartState : GameState
{
	public override string update(float d)
	{
		return "";
	}
	public override void start()
    {

	}
	public override  void over()
	{

	}
	
	public override string postEvent(FSMEvent_C evt)
	{
		string msg = evt.msg;
		switch(evt.msg){

		}
		return "";
	}

    void initCrashReport()
    {
        TyCrashReport.initInfo info = new TyCrashReport.initInfo();
        info._crashReportPlatform = TyCrashReport.CrashReportPlatform.Bugly;
        info._debuglogprint = true;
        info._ver = ApplicationVer.StringVer;
        info._appId_Android = TyBuglyAgent._appId_Android;
        info._appId_IOS = TyBuglyAgent._appId_IOS;
        info._user = "ty";
        TyCrashReport.init(info);
    }
}

