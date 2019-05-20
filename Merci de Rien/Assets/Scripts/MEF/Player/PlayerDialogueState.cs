﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueState : State
{
    PlayerManager curPlayer;

    PnjManager pnj;
    Quaternion pnjBaseRotation;

    State prevState;

    DialogueUiManager dialogueUiManager;

    public PlayerDialogueState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_DIALOGUE_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
    }

    public PlayerDialogueState(ObjectManager curObject, GameObject pnj, State prevState) : base(curObject)
    {
        stateName = "PLAYER_DIALOGUE_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
        this.pnj = pnj.GetComponent<PnjManager>();
        this.prevState = prevState;
        dialogueUiManager = DialogueUiManager.Instance;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        Camera.main.GetComponent<CameraManager>().SetDialogueCamera(pnj.gameObject);
        curPlayer.transform.LookAt(pnj.transform.position);
        curPlayer.GetAnimator().SetFloat("MoveSpeed", 0f);
        pnj.ChangeState(new PnjDialogueState(pnj, curPlayer, pnj.GetCurrentState()));
        dialogueUiManager.StartDialogue(pnj.dialogueManager.GetDialogue());
    }

    public override void Execute()
    {
        if (curPlayer.GetInputManager().GetInteractInputDown())
        {
            if (!dialogueUiManager.DisplayNextSentence())
            {
                dialogueUiManager.EndDialogue();
                curPlayer.ChangeState(prevState);
            }
        }
    }

    public override void Exit()
    {
        Camera.main.GetComponent<CameraManager>().SetNewCamera(CameraManager.CameraType.Base);
        PnjDialogueState pnjCurrentState = (PnjDialogueState)pnj.GetComponent<PnjManager>().GetCurrentState();
        pnjCurrentState.EndDialogue();
    }
}