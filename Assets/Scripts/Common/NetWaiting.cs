using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetWaiting : UIWindow
{
    public static NetWaiting _instance;
    GameObject _thisGameObject;
    public UILabel _text;
    public UISprite _back;
    int count = 0;
    float _time = 0;
    float _tipShowTime = 1.0f;

    public List<TweenPosition> _tp1;
    public List<TweenPosition> _tp2;
    public TweenColor[] _tc;

    byte _curPointId = 0;
    public float _nextPointTime = 2.0f;
    float _pointTime = 0;


    byte _curTextId = 0;
    const float _nextTextTime = 2.0f / 6;
    float _textTime = 0;
    /// <summary>
    /// 改变动画的时间间隔 秒为单位
    /// </summary>
    const byte changeAnimTime = 10;
    /// <summary>
    /// 动画类型总数
    /// </summary>
    const byte atlasCount = 4;
    /// <summary>
    /// atlas名称前半部分，后面跟着数字，1开始
    /// </summary>
    const string atlasName = "nwAnim";
    /// <summary>
    /// 动画精灵名称前缀
    /// </summary>
    const string namePrefix = "nw";
    /// <summary>
    /// 放动画的精灵
    /// </summary>
    public UISprite animSp_;
    /// <summary>
    /// 上次替换动画的时间
    /// </summary>
    float lastTimeChageAnim = (float)(-changeAnimTime);
    void Awake()
    {
        _instance = this;
        _thisGameObject = gameObject;
        _thisGameObject.SetActive(false);
        count = 0;
        base._beDeleteWhenBackToLogin = false;
        TweenPosition[] pos = GetComponentsInChildren<TweenPosition>(true);
        for (int i = 0; i < pos.Length; i++)
        {
            if (pos[i].tweenGroup == 0)
            {
                _tp1.Add(pos[i]);
            }
            else
            {
                _tp2.Add(pos[i]);
            }
        }
    }
    
    void Update()
    {
        if (_time > _tipShowTime && isTipVisible() == false)
        {
            setTipVisible(true);
        }
        _time += Time.deltaTime;
        if (_time > 50000.0f)
        {
            if(AutoHideMsgBox.getInstance() != null) AutoHideMsgBox.getInstance().show(RG_Utils.getLanguageString("CodeItem143"));
            hide();
        }

        _pointTime += Time.deltaTime;
        if (_pointTime > _nextPointTime)
        {
            _pointTime = 0;
            _curPointId++;
            if (_curPointId >= 12)
            {
                _curPointId = 0;
            }
        }

        _textTime += Time.deltaTime;
        if (_textTime > _nextTextTime)
        {
            _textTime = 0;
            _tp2[_curTextId].ResetToBeginning();
            _tp2[_curTextId].PlayForward();
            _tp1[_curTextId].ResetToBeginning();
            _tp1[_curTextId].PlayForward();


            _curTextId++;
            if (_curTextId >= 6)
            {
                _curTextId = 0;
            }
        }

    }

    public void show(string info = "",float showTipTime = 2)
    {
        count++;
        _time = 0;
        _tipShowTime = showTipTime;

        if (_thisGameObject.activeSelf == false)
        {
           setTipVisible(false);
        }
        _thisGameObject.SetActive(true); 
    }

    void setTipVisible(bool visible)
    {
        _back.gameObject.SetActive(visible);
        if (visible)
        {
            for (int i = 0; i < _tp1.Count; i++)
            {
                _tp1[i].ResetToBeginning();
            }
            changeAnim();
        }
    }

    void changeAnim()
    {
        if(Time.realtimeSinceStartup - lastTimeChageAnim > changeAnimTime)
        {

        }
    }

    bool isTipVisible()
    {
        return _back.gameObject.activeSelf;
    }

    public override void hide()
    {
        count--;
        if (count <= 0)
        {
            _thisGameObject.SetActive(false);
            count = 0;
            setTipVisible(false);
        }
    }

    public static NetWaiting getInstance()
    {
        return _instance;
    }

    public void AllHide()
    {
        _thisGameObject.SetActive(false);
        count = 0;
    }

    public override void resetWhenBackToLogin()
    {
        AllHide();
    }
}
