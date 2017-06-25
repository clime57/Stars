using UnityEngine;
using System.Collections;

public class Waiting : UIWindow
{
    public static Waiting _instance;
    GameObject _thisGameObject;
    public UILabel _text;
    public UISprite _back;
    int count = 0;
    float _time = 0;
    float _tipShowTime = 1.0f;
    void Awake()
    {
        _instance = this;
        _thisGameObject = gameObject;
        _thisGameObject.SetActive(false);
        base._beDeleteWhenBackToLogin = false;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time > _tipShowTime && isTipVisible() == false)
        {
            setTipVisible(true);
        }
        _time += Time.deltaTime;
        if (_time > 15.0f)
        {
            Debug.LogError("很抱歉在加载过程出现了一个错误!");
            AutoHideMsgBox.getInstance().show(RG_Utils.getLanguageString("CodeItem144"));
            hide();
        }
    }
    /// <summary>
    /// 显示waiting界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="showTipTime"></param>
    /// <param name="showMsgVisible">判断是否显示‘玩命加载中’这几个字，true为显示</param>
    public void show(string info = "",float showTipTime = 0.5f,bool showMsgVisible=true)
    {
        count++;
        _text.text = info;
        if (info == "")
        {
            if (showMsgVisible)
            {
                _text.text = RG_Utils.getLanguageString("CodeItem145");
            }
            else {
                _text.text = "";
            }
        }
        //_text.enabled = info != "";
        //_back.enabled = info != "";
        //_thisGameObject.SetActive(true);
        _time = 0;
        _tipShowTime = showTipTime;
        _thisGameObject.SetActive(true);
        setTipVisible(false);
    }

    /// <summary>
    /// 缺省模式
    /// </summary>
    public override void show()
    {
        show("", 0.5f, true);
    }

    /// <summary>
    /// 开闭模式
    /// </summary>
    /// <param name="showMsgVisible"></param>
    public void show(bool showMsgVisible)
    {
        show("", 0.5f, showMsgVisible);
    }

    void setTipVisible(bool visible)
    {
        _back.gameObject.SetActive(visible);
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
        }
    }
    public void AllHide()
    {
        _thisGameObject.SetActive(false);
        count = 0;
    }

    public static Waiting getInstance()
    {
        return _instance;
    }

    public override void resetWhenBackToLogin()
    {
        AllHide();
    }
}
