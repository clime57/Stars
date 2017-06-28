using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Stars;
using System;
using System.IO;

namespace Stars
{
    public class LoadScene : MonoBehaviour
    {
        public static string _loadingScene = "";
        private float time;
        AsyncOperation async;
        int progress = 0;
        public static LoadScene _instance = null;

        static public Action _loadingEndCallBack = null;
        static public Action _loadingEndPreCallBack = null;
        /// <summary>
        /// 隐藏loading界面时的回调
        /// </summary>
        static public Action _hideLoadingCallBack = null;
        bool _isLoading = false;
        bool _couldLoadScene = false;
        AssetBundle bundle = null;
        string _scenePath = "";

        public float hideloadingTime = 1.0f;
        //加载完，回调完毕是否隐藏加载界面
        public bool _isLoadedHideLoadingView = true;
        bool _isHideUnloadDR = true;

        void Awake()
        {
            _instance = this;
        }

        public static LoadScene Get()
        {
            return _instance;
        }

        void OnDestroy()
        {
            StopAllCoroutines();
            if (bundle != null)
            {
                bundle.Unload(false);
                bundle = null;
            }
        }

        void Update()
        {
            if (_isLoading)
            {
                //if (_couldLoadScene == true)
                //{

                //    _couldLoadScene = false;
                //    return;
                //}
                if (async == null)
                    return;

                //setProgress(async.progress);

                if (async != null && async.progress == 1.0f)
                {
                    async = null;
                    if (_loadingEndPreCallBack != null)
                        _loadingEndPreCallBack();
                    _loadingEndPreCallBack = null;
                    //if (Net.getInstance() != null)
                    //    Net.getInstance().setActivate(true);
                    Invoke("hideloading", hideloadingTime);

                }
            }
        }

        public void setHideLoading(float time = 0.2f)
        {
            if (time == 0)
            {
                SceneHideLoading();
            }
            else
            {
                Invoke("SceneHideLoading", time);
            }
        }


        void hideloading()
        {
            if (_loadingEndCallBack != null)
                _loadingEndCallBack();
            _loadingEndCallBack = null;
            if (bundle != null)
            {
                bundle.Unload(false);
                bundle = null;
            }

            if (Game.getInstance() != null && (_isLoadedHideLoadingView == true && hideloadingTime != 0))
            {
                SceneHideLoading();
            }
            _isLoading = false;
            async = null;
            StopAllCoroutines();
        }

        void SceneHideLoading()
        {
            //UIhide() or Destroy;
            if (_hideLoadingCallBack != null)
            {
                TyLogger.Log("SceneHideLoading _hideLoadingCallBack -> only for test clime");
                _hideLoadingCallBack();
                _hideLoadingCallBack = null;
            }
        }
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="ScenePath">场景路径 例如"Assets\Scene\aaa.unity"</param>
        public void load(string ScenePath)
        {
            beginload(ScenePath);
        }


        void beginload(string ScenePath = "")
        {
            if (Game.getInstance() != null)
            {
                //UIshow();
                //setProgress(0);
                //if (Net.getInstance() != null)
                //    Net.getInstance().setActivate(false);
            }
            try
            {
                SceneManager.sceneLoaded += OnSceneWasLoaded;
                SceneManager.LoadScene("empty");
            }
            catch
            {

            }
            _isLoading = true;
            _scenePath = ScenePath;
            _couldLoadScene = false;
            _loadingScene = Path.GetFileNameWithoutExtension(ScenePath);

        }

        public void OnSceneWasLoaded(Scene scene,LoadSceneMode mode)
        {
            if (scene.name == "empty" && isLoading())//empty场景加载完成,并处于有背景的加载过程中
            {
                AssetBundleManager.Instance.LoadAsync(_scenePath, (a) =>
                {
                    _couldLoadScene = true;

                    StopAllCoroutines();
                    async = SceneManager.LoadSceneAsync(_loadingScene);
                });
            }
            SceneManager.sceneLoaded -= OnSceneWasLoaded;
        }


        public bool isLoading()
        {
            return _isLoading;
        }

        public void resetWhenBackToLogin()
        {
            TyLogger.Log("loading resetWhenBackToLogin");
            StopAllCoroutines();
            if (bundle != null)
            {
                bundle.Unload(false);
                bundle = null;
            }
            _isLoading = false;
            async = null;
            _loadingEndPreCallBack = null;
            _loadingEndCallBack = null;
            CancelInvoke();
        }
    }
}
