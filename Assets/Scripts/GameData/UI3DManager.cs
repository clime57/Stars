using UnityEngine;
using System.Collections;

public class UI3DManager : MonoBehaviour{
	Hashtable _ctlList;
	static UI3DManager instance_;
	void Awake(){
		_ctlList = new Hashtable();
		instance_ = this;		
	}
	static public UI3DManager getInstance(){
		return instance_;	
	}
	
	public UI3DController getController(string name){
		if(_ctlList.Contains(name)){
			return _ctlList[name] as UI3DController;
		}else{
            GameObject go = Resources.Load("Reserved/Prefab/UI3dController") as GameObject;
			GameObject mygo = GameObject.Instantiate(go) as GameObject;
			UI3DController ctl = mygo.GetComponent<UI3DController>();
			_ctlList.Add(name,ctl);
			mygo.transform.parent = gameObject.transform;
			return ctl;
		}
	}
	
	public void remove(string name){
		if(_ctlList.Contains(name)){
			UI3DController ctl = _ctlList[name] as UI3DController;
			Destroy(ctl.gameObject);
			_ctlList.Remove(name);
		}
	}
}
