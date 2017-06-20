using UnityEngine;
using System.Collections;

public class MsgBox : UIWindow
{
    static MsgBox _instance = null;
    public UILabel _tip = null;
    public GameObject _goOK;
    public GameObject _goYesCancel;

    // bool _alreadyClick = false;   //���Ѿ�����ˣ���Ҫ�ȵ����غ�����ٴε��
    bool _OpenAgain = false;     //���ٴδ򿪴˽����Ͳ���Ҫ���ش˽��棬trueΪ�ٴδ򿪴˽���

    public delegate void CallBack();
    CallBack callback_;
    public delegate void CancelCallBack();  //ȷ��ȡ���Ļص�����
    CancelCallBack _cancelCallBack;

    public enum ShowType
    {
        OK,
        YesCancel,				//ȡ��/ȷ��
        EscCancel
    }
    void Awake()
    {
        base.Awake();
        base._beDeleteWhenBackToLogin = false;
        _instance = this;
        gameObject.SetActive(false);
    }

    public override void resetWhenBackToLogin()
    {
        hide();
    }

    public void onOK()
    {
        FinishOKCallback();
    }

    void FinishOKCallback()
    {
        if (callback_ != null)
        {
            callback_();
        }

        if (!_OpenAgain)
        {
            base.hide();
            gameObject.SetActive(false);
            callback_ = null;
        }
        else
        {
            _OpenAgain = false;
        }
    }

    public void onCancel()
    {
        FinishCancelCallBack();
    }

    void FinishCancelCallBack()
    {
        callback_ = null;
        if (_cancelCallBack != null)
        {
            _cancelCallBack();
            _cancelCallBack = null;
        }
        base.hide();
        gameObject.SetActive(false);

    }
    public void hide()
    {
        base.hide();
        gameObject.SetActive(false);
    }

    public bool Visible
    {
        get
        {
            return gameObject.activeSelf;
        }
    }

    public void show(string str, MsgBox.ShowType type = ShowType.OK, MsgBox.CallBack callback = null, UI_LAYER layer = UI_LAYER.COMMON_UI, MsgBox.CancelCallBack cancelCallback = null)
    {
        if (_tip != null) _tip.text = str;
        if (string.IsNullOrEmpty(str))
        {
            TyCrashReport.reportException(new System.Exception(), "msgbox content is empty");
        }
        if (callback_ != null)
        {
            base.hide();
            _OpenAgain = true;
        }
        gameObject.SetActive(true);

        {
            gameObject.transform.localPosition = new Vector3(0, 0, -(int)layer);
        }

        UIPanel panel = GetComponent<UIPanel>();

        if (panel != null)
        {
            panel.depth = (int)layer;
        }
        setWinAnimIgnoreTimeScale(Time.timeScale != 1);//���ʱ�����Ų�Ϊ1������Ҫ��������
        base.show();
        hideAll();
        callback_ = callback;
        _cancelCallBack = cancelCallback;
        if (type == ShowType.OK)
        {
            _goOK.SetActive(true);
        }
        else if (type == ShowType.YesCancel)
        {
            _goYesCancel.SetActive(true);
        }
        
    }


    void hideAll()
    {
        _goOK.SetActive(false);
        _goYesCancel.SetActive(false);
    }
    static public MsgBox getInstance()
    {
        return _instance;
    }
    
}
