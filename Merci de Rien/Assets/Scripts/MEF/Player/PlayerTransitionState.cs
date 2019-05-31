using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransitionState : State
{

    protected State prevState;
    protected PlayerManager manager;

    public PlayerTransitionState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_WAIT_STATE";
        this.curObject = curObject;
    }

    public PlayerTransitionState(ObjectManager curObject, State prevState) : base(curObject)
    {
        stateName = "PLAYER_WAIT_STATE";
        this.curObject = curObject;
        this.prevState = prevState;
        manager = (PlayerManager)curObject;
    }

    public virtual void ReturnBackToPrevState()
    {
        if(prevState!=null)
            manager.ChangeState(prevState);
    }


    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
