using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stars;
using UnityEngine.SceneManagement;

public class LoadABTest : MonoBehaviour {
    AssetBundleManager manager;
    // Use this for initialization
    void Start () {

        manager = AssetBundleManager.Instance;
        if (AssetBundleManager.Instance == null)
        {
            manager = gameObject.AddComponent<AssetBundleManager>();
        }
        manager.Init(() =>
        {
            LoadObjects();
        });
    }


    void LoadObjects()
    {
        AssetBundleInfo a = manager.LoadSync(@"Assets\ThirdParty\ABSystem\Prefabs\Sphere.prefab");//
        {
            //GameObject go = Instantiate(a.mainObject) as GameObject;//a.Instantiate();
            GameObject go = a.Instantiate(a.mainObject) as GameObject;//a.Instantiate();
            go.transform.localPosition = new Vector3(3, 3, 3);
        }
        //manager.Load("Assets.Prefabs.Cube.prefab.ab", (a) =>
        //{
        //    GameObject go = a.Instantiate();
        //    go.transform.localPosition = new Vector3(6, 3, 3);
        //});
        //manager.Load("Assets.Prefabs.Plane.prefab.ab", (a) =>
        //{
        //    GameObject go = a.Instantiate();
        //    go.transform.localPosition = new Vector3(9, 3, 3);
        //});
        //manager.Load("Assets.Prefabs.Capsule.prefab.ab", (a) =>
        //{
        //    GameObject go = a.Instantiate();
        //    go.transform.localPosition = new Vector3(12, 3, 3);
        //});

        //AssetBundleInfo b = manager.LoadSync(@"Assets\ThirdParty\ABSystem\Scenes\sceneforab1.unity");
        //SceneManager.LoadScene("sceneforab1");
        Stars.LoadScene.Get().load(@"Assets\ThirdParty\ABSystem\Scenes\sceneforab1.unity");
    }
}
