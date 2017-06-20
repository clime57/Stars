using UnityEngine;
using System.Collections;

public class GuideObject : Message {
    public string _name = "";
    //如果设置目标，在其之上显示提示会基于这个depth之上显示
    public GameObject _depthObj = null;
    public GameObject _needInstantiateObj;   //需要复制的物体，用于复制一份放在压黑背景上
    bool _isCopy = false;
    public void OnClick()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Global");
        if (obj != null) {
//            Debug.Log("GuideObject = " + _name);
            obj.SendMessage("processMsg", gameObject);
        }
    }

    void Start()
    {
        _objectType = "GuideObject";
    }

    public bool IsCopy
    {
        get
        {
            return _isCopy;
        }
        set
        {
            _isCopy = value;
        }
    }
}
