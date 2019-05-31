using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransitionState : State
{

    protected State prevState;
    protected PlayerManager manager;
    protected GameObject bringObject;

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

    public void UpdateBringingObjectPosition()
    {
        if (bringObject != null)
        {
            if (bringObject.GetComponent<BringObject>() != null)
            {
                this.bringObject.transform.position = manager.GetBringPosition().position;
            }
        }
    }

    public void InitBringObject()
    {
        if (manager.IsBringingTool() != null)
            bringObject = manager.IsBringingTool().gameObject;
        else if (manager.IsBringingObject() != null)
            bringObject = manager.IsBringingObject().gameObject;
        if (bringObject != null)
            bringObject.GetComponent<Rigidbody>().isKinematic = true;
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
