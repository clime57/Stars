using UnityEngine;
using System.Collections;

public class RGLoginLoginState : GameState
{
    public override string update(float d)
    {
        return "";
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

