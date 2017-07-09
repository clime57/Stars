using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
class a
{

}

public class TestCreate : MonoBehaviour {
    AssetBundle assetbundle;
    // Use this for initialization
    void Start () {
        //string path = Application.dataPath + "!assets/AssetBundles/82d969bfaccfa20a2e968295ebc25e3076e48602.ab";

        string path = Application.streamingAssetsPath + "/AssetBundles/82d969bfaccfa20a2e968295ebc25e3076e48602.ab";
        assetbundle = AssetBundle.LoadFromFile(path);

        GameObject go = (GameObject)assetbundle.LoadAsset<GameObject>("Sphere");
        GameObject go1 = GameObject.Instantiate(go);
        go1.transform.position = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void TestCreateA()
    {
    }
}
