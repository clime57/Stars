using UnityEngine;
using UnityEditor;

public class DebugMenu 
{
    [MenuItem("Debug/UnloadUnusedMemory")]
    static void UnloadUnusedMemory()
    {
        //UIWindowManager.getInstance().clearOther();
        EditorUtility.UnloadUnusedAssets();
        System.GC.Collect();
    }

    [MenuItem("Debug/UnloadUnusedAssetsIgnoreManagedReferences")]
    static void UnloadUnusedAssetsIgnoreManagedReferences()
    {
        EditorUtility.UnloadUnusedAssetsIgnoreManagedReferences();
        System.GC.Collect();
    }


    [MenuItem("Debug/Time/SetTimeScale:1")]
    static void SetTimeScale1()
    {
        Time.timeScale = 1;
    }

    [MenuItem("Debug/Time/SetTimeScale:0.5")]
    static void SetTimeScale05()
    {
        Time.timeScale = 0.5f;
    }

    [MenuItem("Debug/Time/SetTimeScale:0.2")]
    static void SetTimeScale02()
    {
        Time.timeScale = 0.2f;
    }

    [MenuItem("Debug/Time/SetTimeScale:0.1")]
    static void SetTimeScale01()
    {
        Time.timeScale = 0.1f;
    }    

    [MenuItem("Debug/Profiler/BeginLog")]
    static void BeginLog()
    {
        UnityEngine.Profiling.Profiler.logFile = "mylog.log";
        UnityEngine.Profiling.Profiler.enabled = true;
    }

    [MenuItem("Debug/Profiler/EndLog")]
    static void EndLog()
    {
        UnityEngine.Profiling.Profiler.enabled = false;
    } 

    [MenuItem("Debug/返回登录")]
    static void BackToLogin()
    {
        RGGameLogic.backToLogin();
    }

    //[MenuItem("Debug/删除登录窗口")]
    //static void destroyWindowLoginPanel()
    //{
    //    UIWindowManager.getInstance().destroyWindow("LoginPanel");

    //}
    [MenuItem("Debug/设置图像质量/最高精度")]
    static void QualitySettingsmasterTextureLimit0()
    {
        QualitySettings.masterTextureLimit = 0;
    }
    [MenuItem("Debug/设置图像质量/一半精度")]
    static void QualitySettingsmasterTextureLimit1()
    {
        QualitySettings.masterTextureLimit = 1;
    } 
}
