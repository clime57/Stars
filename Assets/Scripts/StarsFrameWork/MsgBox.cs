using UnityEngine;
using System.Collections;
namespace Stars
{
    public class MsgBox : UIWindow
    {
        static MsgBox _instance = null;
        public UILabel _tip = null;
        public GameObject _goOK;
        public GameObject _goYesCancel;

        // bool _alreadyClick = false;   //当已经点击了，需要等到隐藏后才能再次点击
        bool _OpenAgain = false;     //当再次打开此界面后就不需要隐藏此界面，true为再次打开此界面

        public delegate void CallBack();
        CallBack callback_;
        public delegate void CancelCallBack();  //确定取消的回调函数
        CancelCallBack _cancelCallBack;

        public enum ShowType
        {
            OK,
            YesCancel,              //取消/确定
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
            setWinAnimIgnoreTimeScale(Time.timeScale != 1);//如果时间缩放不为1，则需要忽略缩放
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
}