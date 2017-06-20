using UnityEngine;
using System.Collections;

public class RGGameLogic : GameLogic
{
    override public void init()
    {
        this.fsm_ = new FSM_C();

        GameState state = new RGGameState();
        this.fsm_.addState("Game", state, "");

        state = new RGLoginState();
        this.fsm_.addState("Login", state, "Game");
        
        state = new RGTownState();
        this.fsm_.addState("GameMain_Town", state, "GameMain");

        state = new RGInstanceState();
        this.fsm_.addState("GameMain_Instance", state, "GameMain");
        
        state = new RGStartState();
        this.fsm_.addState("Start", state, "Game");

        this.fsm_.init("Start");
    }

    static public void backToLogin()
    {
        GameLogic gl = Game.getInstance().findObject<GameLogic>();
        gl.postEvt(new FSMEvent_C("Login_Login"));
    }
}
