using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueState : PlayerTransitionState
{

    PnjManager pnj;
    Quaternion pnjBaseRotation;
    DialogueUiManager dialogueUiManager;

    public PlayerDialogueState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_DIALOGUE_STATE";
        this.curObject = curObject;
        manager = (PlayerManager)this.curObject;
    }

    public PlayerDialogueState(ObjectManager curObject, GameObject pnj, State prevState) : base(curObject)
    {
        stateName = "PLAYER_DIALOGUE_STATE";
        this.curObject = curObject;
        manager= (PlayerManager)this.curObject;
        this.pnj = pnj.GetComponent<PnjManager>();
        this.prevState = prevState;
        dialogueUiManager = DialogueUiManager.Instance;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        Camera.main.GetComponent<CameraManager>().SetDialogueCamera(pnj.gameObject);
        manager.Move(false);
        manager.ResetNearInteractObject();
        manager.transform.LookAt(pnj.transform.position);
        manager.GetAnimator().SetFloat("MoveSpeed", 0f);
        Dialogue curDialogue = pnj.dialogueManager.GetDialogue();
        pnj.PlayOnomatope();
        GameManager.Instance.AddToHistoric(curDialogue);
        if (pnj.GetCurrentState().stateName != "PNJ_DIALOGUE_STATE")
            pnj.ChangeState(new PnjDialogueState(pnj, manager, pnj.GetCurrentState()));
        dialogueUiManager.StartDialogue(curDialogue);
        InitBringObject();
    }

    public override void Execute()
    {
        UpdateBringingObjectPosition();
       /* if (manager.GetInputManager().GetInteractInputDown())
        {
            if (!dialogueUiManager.DisplayNextSentence())
            {
                dialogueUiManager.EndDialogue();
                manager.ChangeState(prevState);
            }
            else
            {

            }
        }*/
    }

    public override void Exit()
    {
        Camera.main.GetComponent<CameraManager>().SetNewCamera(CameraManager.CameraType.Base);
        PnjDialogueState pnjCurrentState = (PnjDialogueState)pnj.GetCurrentState();
        pnjCurrentState.EndDialogue();
    }
}