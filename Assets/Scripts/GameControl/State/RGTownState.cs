using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RGTownState : GameState
{
    string _stateToGo = "";
    public override string update(float d)
    {
        return _stateToGo;
    }
    public override void start()
    {
 
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

