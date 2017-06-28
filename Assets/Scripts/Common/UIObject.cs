using UnityEngine;
using System.Collections;
namespace Stars
{
    public enum WinOpenAnimType
    {
        None,//无动画
        Pop,//弹出窗口
        Animator,//使用Animator实现的界面动画,计划实现中。。。
    }

    public class UIObject : MonoBehaviour
    {
        /// <summary>
        /// 窗口打开动画类型选择
        /// </summary>
        public WinOpenAnimType _winOpenAnimType = WinOpenAnimType.None;
        /// <summary>
        /// 是否显示蒙层
        /// </summary>
        public bool _isShowBlackMask = false;
        /// <summary>
        /// 蒙层是否响应点击并关闭
        /// </summary>
        public bool _isBlackMaskCanClickClose = true;
        TweenAlpha _openTa;
        TweenScale _openTs;
        virtual public void Awake()
        {
            switch (_winOpenAnimType)
            {
                case WinOpenAnimType.Pop:
                    createPopAnim();
                    break;
            }
        }

        virtual public void OnDestroy()
        {

        }

        public void setWinAnimIgnoreTimeScale(bool isIgnore)
        {
            if (_winOpenAnimType == WinOpenAnimType.Pop)
            {
                if (_openTa != null)
                {
                    _openTa.ignoreTimeScale = isIgnore;
                }
                if (_openTs != null)
                {
                    _openTs.ignoreTimeScale = isIgnore;
                }
            }
        }

        public virtual void show()
        {
            gameObject.SetActive(true);
            switch (_winOpenAnimType)
            {
                case WinOpenAnimType.Pop:
                    playPopAnim();
                    break;
            }
        }

        public virtual void hide()
        {
            gameObject.SetActive(false);
        }

        void createPopAnim()
        {
            _openTa = gameObject.AddComponent<TweenAlpha>();
            _openTs = gameObject.AddComponent<TweenScale>();

            _openTa.ignoreTimeScale = false;
            _openTa.duration = 5.0f / 30;
            _openTa.from = 0.01f;
            _openTa.to = 1.0f;
            _openTa.enabled = false;

            _openTs.ignoreTimeScale = false;
            _openTs.duration = 15.0f / 30;
            _openTs.from = new Vector3(0.6f, 0.6f, 0.6f);
            _openTs.to = new Vector3(1, 1, 1);
            _openTs.animationCurve = new AnimationCurve();
            Keyframe[] newKeys = new Keyframe[4];
            newKeys[0] = new Keyframe(0f, 0f, 2.987178f, 2.987178f);
            newKeys[1] = new Keyframe(0.5285335f, 1.095492f, 0f, 0f);
            newKeys[2] = new Keyframe(0.7976344f, 0.9515332f, 0f, 0f);
            newKeys[3] = new Keyframe(1f, 1f, 0.5183413f, 0.5183413f);
            for (int i = 0; i < newKeys.Length; i++)
            {
                newKeys[i].tangentMode = 21;
                _openTs.animationCurve.AddKey(newKeys[i]);
            }
            _openTs.enabled = false;
            _openTs.AddOnFinished(onOpenAnimFinished);
        }

        void playPopAnim()
        {
            _openTs.ResetToBeginning();
            _openTs.PlayForward();
            _openTa.ResetToBeginning();
            _openTa.PlayForward();
        }

        void onOpenAnimFinished()
        {
            transform.localScale = Vector3.one;
            onOpenAnimFinishedCallback();
        }

        virtual protected void onOpenAnimFinishedCallback()
        {

        }
        /// <summary>
        /// 点击蒙层的回调
        /// </summary>
        virtual protected void onMaskClose()
        {
            hide();
        }
    }
}