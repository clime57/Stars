using UnityEngine;
using System.Collections.Generic;
using System;

public interface IConfig
{
    uint id { get; }

   

}

public class ConfigDataManager
{
    //数据字典
    private static Dictionary<Type, IConfig[]> dataDictionary = new Dictionary<Type, IConfig[]>();

    /// <summary>存储序列化数据</summary>
    public static void addConfigData(Type key, IConfig[] dataArr)
    {
        dataDictionary.Add(key, dataArr);
    }
     
    /// <summary>通过表名获取表引用</summary>
    public static IConfig[] GetDataArrByName(Type tableName)
    {
        if (false == dataDictionary.ContainsKey(tableName))
        {
            Debug.LogError("can not found this table:" + tableName);
            return null;
        }
        return dataDictionary[tableName];
    }
    public static IConfig[] GetDataArrByName(string tableName)
    {
        Type t = Type.GetType(tableName);

        return GetDataArrByName(t);
    }
    public static T[] GetDataArrByName<T>()
    {
        Type t = typeof(T);
        
        IConfig[] iconfigs = GetDataArrByName(t);
        T[] ts = new T[iconfigs.Length];
        for (int i = 0; i < iconfigs.Length; i++)
        {
            ts[i] = (T)iconfigs[i];
        }
        return ts;
    }

    public static Dictionary<uint, T> GetDataDictionary<T>() where T : IConfig
    {
        Dictionary<uint, T> result = new Dictionary<uint, T>();
        Type t = typeof(T);
        IConfig[] iconfigs = GetDataArrByName(t);
        for (int i = 0; i < iconfigs.Length; i++)
        {
            result[iconfigs[i].id] = (T)iconfigs[i];
        }

        return result;
    }
    /// <summary>根据id获取相关数据</summary>
    public static IConfig GetDataById(Type tableName, int id)
    {
        if (false == dataDictionary.ContainsKey(tableName))
        {
            Debug.LogError("can not found this table:" + tableName);
            return null;
        }
        else
        {
            IConfig[] arr = dataDictionary[tableName];
            int len = arr.Length;
            IConfig data = null;
            for (int i = 0; i < len; i++)
            {
                data = arr[i];
                if (data.id == id)
                {
                    return data;
                }
                else
                {
                    continue;
                }
            }
        }
        Debug.LogError("can not found this id:" + id.ToString());
        return null;
    }

}
