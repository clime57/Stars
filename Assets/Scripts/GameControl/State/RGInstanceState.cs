using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RGInstanceState : GameState
{
    

    public override void start()
	{

    }

	public override string update(float time)
	{

		return "";
	}

	public override void over()
	{

	}

	public override string postEvent(FSMEvent_C evt)
	{
		string msg = evt.msg;
		switch (evt.msg)
		{

		}
		return "";
	}
    
}
