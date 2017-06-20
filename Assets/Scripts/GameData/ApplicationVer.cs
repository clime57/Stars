using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ApplicationVer
{
    //初始的程序版本的字符串格式
    public static string _origenalAppVer = "0.0.0.1";
    //存取64位整型程序
    static public UInt64 UInt64Ver
    {
        get
        {
            return 1;
        }
    }
    /// <summary>
    /// 得到版本字符串
    /// </summary>
    static public string StringVer
    {
        get
        {
            return _origenalAppVer;
        }

        set
        {
            _origenalAppVer = value;
        }
    }
}

