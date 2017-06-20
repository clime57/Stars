#define ShowTimeMsgBox

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum WinBackgroundType{
    Normal,//2D背景，各模块自行管理
    Common,//通用3D背景
    HeroList,//英雄界面3D背景
    Arena,//竞技场3D背景
    CallHero,   //召唤英灵界面
    Chapter,//章节界面
    MianUI, //主界面
}

public class UIWindow : UIObject
{
    public bool _needDisable3DRayDetection = false;//禁止鼠标穿透UI点击到场景3d物体,目前已无实际用处，暂时保留
    public bool _beHideWhenOtherWindowOpen = false;//隐藏当前正在显示的界面,会调用hide
    public bool _beResetWhenBackToLogin = false;//回到角色选择界面前,会调用resetWhenBackToLogin
    public bool _beDeleteWhenBackToLogin = true;//回到登录时被删除
    public bool _isHide3DObjectWhenVisible = false;//在本窗口打开时是否隐藏3D物体
    protected bool _customSet3DObjectVisible = false;//自定义设置窗口打开时是否隐藏3D物体
    public bool _hideOtherWindowWhenShow = false;//显示时强制隐藏其它窗口
    /// <summary>
    /// 窗口类型
    /// </summary>
    public WinType _winType = WinType.Normal;
    public WinBackgroundType _bgType = WinBackgroundType.Normal;
    /// <summary>
    /// 在预制件上使用的Atlas，在面板上预先设置,从预制件所在的AssetBundle包里加载出来。
    /// </summary>
    public UIAtlas[] atlas_;

    bool visible_ = false;//逻辑上是否可见
    /// <summary>
    /// 是否已点击关闭
    /// </summary>
    bool isReadyToHide_ = false;
    /// <summary>
    /// 窗口名称
    /// </summary>
    string name_ = "";
    /// <summary>
    /// 显示开始时间
    /// </summary>
    float _showBeginTime = 0;

    /// <summary>
    /// 显示窗口
    /// </summary>
    virtual public void show()
    {
        showTimeBegin();
        base.show();
        if (_hideOtherWindowWhenShow && !isVisible())
        {
            Game.getInstance().findObject<UIWindowManager>().hideOtherWindow(this);
        }
        gameObject.SetActive(true);
        if (_isHide3DObjectWhenVisible && !_customSet3DObjectVisible)
        {
            RG_Utils.set3DObjectVisible(false);
        }
        if (_bgType != WinBackgroundType.Normal)
        {
        }
        visible_ = true;
    }

    virtual public void show(CacheWinInfo info)
    {
        show();
    }


    virtual public void hide()
    {
        base.hide();
        realHide();
    }

    //跳转下一个界面时，通知当前界面
    virtual public void JumpNextViwe()
    {

    }

    virtual public void realHide()
    {
        gameObject.SetActive(false);
        if (_isHide3DObjectWhenVisible)
        {
            RG_Utils.set3DObjectVisible(true);
        }
        visible_ = false;
    }

    virtual public void setVisible(bool visible)
    {
        gameObject.SetActive(visible);
        visible_ = visible;
    }

    virtual public bool isVisible()
    {
        return visible_;
    }

    virtual public void resetWhenBackToLogin()
    {
        hide();
    }

    virtual public CacheWinInfo saveState()
    {
        return null; 
    }
    /// <summary>
    /// 得到窗体预制件上的Atlas
    /// </summary>
    /// <param name="atlasName">atlas名称</param>
    /// <returns>找不到返回NULL</returns>
    public UIAtlas getPrefabUIAtlas(string atlasName)
    {
        if (atlas_ != null)
        {
            for (int i = 0; i < atlas_.Length; i++)
            {
                if (atlas_[i].name.Equals(atlasName))
                {
                    return atlas_[i];
                }
            }
        }
        TyLogger.LogError("can't find atlas " + atlasName);
        return null;
    }

    /// <summary>
    /// 得到窗体预制件上的Atlas
    /// </summary>
    /// <param name="index">atlas序号,从0开始</param>
    /// <returns>找不到返回NULL</returns>
    public UIAtlas getPrefabUIAtlas(int index)
    {
        if (atlas_ != null && index < atlas_.Length)
        {
            return atlas_[index];
        }
        TyLogger.LogError("can't find atlas " + index);
        return null;
    }

    override public void Awake()
    {
        base.Awake();
        //Debug.Log("UIWindow Awake");
        regEvent();
    }

    override public void OnDestroy()
    {
        base.OnDestroy();
        removeEvent();
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    virtual protected void regEvent() { }
    /// <summary>
    /// 取消事件
    /// </summary>
    virtual protected void removeEvent() { }
    /// <summary>
    /// 是否处于待关闭状态
    /// </summary>
    public bool IsReadyToHide
    {
        set
        {
            isReadyToHide_ = value;
        }
        get
        {
            return isReadyToHide_;
        }
    }
    /// <summary>
    /// 窗口名称
    /// </summary>
    public string windowName
    {
        set
        {
            name_ = value;
        }
        get
        {
            return name_;
        }
    }
    /// <summary>
    /// 显示窗口计时开始
    /// </summary>
    public void showTimeBegin()
    {
        if (_showBeginTime == 0)
        {
            _showBeginTime = Time.realtimeSinceStartup;
        }
    }
    /// <summary>
    /// 设置窗口计时开始时间
    /// </summary>
    /// <param name="time"></param>
    public void setShowTimeBegin(float time)
    {
        _showBeginTime = time;
    }

    /// <summary>
    /// 显示窗口结束的计时结算
    /// </summary>
    public void showTimeEnd()
    {
        float showTimeDuring = Time.realtimeSinceStartup - _showBeginTime;
        if (showTimeDuring > 1.0f && showTimeDuring < 10)
        {
            string str = windowName + RG_Utils.getLanguageString("CodeItem140");
#if ShowTimeMsgBox
            MsgBox.getInstance().show(str);
#endif
            TyLogger.LogError(str);
        }
    }
    /// <summary>
    /// 窗口销毁时必须调用的函数
    /// </summary>
    virtual public void finalize()
    {

    }
}

