using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
namespace Stars
{
    public enum WinType
    {
        Normal,//普通窗口，默认
        Type1,//类似于副本选择界面，这类界面互斥
        Type2//类似城镇右侧下拉式菜单里的各界面，这类界面互斥
    }
}