using UnityEngine;
using System.Collections;

public class DeviceShake
{
    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static private int _IOSShake();
    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static private void SDKInit();
    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static private void SDKLogin();
    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static private void SDKCharge(string userid, string roleid, string price, string itemid, string billtitle, string zoneid, string payext);
    //SDKShowFloatView
    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static private void SDKShowFloatView();
    [System.Runtime.InteropServices.DllImport("__Internal")]
    extern static private void SDKUserCenter();


    public static void active()
    {
#if UNITY_IPHONE
	_IOSShake();
#else

#endif
    }
    public static void Init()
    {
#if UNITY_IPHONE
        SDKInit();
#endif
    }

    public static void Login()
    {
#if UNITY_IPHONE
        SDKLogin();
#endif
    }

    //void SDKCharge(const char* useridu, const char* roleidu,const char* priceu, const char* itemidu, const char* billtitleu, const char* zoneidu, const char* payextu){
    /// <summary>
    /// Gets the charge.
    /// </summary>
    /// <param name="userid">Userid.</param>
    /// <param name="roleid">Roleid.</param>
    /// <param name="price">Price.</param>
    /// <param name="itemid">游戏内购买项ID，由运营人员为游戏创建商品列表</param>
    /// <param name="billtitle">购买项描述信息</param>
    /// <param name="zoneid">购买项目金额</param>
    /// <param name="payext">P扩展参数，各个渠道充值完成后会将改参数原样回调给BH服务器充值接口</param>
    public static void GetCharge(string userid, string roleid, string price, string itemid, string billtitle, string zoneid, string payext)
    {
#if UNITY_IPHONE
		//Waiting.getInstance ().show ("",0.5f,false);
		SDKCharge(userid,roleid,price,itemid,billtitle,zoneid,payext);
#endif
    }

    //show fuceng
    public static void ShowDiaView()
    {
        //        if (LoginData.getInstance()._chooseDiffPlat == LoginData.CHOOSEDIFFPLATFORM.TONGBUTUI)
        //        {
        //#if UNITY_IPHONE
        //        SDKShowFloatView();   //目前除了同步推，其他渠道都没有悬浮窗所以都隐藏悬浮窗
        //#endif
        //        }

    }

    //设置用户中心
    public static void ShowUserSetting()
    {
        //  if (LoginData.getInstance()._chooseDiffPlat != LoginData.CHOOSEDIFFPLATFORM.KUAIYONG)
        //  {
#if UNITY_IPHONE
		SDKUserCenter();  //showusersetting
#endif
        //     }
        //     else {
        //        MsgBox1.getInstance().show("此平台下没有此功能！");
        //    }
    }

}

