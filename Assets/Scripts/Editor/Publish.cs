#region HeadComments
/* ========================================================================
* Copyright (C) 2015 SinceMe
* 
* 作    者：Clime
* 文件名称：Publish
* 功    能：发布
* 创建时间：2015/11/23 11:10:47
* 版    本：V1.0.0
*
* 修改日志：修改者： 时间： 修改内容：
* 
* =========================================================================
*/
#endregion

using UnityEngine;
using UnityEditor;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;

public class Publish
{
    static public void BuildAPK(bool tip = true,bool isRecomress = false,bool isEncrypt = false,string fileName = "")
    {
        if (tip)
        {
            if (!EditorUtility.DisplayDialog("提示", "要开始打APK包啰？", "确定", "取消"))
            {
                return;
            }
        }
        delOtherPlatformAssetBundle(BuildTarget.Android);
        string result = null;
        DateTime dt = DateTime.Now.ToLocalTime();
        string apkFilename = dt.Year.ToString("d2") + dt.Month.ToString("d2") + dt.Day.ToString("d2") +
            dt.Hour.ToString("d2") + dt.Minute.ToString("d2");
        string apkPath = apkFilename + ".apk";
        if (fileName != "")
        {
            apkPath = fileName;
        }
        Debug.Log("apk filename = " + apkPath);
        try
        {
            result =
                BuildPipeline.BuildPlayer(
                    (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray(),
                    apkPath, BuildTarget.Android, BuildOptions.None);
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
            return;
        }
        if (!String.IsNullOrEmpty(result))
        {
            Debug.LogError(result);
            return;
        }
        if (isRecomress) APKZip.reCompress(apkPath);
        EditorUtility.DisplayDialog("提示", "恭喜！您的APK包" + System.IO.Directory.GetCurrentDirectory() + "/" + apkPath + "生成完毕!", "确定");
    }


    static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();

        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;
            if (e.enabled)
                names.Add(e.path);
        }
        return names.ToArray();
    }


    static public void BuildForIPhone()
    {
        BuildPipeline.BuildPlayer(GetBuildScenes(),"xcodeprj", BuildTarget.iOS, BuildOptions.None);
    }

    static public void delOtherPlatformAssetBundle(BuildTarget target){
        List<string> paths = new List<string>();
        addPaths(paths, target, BuildTarget.StandaloneWindows);
        addPaths(paths, target, BuildTarget.Android);
        addPaths(paths, target, BuildTarget.iOS);
        addPaths(paths, target, BuildTarget.StandaloneOSXUniversal);
        delPlatformAssetBundle(paths.ToArray());
    }

    static void addPaths(List<string> paths, BuildTarget target, BuildTarget compareTarge)
    {
        if (target != compareTarge)
        {
            string platformstr = "";
            paths.Add("Assets/StreamingAssets/" + platformstr.Substring(0,platformstr.Length -1));
        }
    }
    
    static public void delPlatformAssetBundle(string[] paths)
    {
        string[] guids = AssetDatabase.FindAssets("t:Object", paths);
        for (int i = 0; i < guids.Length; i++)
        {
            try
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(guids[i]));
                Debug.Log(AssetDatabase.GUIDToAssetPath(guids[i]) + "  deleted");
            }
            catch
            {
                Debug.LogError(AssetDatabase.GUIDToAssetPath(guids[i]) + "delete fail!");
            }
        }
        AssetDatabase.Refresh();
    }

    static public void delAllPlatformAssetBundle()
    {
        delOtherPlatformAssetBundle(BuildTarget.Android);
        delOtherPlatformAssetBundle(BuildTarget.iOS);
    }


    static void commandLineBuild()
    {
        BuildArgs ba  = parseCommandLine();
        buildFromArgs(ba);
    }

    static void checkBuildTarget()
    {
        BuildArgs ba = parseCommandLine();
        if (ba._buildTarget != EditorUserBuildSettings.activeBuildTarget)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(ba._buildTarget);
        }
    }

    static BuildArgs parseCommandLine()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        Dictionary<BuildArgs.BuildArgsEnum, string> argsDic = new Dictionary<BuildArgs.BuildArgsEnum, string>();
        for (int i = 0; i < args.Length; i++)
        {
            if (i > (int)BuildArgs.BuildArgsEnum.First)
            {
                argsDic.Add((BuildArgs.BuildArgsEnum)i, args[i]);
                Debug.Log(i + " " + args[i]);
            }
            Debug.Log(args[i]);
        }
        BuildArgs ba = new BuildArgs();
        ba.setArgs(argsDic);
        return ba;
    }


    static public void buildAfterTagetSwitch(BuildArgs args)
    {
        Debug.Log("buildAfterTagetSwitch");

        PlayerSettings.applicationIdentifier = args._bundleId;
        PlayerSettings.bundleVersion = args._applicationVer;
        //PlayerSettings.shortBundleVersion = args._shortBundleVersion;


        if (args._buildTarget == UnityEditor.BuildTarget.Android)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
            {
                //buildAB

                Publish.BuildAPK(false, true, false, args._package_filename + ".apk");
            }
        }
        else if (args._buildTarget == UnityEditor.BuildTarget.iOS)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
            //ExportAssetBundles.Publish_Dev_OnKeyAB(BuildTarget.iOS, false);
            BuildForIPhone();
        }
    }

    static public void buildFromArgs(BuildArgs args)
    {
        Debug.Log("buildFromArgs");
        //Publish.delAllPlatformAssetBundle();

        if (args._buildTarget == EditorUserBuildSettings.activeBuildTarget)
        {
            buildAfterTagetSwitch(args);
        }
        else
        {
            EditorUserBuildSettings.activeBuildTargetChanged = delegate()
            {
                buildAfterTagetSwitch(args);
            };
            EditorUserBuildSettings.SwitchActiveBuildTarget(args._buildTarget);
        }
    }

}
