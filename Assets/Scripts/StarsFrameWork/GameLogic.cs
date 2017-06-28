using UnityEngine;
using System.Collections;
namespace Stars
{
    public class GameLogic : GameSubSystem
    {
        protected FSM_C fsm_;

        override public void init()
        {
            this.fsm_ = new FSM_C();
        }

        override public void update(float time)
        {
            this.fsm_.update(time);
        }

        public void postEvt(FSMEvent_C evt)
        {
            this.fsm_.postEvent(evt);
        }
    }

}