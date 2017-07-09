using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stars
{
    public class ResourceMgr
    {
        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="FilePath">文件路径，比如@"Assets\Prefabs\Sphere.prefab"</param>
        /// <returns>AssetBundleInfo，使用AssetBundleInfo.Instantiate(AssetBundleInfo.mainObject)保持引用</returns>
        public static AssetBundleInfo LoadSync(string FilePath)
        {
            if (AssetBundleManager.Instance != null)
            {
                return AssetBundleManager.Instance.LoadSync(FilePath);
            }
            return null;
        }
    }

}