using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjDialogueState : State
{
    private PnjManager curPnj;
    PlayerManager curPlayer;

    State prevState;

    Quaternion baseRotation;


    public PnjDialogueState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PNJ_DIALOGUE_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
        baseRotation = curPnj.transform.rotation;
    }

    public PnjDialogueState(ObjectManager curObject, PlayerManager player, State prevState) : base(curObject)
    {
        stateName = "PNJ_DIALOGUE_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
        baseRotation = curPnj.transform.rotation;
        curPlayer = player;
        this.prevState = prevState;
    }

    public void EndDialogue()
    {
        curPnj.ChangeState(prevState);
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        curPnj.GetAgent().SetDestination(curPnj.transform.position);
        curPnj.transform.LookAt(curPlayer.gameObject.transform);
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        curPnj.transform.rotation = baseRotation;
    }
}
