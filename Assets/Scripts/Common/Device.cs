using UnityEngine;
using System.Collections;

public enum Renderquality
{
    Low,
    Normal,
    High,
    VeryHigh
}

public class Device : MonoBehaviour
{
    public static int CPUfreq_ = 0;
    public static int ProcessMem = 0;
    /// <summary>
    /// 判断此种机型运行游戏内存是否吃紧
    /// </summary>
    public static bool _isMemoryNotMuch = false;

    public static void collectInfo()
    {
#if UNITY_IPHONE || UNITY_EDITOR
        collectRenderInfo();
        if(SystemConfig.renderquality == Renderquality.Low ){
            _isMemoryNotMuch = true;
        }
#elif UNITY_ANDROID && !UNITY_EDITOR
        RG_Utils.callAndroidJava("getMaxCpuFreq");
        string graphicsDeviceName = SystemInfo.graphicsDeviceName;
        if(SystemInfo.graphicsMemorySize + SystemInfo.systemMemorySize <= 800 ||  
            (SystemInfo.graphicsMemorySize + SystemInfo.systemMemorySize < 1200 &&  
            !(graphicsDeviceName.Contains("Andreno") || 
              graphicsDeviceName.Contains("Mali") ||
              graphicsDeviceName.Contains("PowerVR") ||
              graphicsDeviceName.Contains("Tegra") ))){
                _isMemoryNotMuch = true;
                TyLogger.Log("MemoryNotMuch");
        }
        TyLogger.Log("graphicsDeviceName " + SystemInfo.graphicsDeviceName);
#endif
    }
    static void collectRenderInfo()
    {
        setTextureQuality();
        if (SystemConfig.isUserSetRenderQuality())
        {
            SystemConfig.setAnisotropicFilter();
            return;
        }
#if UNITY_IPHONE	
		iPhoneGeneration iosGen = iPhone.generation;
        if(iosGen == iPhoneGeneration.Unknown){
            SystemConfig.renderquality = Renderquality.VeryHigh;
        }

        if (iosGen == iPhoneGeneration.iPad3Gen || iosGen == iPhoneGeneration.iPad4Gen || iosGen == iPhoneGeneration.iPad5Gen)
        {
            SystemConfig.renderquality = Renderquality.High;
        }

		if(iosGen <= iPhoneGeneration.iPad2Gen)
        {
        //            //
        //// ժҪ: 
        ////     First generation device.
        //iPhone = 1,
        ////
        //// ժҪ: 
        ////     Second generation.
        //iPhone3G = 2,
        ////
        //// ժҪ: 
        ////     Third generation.
        //iPhone3GS = 3,
        ////
        //// ժҪ: 
        ////     iPod Touch, first generation.
        //iPodTouch1Gen = 4,
        ////
        //// ժҪ: 
        ////     iPod Touch, second generation.
        //iPodTouch2Gen = 5,
        ////
        //// ժҪ: 
        ////     iPod Touch, third generation.
        //iPodTouch3Gen = 6,
        ////
        //// ժҪ: 
        ////     iPad, first generation.
        //iPad1Gen = 7,
        ////
        //// ժҪ: 
        ////     Fourth generation.
        //iPhone4 = 8,
        ////
        //// ժҪ: 
        ////     iPod Touch, fourth generation.
        //iPodTouch4Gen = 9,
        ////
        //// ժҪ: 
        ////     iPad, second generation.
        //iPad2Gen = 10,
			SystemConfig.renderquality = Renderquality.Low;	
		}
        else if(iosGen <= iPhoneGeneration.iPad3Gen || iosGen == iPhoneGeneration.iPadMini1Gen
            || iosGen == iPhoneGeneration.iPad4Gen)
        {
        //            //
        //// ժҪ: 
        ////     Fifth generation.
        //iPhone4S = 11,
        ////
        //// ժҪ: 
        ////     iPad, third generation.
        //iPad3Gen = 12,
                    //
        // ժҪ: 
        ////     iPadMini, first generation.
        //iPadMini1Gen = 15,
        ////
        //// ժҪ: 
        ////     iPad, fourth generation.
        //iPad4Gen = 16,
			SystemConfig.renderquality = Renderquality.Normal;
        }
        else if(iosGen <= iPhoneGeneration.iPadMini2Gen)
        {
        //            //
        //// ժҪ: 
        ////     iPhone5.
        //iPhone5 = 13,
        ////
        //// ժҪ: 
        ////     iPod Touch, fifth generation.
        //iPodTouch5Gen = 14,
            if (iosGen == iPhoneGeneration.iPhone5S)
            {
                SystemConfig.renderquality = Renderquality.VeryHigh;
            }
			SystemConfig.renderquality = Renderquality.High;
        }
        //else if (iosGen == iPhoneGeneration.iPadMini2Gen)
        //{

        //    return Renderquality.VeryHigh;
        //}
        else
        {
            SystemConfig.renderquality = Renderquality.VeryHigh;
        }


#elif UNITY_ANDROID 

        SystemConfig.renderquality = getAndroidDefaultRenderQuality();
        Debug.Log("Android Renderquality = " + SystemConfig.renderquality.ToString());
#else
        //return Renderquality.Normal;
        SystemConfig.renderquality = Renderquality.Normal;
#endif
    }



    static Renderquality getAndroidDefaultRenderQuality()
    {
        if (CPUfreq_ <= 1500000 || SystemInfo.systemMemorySize + SystemInfo.graphicsMemorySize <= 512 )
        {
            return Renderquality.Low;
        }
        else if (CPUfreq_ <= 2000000)
        {
            return Renderquality.Normal;
        }
        else if (CPUfreq_ > 2000000)
        {
            return Renderquality.High;
        }
        return Renderquality.Low;
        
    }


    public static Renderquality getRenderQualityLevle()
    {

#if UNITY_EDITOR
        return Renderquality.VeryHigh;
#endif
        return SystemConfig.renderquality;

    }

    public void onGetMaxCpuFreq(string freq)
    {
        try
        {
            CPUfreq_ = int.Parse(freq);
        }
        catch
        {
            Debug.Log("cpu freq = " + freq);
        }
        //目前只收集cpu的信息
        collectRenderInfo();
    }

    public void onGetMemory(string memory)
    {
        try
        {
            ProcessMem = int.Parse(memory);
        }
        catch
        {
            Debug.Log("RSSMemory_ = " + memory);
        }
    }
    /// <summary>
    /// 设置纹理质量
    /// </summary>
    public static void setTextureQuality()
    {
        if (SystemInfo.systemMemorySize + SystemInfo.graphicsMemorySize <= 1024)
        {
            QualitySettings.masterTextureLimit = 1;
        }
        else
        {
            QualitySettings.masterTextureLimit = 0;
        }
    }
}