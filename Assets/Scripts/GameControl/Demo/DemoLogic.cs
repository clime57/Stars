using UnityEngine;
using System.Collections;
using Stars;
public class DemoLogic
{
    FSM_C fsm_;

    public void init()
    {
        this.fsm_ = new FSM_C();

        GameState state = new DemoMainState();
        this.fsm_.addState("Game", state, "");

        this.fsm_.init("Game");
    }

    public void update()
    {
        this.fsm_.update(Time.deltaTime);
    }

    public void postEvt(FSMEvent_C evt)
    {
        this.fsm_.postEvent(evt);
    }
}
