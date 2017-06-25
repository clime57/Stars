using System;

namespace Stars
{
    /// <summary>
    /// Loader 父类
    /// </summary>
    public abstract class AssetBundleLoader : IComparable<AssetBundleLoader>
    {
        static int idCounter = 0;

        public AssetBundleManager.LoadAssetCompleteHandler onComplete;
        public string bundleName;
        public AssetBundleData bundleData;
        public AssetBundleInfo bundleInfo;
        public AssetBundleManager bundleManager;
        public LoadState state = LoadState.State_None;

        protected AssetBundleLoader[] depLoaders;

        private int _prority;

        public AssetBundleLoader()
        {
            id = idCounter++;
        }
        
        public virtual void Start()
        {
            id = idCounter++;
        }

        /// <summary>
        /// 其它都准备好了，加载AssetBundle
        /// 注意：这个方法只能被 AssetBundleManager 调用
        /// 由 Manager 统一分配加载时机，防止加载过卡
        /// </summary>
        public virtual void LoadBundle()
        {

        }

        public virtual AssetBundleInfo LoadBundleSync()
        {
            return null;
        }

        public int id
        {
            get; private set;
        }

        public int prority
        {
            get { return _prority; }
            set
            {
                if (_prority != value)
                {
                    _prority = value;
                    RefreshPrority();
                }
            }
        }

        public virtual bool isComplete
        {
            get
            {
                return state == LoadState.State_Error || state == LoadState.State_Complete;
            }
        }

        protected void RefreshPrority()
        {
            for (int i = 0; depLoaders != null && i < depLoaders.Length; i++)
            {
                AssetBundleLoader dep = depLoaders[i];
                if (dep.prority < prority)
                    dep.prority = prority;
            }
        }

        protected virtual void Complete()
        {
            FireEvent();
            bundleManager.LoadComplete(this);
        }

        protected virtual void Error()
        {
            FireEvent();
            bundleManager.LoadError(this);
        }

        public void FireEvent()
        {
            if (onComplete != null)
            {
                var handler = onComplete;
                onComplete = null;
                handler(bundleInfo);
            }
        }

        int IComparable<AssetBundleLoader>.CompareTo(AssetBundleLoader other)
        {
            if (other.prority == prority)
                return id.CompareTo(other.id);
            else
                return other.prority.CompareTo(prority);
        }
    }
}
