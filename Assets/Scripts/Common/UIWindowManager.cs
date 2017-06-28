using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Stars
{
    public enum UI_LAYER
    {
        CHAR_NAME = -50,
        MAIN_UI = 0,
        SECONDARY_UI = 101,
        SECONDARYPOINT = 122,
        THIRD_UI = 201,
        FOURTHUI = 251,
        COMMON_UI = 301,
        TOOLTIP = 351,
        SYSTEMNEWS = 401,
        GUIDE = 451,
        MOVIE = 501,
        WAITING = 551,
        LOADING = 601,
        SYSTEMTIP = 651,
    }

    public enum UI_TYPE
    {
        COMMON,
        LOGIN,
        MAIN,
        Movie
    }

    class WindowLoadingData
    {
        //窗口名
        public string name = null;
        public string path;
        public UI_LAYER layer;
        public UI_TYPE ui_type;
        //資源名
        public string resName = null;
        //回調
        public UIWindowManager.LoadWindowCallback callback = null;
    }

    public class CacheWinInfo
    {
        public WinType _winType;
        public object _cacheWinStateInfo;
        public delegate void Resume(CacheWinInfo info);
        public Resume _resume;
        public void resumeWin()
        {
            if (_resume != null)
            {
                _resume(this);
            }
        }
    }
    //窗口加载,移除,获取
    public class UIWindowManager : GameSubSystem
    {
        GameObject UISystem_ = null;
        UIRoot uiRoot_ = null;
        Hashtable windowList_ = null;
        GameObject mainUIPanel_ = null;
        GameObject _tempParentUI = null;
        GameObject mainUIWindow_ = null;
        GameObject commonUIAnchor_ = null;
        GameObject loginUIAnchor_ = null;
        GameObject moviePanel_ = null;
        GameObject mRTPanel = null;
        static UIWindowManager instance_ = null;
        Dictionary<string, WindowLoadingData> _loadList = new Dictionary<string, WindowLoadingData>();
        Dictionary<string, WindowLoadingData>.Enumerator _curLoadEnum;
        float _curWinLoadProgress = 0f;

        public delegate void LoadWindowCallback(UIWindow uiWindow);

        Stack<CacheWinInfo> cacheWinInfos_ = new Stack<CacheWinInfo>();
        /// <summary>
        /// 资源自动释放计时
        /// </summary>
        System.Diagnostics.Stopwatch _resReleaseSw = new System.Diagnostics.Stopwatch();
        /// <summary>
        /// 销毁窗口后释放资源达到的最大时间
        /// </summary>
        const short _maxDestroyWinAutoReleaseResTime = 15000;
        /// <summary>
        /// 销毁窗口计数
        /// </summary>
        byte _destroyWinCount = 0;
        /// <summary>
        /// 销毁窗口后释放资源达到的最大次数
        /// </summary>
        const byte _maxDestroyWinAutoReleaseResCount = 5;

        public UIWindowManager()
        {
            instance_ = this;
            _resReleaseSw.Start();
        }

        public static UIWindowManager getInstance()
        {
            return instance_;
        }

        override public void init()
        {
            GameObject go = GameObject.Find("UISystem");
            windowList_ = new Hashtable();
            if (go != null)
            {
                UISystem_ = go;
                loadMainUI();
            }
        }


        public void loadMainUI()
        {
            GameObject MainUI = RG_Utils.loadPrefeb("MainUIRoot", "Reserved/UI/MainUI/MainUIRoot");
            if (UISystem_ != null)
            {
                MainUI.transform.parent = UISystem_.transform;
                uiRoot_ = MainUI.GetComponent<UIRoot>();
            }
            commonUIAnchor_ = GameObject.Find("ComCenterAnchor");
            loginUIAnchor_ = GameObject.Find("LoginCenterAnchor");
            moviePanel_ = GameObject.Find("MoviePanel");
            UICamera MainUICamera = MainUI.GetComponentInChildren<UICamera>();
            GameObject MainUiWindow = RG_Utils.loadPrefeb("MainUIWindow", "Reserved/UI/MainUI/MainUIWindow");
            MainUiWindow.gameObject.SetActive(true);
            MainUiWindow.transform.parent = MainUICamera.transform;
            mainUIPanel_ = GameObject.Find("MainUIPanel");
            _tempParentUI = GameObject.Find("TempParentUI");
            mainUIWindow_ = GameObject.Find("MainUIWindow");
            UIWindow win = MainUiWindow.GetComponent<UIWindow>();
            windowList_.Add("MainUIWindow", win);
            MainUiWindow.SetActive(false);

        }

        public void setMainUIWindowVisible(bool visible)
        {
            mainUIWindow_.SetActive(visible);
        }

        public bool isMainUIWindowVisible()
        {
            return mainUIWindow_.activeSelf;
        }

        public UIWindow setWindow(string name, GameObject go, UI_LAYER layer, UI_TYPE ui_type)
        {
            switch (ui_type)
            {
                case UI_TYPE.COMMON:
                    go.transform.parent = commonUIAnchor_.transform;
                    break;
                case UI_TYPE.LOGIN:
                    go.transform.parent = loginUIAnchor_.transform;
                    go.transform.localPosition = Vector3.zero;
                    break;
                case UI_TYPE.MAIN:
                    go.transform.parent = mainUIPanel_.transform;
                    break;
                case UI_TYPE.Movie:
                    go.transform.parent = moviePanel_.transform;
                    break;
            }
            UIWindow win = go.GetComponent<UIWindow>();
            if (win != null)
            {
                win.windowName = name;
                if (!windowList_.ContainsKey(name))
                {
                    windowList_.Add(name, win);
                }
                else
                {
                    windowList_[name] = win;
                }
            }
            setLayer(go, layer);
            go.SetActive(false);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.name = name;
            return win;
        }

        //同步加载窗口
        public UIWindow loadWindowSync(string winName, string srcName, UI_LAYER layer, UI_TYPE ui_type)
        {
            AssetBundleInfo abInfo = AssetBundleManager.Instance.LoadSync(srcName);
            GameObject go = GameObject.Instantiate(abInfo.mainObject) as GameObject;
            UIWindow win = setWindow(winName, go, layer, ui_type);
            return win;
        }

        public void destroyWindow(string name)
        {
            UIWindow win = find(name);
            destroyWindow(win);
        }

        public void destroyWindow(UIWindow win)
        {
            if (win != null)
            {
                string name = win.windowName;
                win.finalize();
                GameObject.Destroy(win.gameObject);
                windowList_.Remove(name);
                autoReleaseRes();
            }
        }

        void autoReleaseRes()
        {
            _destroyWinCount++;
            if (_resReleaseSw.ElapsedMilliseconds > _maxDestroyWinAutoReleaseResTime && _destroyWinCount > _maxDestroyWinAutoReleaseResCount)
            {
                Resources.UnloadUnusedAssets();
                _resReleaseSw.Reset();
                _resReleaseSw.Start();
                _destroyWinCount = 0;
                TyLogger.Log("UIWindowManager AutoReleaseRes");
            }
        }

        public void addToLoadingList(string name, string path, string resName, bool isInAssetList, UI_LAYER layer, UI_TYPE ui_type)
        {
            WindowLoadingData data = new WindowLoadingData();
            data.name = name;
            data.path = path;
            if (isInAssetList)
                data.resName = resName;
            data.layer = layer;
            data.ui_type = ui_type;
            _loadList.Add(name, data);
        }

        public void setLayer(GameObject go, UI_LAYER layer)
        {
            UIWidget[] uiWindgets = go.GetComponentsInChildren<UIWidget>(true);
            foreach (UIWidget widget in uiWindgets)
            {
                widget.depth += (int)layer;
            }
            UIPanel panel = go.GetComponent<UIPanel>();

            UIPanel[] panels = go.GetComponentsInChildren<UIPanel>(true);
            foreach (UIPanel p in panels)
            {
                p.depth += (int)layer;
            }
            go.transform.localPosition = new Vector3(0, 0, 0 - (int)layer);
        }

        public UIWindow find(string name)
        {
            if (windowList_.Contains(name))
            {
                return (UIWindow)windowList_[name];
            }
            return null;
        }

        public void show(string name)
        {
            UIWindow win = find(name);
            if (win != null)
            {
                win.show();
            }
        }

        public void hide(string name)
        {
            UIWindow win = find(name);
            if (win != null)
            {
                win.hide();
            }
        }
        //隐藏其它界面
        public void hideOtherWindow(UIWindow newOpenWin = null)
        {
            List<string> delWin = new List<string>();
            foreach (System.Collections.DictionaryEntry objDE in windowList_)
            {
                UIWindow win = objDE.Value as UIWindow;
                if (win._beHideWhenOtherWindowOpen && win.isVisible())
                {
                    delWin.Add((string)objDE.Key);
                }
            }
            foreach (string winName in delWin)
            {
                Debug.Log("delWin = " + winName);
                UIWindow win = windowList_[winName] as UIWindow;
                if (win != null)
                {
                    compareWinType(newOpenWin, win);
                    win.hide();
                    if (win != null && win.gameObject != null)
                    {
                        GameObject.Destroy(win.gameObject);
                    }
                    windowList_.Remove(winName);
                }
            }
        }

        void compareWinType(UIWindow newOpenWin, UIWindow hideWin)
        {
            //Debug.Log(newOpenWin._winType + " " + hideWin._winType);
            if (newOpenWin != null && newOpenWin._winType != WinType.Normal
                && hideWin._winType != WinType.Normal
                && newOpenWin._winType != hideWin._winType && hideWin.IsReadyToHide == false)
            {
                pushCacheWinInfo(hideWin.saveState());
            }
        }

        public void setVisible(string name, bool visible)
        {
            UIWindow win = find(name);
            if (win != null)
                win.setVisible(visible);
        }

        public void setUISystemVisible(bool visible)
        {
            if (UISystem_ != null)
                UISystem_.SetActive(visible);
        }

        public GameObject getUISystem()
        {
            return UISystem_;
        }

        public GuideObject findGuideObject(string str)
        {
            GuideObject[] objs = UISystem_.GetComponentsInChildren<GuideObject>(true);
            foreach (GuideObject obj in objs)
            {
                if (obj._name == str && obj.IsCopy == false)
                {
                    return obj;
                }
            }
            return null;
        }

        public GameObject getMainUIPanel()
        {
            return mainUIPanel_;
        }

        public GameObject getTempParentUI()
        {
            return _tempParentUI;
        }


        public GameObject addPrefebToWindow(GameObject parent, string path, UI_LAYER layer, string name = "")
        {
            return addPrefebToWindowStatic(parent, path, layer, name);
        }

        static public GameObject addPrefebToWindowStatic(GameObject parent, string path, UI_LAYER layer, string name = "")
        {
            GameObject go = RG_Utils.loadPrefeb(name, path);
            if (go != null)
            {
                go.transform.parent = parent.transform;
                UIWidget[] uiWindgets = go.GetComponentsInChildren<UIWidget>(true);
                foreach (UIWidget widget in uiWindgets)
                {
                    widget.depth += (int)layer;
                }
                go.transform.localPosition = new Vector3(0, 0, -10);
                go.transform.localScale = new Vector3(1, 1, 1);
            }
            return addGameObjectToWindowStatic(parent, go, layer, name, false);
        }

        static public GameObject addGameObjectToWindowStatic(GameObject parent, GameObject myObj, UI_LAYER layer, string name = "", bool needCopy = true)
        {
            GameObject go = myObj;
            if (needCopy)
            {
                go = GameObject.Instantiate(myObj) as GameObject;
            }
            if (go != null)
            {
                go.transform.parent = parent.transform;
                UIWidget[] uiWindgets = go.GetComponentsInChildren<UIWidget>(true);
                foreach (UIWidget widget in uiWindgets)
                {
                    widget.depth += (int)layer;
                }
                go.transform.localPosition = new Vector3(0, 0, -10);
                go.transform.localScale = new Vector3(1, 1, 1);
            }
            return go;
        }

        public void backToLogin()
        {
            deleteWindowWhenBackToLogin();
            cacheWinInfos_.Clear();
        }
        /// <summary>
        /// 删除在游戏中返回登陆界面时不用的界面
        /// </summary>
        public void deleteWindowWhenBackToLogin()
        {
            List<UIWindow> _delWin = new List<UIWindow>();
            foreach (DictionaryEntry de in windowList_)
            {
                UIWindow win = de.Value as UIWindow;
                if (win._beDeleteWhenBackToLogin)
                {
                    _delWin.Add(win);
                }
                else
                {
                    win.resetWhenBackToLogin();
                }
            }

            foreach (UIWindow win in _delWin)
            {
                win.resetWhenBackToLogin();
                destroyWindow(win.windowName);
            }
        }

        public float getCurWinLoadingProgress()
        {
            return _curWinLoadProgress;
        }

        public UIRoot getUIRoot()
        {
            return uiRoot_;
        }

        public void pushCacheWinInfo(CacheWinInfo info)
        {
            if (info != null)
            {
                cacheWinInfos_.Push(info);
            }
            else
            {
                Debug.Log("pushCacheWinInfo info = null");
            }
        }

        public CacheWinInfo popCacheWinInfo()
        {
            if (cacheWinInfos_.Count > 0)
            {

                CacheWinInfo info = cacheWinInfos_.Pop();
                return info;
            }
            else
            {
                Debug.Log("popCacheWinInfo cacheWinInfos_ count = 0");
                return null;
            }
        }

        public int getCacheWinInfoCount()
        {
            return cacheWinInfos_.Count;
        }

        public CacheWinInfo resumeCacheWin(UIWindow win)
        {
            win.IsReadyToHide = true;
            CacheWinInfo info = popCacheWinInfo();
            if (info != null)
            {
                info.resumeWin();
            }
            return info;
        }

        public void clearCacheWinInfo()
        {
            cacheWinInfos_.Clear();
        }
    }

}