using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Reflection;
public class UBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        Assembly ass = Assembly.GetExecutingAssembly();
        return ass.GetType(typeName);
    }
} 
public class DataLoad : GameData {
    int index = 0;//load表 递增值
    float _loadProgress = 0;
    int _loadState = 0;//0,初始,1,正在从存储设备加载资源,2,正在解析,3解析完毕

    float Time1;
     public void load()
    {
        _loadProgress = 0;
        _loadState = 1;

        index = 0;
        Time1 = Time.realtimeSinceStartup;
        loadData();
    }

    void loadData()
     {
         _loadState = 2;
         _loadProgress = index / ConfigCollect.CONFIG_ARRAY.Length;
         //CBResourceManager.loadResource(ConfigCollect.CONFIG_ARRAY[index].ToString(), DataCallBackHandler, true);
     }

    void DataCallBackHandler()
    {
        TextAsset textAsset = null;//= (TextAsset)dr.Data;
        if (textAsset == null)
        {
            Debug.LogError("can not found this table:" + ConfigCollect.CONFIG_ARRAY[index]);
            return;
        }
        Debug.Log("ConfigCollect.CONFIG_ARRAY:" + ConfigCollect.CONFIG_ARRAY[index]);
       IFormatter formatter = new BinaryFormatter();
       formatter.Binder = new UBinder();
       MemoryStream stream = new MemoryStream(textAsset.bytes);
       object[] dataArr = formatter.Deserialize(stream) as object[];
       IConfig[] iConfigArr = Array.ConvertAll<object, IConfig>(dataArr, delegate(object s) { return s as IConfig;});
       ConfigDataManager.addConfigData(ConfigCollect.CONFIG_ARRAY[index], iConfigArr);
        stream.Close();
        if (ConfigCollect.CONFIG_ARRAY.Length <= (index + 1))
        {
            Debug.Log("Run Time:" + (Time.realtimeSinceStartup - Time1).ToString());
            _loadProgress = 1.0f;
            _loadState = 3;
            return;
        }
        AsTableHandler();
        index++;
        loadData();
    }

    //强转表
    private void AsTableHandler()
    {

    }

    public float getLoadProgress()
    {
        {
            return _loadProgress;
        }
    }
}
