using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerWaitState : State
{
    StrangerManager manager;
    public StrangerWaitState(ObjectManager curObject) : base(curObject)
    {
        stateName = "STRANGER_WAIT_STATE";
        this.curObject = curObject;
        manager = (StrangerManager)curObject;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        manager.Enable(false);
        manager.DisableHairParticle();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}
