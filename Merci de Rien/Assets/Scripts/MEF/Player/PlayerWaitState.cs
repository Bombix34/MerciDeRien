using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitState : State
{

    State prevState;
    PlayerManager manager;
    public PlayerWaitState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_WAIT_STATE";
        this.curObject = curObject;
    }

    public PlayerWaitState(ObjectManager curObject, State prevState) : base(curObject)
    {
        stateName = "PLAYER_WAIT_STATE";
        this.curObject = curObject;
        this.prevState = prevState;
        manager = (PlayerManager)curObject;
    }


    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        manager.Move(false); 
        manager.GetAnimator().SetFloat("MoveSpeed", 0f);
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
