using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
namespace Stars
{

    public class LoadConfig
    {

        float _loadProgress = 0;
        int _loadState = 0;//0,初始,1,正在从存储设备加载资源,2,正在解析,3解析完毕
        Type configType = null;
        public LoadConfig(Type _type)
        {
            configType = _type;
        }

        public void load()
        {
            _loadState = 2;
            _loadProgress = 0.5f;
        }

        void DataCallBackHandler()
        {
            TextAsset textAsset = null;
            if (textAsset == null)
            {
                Debug.LogError("can not found this table:" + configType);
                return;
            }
            TyLogger.Log("ConfigCollect.CONFIG_ARRAY:" + configType);
            IFormatter formatter = new BinaryFormatter();
            formatter.Binder = new UBinder();
            MemoryStream stream = new MemoryStream(textAsset.bytes);
            object[] dataArr = formatter.Deserialize(stream) as object[];
            IConfig[] iConfigArr = Array.ConvertAll<object, IConfig>(dataArr, delegate (object s) { return s as IConfig; });
            ConfigDataManager.addConfigData(configType, iConfigArr);
            stream.Close();
            _loadProgress = 1.0f;
            _loadState = 3;
        }

        public float getLoadProgress()
        {
            {
                return _loadProgress;
            }
        }
    }

}