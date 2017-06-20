using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AutoHideMsgBox : MonoBehaviour
{
    static AutoHideMsgBox _instance = null;
	public UILabel _tip = null;
	void Awake(){
		_instance = this;
		gameObject.SetActive(false);
	}
	void Start () {

	}
	void Update () {
	
	}
	public void show(string str){
        gameObject.SetActive(true);
        _tip.text = str;
        gameObject.GetComponentInChildren<UISprite>().width = _tip.width + 24;
        TweenAlpha[] tpos = gameObject.GetComponentsInChildren<TweenAlpha>();
        for (int i = 0; i < tpos.Length; i++){
            //tpos[i].Reset();
            tpos[i].ResetToBeginning();
            tpos[i].PlayForward();
        }
        try
        {
            if (str == null || str.Length < 3)
            {
                throw new System.Exception();
            }
        }
        catch(System.Exception e)
        {
            TyCrashReport.reportException(e, RG_Utils.getLanguageString("CodeItem142"));
        }
	}
	
	public void hideAll(){
        TweenAlpha[] tpos = gameObject.GetComponentsInChildren<TweenAlpha>();
        for (int i = 0; i < tpos.Length; i++)
        {
            tpos[i].ResetToBeginning();
        }
        gameObject.SetActive(false);
	}
    static public AutoHideMsgBox getInstance()
    {
		return _instance;
	}
}
