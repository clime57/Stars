using Stars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestLoadConfig : MonoBehaviour {
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
        for (int i = 0; i < ConfigCollect.CONFIG_ARRAY.Length; i++)
        {
            LoadConfig loadConfig = new LoadConfig(ConfigCollect.CONFIG_ARRAY[i]);
            loadConfig.load();
        }
        IConfig[] config = ConfigDataManager.GetDataArrByName(ConfigCollect.CONFIG_ARRAY[0].ToString());
        SampleExcelFile sef = config[0] as SampleExcelFile;
        Debug.Log(sef.id);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
