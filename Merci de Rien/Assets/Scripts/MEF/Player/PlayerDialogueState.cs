using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueState : State
{
    PlayerManager curPlayer;

    PnjManager pnj;
    Quaternion pnjBaseRotation;

    public PlayerDialogueState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_DIALOGUE_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
    }

    public PlayerDialogueState(ObjectManager curObject, GameObject pnj) : base(curObject)
    {
        stateName = "PLAYER_DIALOGUE_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
        this.pnj = pnj.GetComponent<PnjManager>();
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        Camera.main.GetComponent<CameraManager>().SetDialogueCamera(pnj.gameObject);
        pnj.ChangeState(new PnjDialogueState(pnj, curPlayer, pnj.GetCurrentState()));
    }

    public override void Execute()
    {
        if (curPlayer.GetInputManager().GetInteractInputDown())
        {
            Camera.main.GetComponent<CameraManager>().SetNewCamera(CameraManager.CameraType.Base);
            PnjDialogueState pnjCurrentState =(PnjDialogueState)pnj.GetComponent<PnjManager>().GetCurrentState();
            pnjCurrentState.EndDialogue();
            curPlayer.ChangeState(new PlayerBaseState(curPlayer));
        }
    }

    public override void Exit()
    {
    }
}