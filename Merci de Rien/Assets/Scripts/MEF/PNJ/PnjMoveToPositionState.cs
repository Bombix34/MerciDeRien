using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PnjMoveToPositionState : State
{
    private PnjManager curPnj;
    private Vector3 target;
    private NavMeshAgent agent;

    public PnjMoveToPositionState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PNJ_MOVE_TO_PLACE_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
    }

    public PnjMoveToPositionState(ObjectManager curObject, Vector3 target) : base(curObject)
    {
        stateName = "PNJ_MOVE_TO_PLACE_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
        this.target = target;
        agent=curPnj.GetAgent();
    }

    public void EndWalk()
    {
        curPnj.ChangeState(new WanderAroundState(curPnj, target));
    }
    
    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
    }

    public override void Execute()
    {
        agent.Move(target);
    }

    public override void Exit()
    {
        // curPnj.transform.rotation = baseRotation;
    }
}
