using UnityEngine;
using System.Collections;

public class DemoMainState : GameState
{
	public override string update(float d)
	{
		return "";
	}
	public override void start()
	{
		Debug.Log ("DemoMainState start");
	}
	public override  void over()
	{
		Debug.Log ("DemoMainState over");
	}
	
	public override string postEvent(FSMEvent_C evt)
	{
		return "";
	}
}

