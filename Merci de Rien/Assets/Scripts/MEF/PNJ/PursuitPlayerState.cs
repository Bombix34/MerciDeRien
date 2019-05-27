using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuitPlayerState : State
{
    private PnjManager curPnj;
    private NavMeshAgent agent;
    PlayerManager curPlayer;

    float baseSpeed;

    State prevState;
    
    public PursuitPlayerState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PURSUIT_PLAYER_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
        curPlayer = PlayerManager.instance;
        agent = curPnj.GetAgent();
        baseSpeed = agent.speed;
    }

    public PursuitPlayerState(ObjectManager curObject, State prevState) : base(curObject)
    {
        stateName = "PURSUIT_PLAYER_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
        curPlayer = PlayerManager.instance;
        agent = curPnj.GetAgent();
        baseSpeed = agent.speed;
        this.prevState = prevState;
    }

    public void LaunchDialogueWithPlayer()
    {
        if (curPlayer.GetCurrentState().stateName == "PLAYER_DIALOGUE_STATE")
            return;
        curPlayer.ChangeState(new PlayerDialogueState(curPlayer,curPnj.gameObject,curPlayer.GetCurrentState()));
        curPnj.ChangeState(new PnjDialogueState(curPnj,curPlayer,this.prevState));
    }
    
    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        agent.speed *= 2f;
        curPnj.GetInteractionManager().CanInteract = false;
    }

    public override void Execute()
    {
        agent.SetDestination(curPlayer.transform.position);
        if(curPnj.RaycastPlayer())
        {
            if (curPlayer.IsOccuped() )
            {
                agent.SetDestination(curPnj.transform.position);
                return;
            }
            else
            {
                agent.SetDestination(curPlayer.transform.position);
            }
            LaunchDialogueWithPlayer();
        }
    }

    public override void Exit()
    {
        agent.speed = baseSpeed;
        curPnj.GetInteractionManager().CanInteract = true;
    }
}
