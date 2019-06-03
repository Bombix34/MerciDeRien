using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransitionState : State
{

    protected State prevState;
    protected PlayerManager manager;

    public PlayerTransitionState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_TRANSITION_STATE";
        this.curObject = curObject;
    }

    public PlayerTransitionState(ObjectManager curObject, State prevState) : base(curObject)
    {
        stateName = "PLAYER_TRANSITION_STATE";
        this.curObject = curObject;
        this.prevState = prevState;
        manager = (PlayerManager)curObject;
    }

    public virtual void ReturnBackToPrevState()
    {
        if (prevState != null)
        {
            if (manager.IsBringingObject() != null)
                prevState = new PlayerBringObjectState(manager, manager.IsBringingObject().gameObject);
            else if (manager.IsBringingTool() != null)
                prevState = new PlayerUseToolState(manager, manager.IsBringingTool().gameObject);
            else
                prevState = new PlayerBaseState(manager);
            manager.ChangeState(prevState);
        }
    }

    public void UpdateBringingObjectPosition()
    {
        if (manager.IsBringingObject() != null)
        {
            manager.IsBringingObject().transform.position = manager.GetBringPosition().position;
        }
        else if (manager.IsBringingTool() != null)
        {
            manager.IsBringingTool().transform.position = manager.IsBringingTool().transform.parent.position;
        }
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
