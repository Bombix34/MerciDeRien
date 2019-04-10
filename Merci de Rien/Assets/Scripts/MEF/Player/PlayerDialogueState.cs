using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueState : State
{
    PlayerManager curPlayer;

    GameObject pnj;
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
        this.pnj = pnj;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        pnjBaseRotation = pnj.transform.rotation;
        pnj.transform.LookAt(curPlayer.gameObject.transform);
        Camera.main.GetComponent<CameraManager>().SetDialogueCamera(pnj);
    }

    public override void Execute()
    {
        if (curPlayer.GetInputManager().GetInteractInputDown())
        {
            Camera.main.GetComponent<CameraManager>().SetNewCamera(CameraManager.CameraType.Base);
            pnj.transform.rotation = pnjBaseRotation;
            curPlayer.ChangeState(new PlayerBaseState(curPlayer));
        }
    }

    public override void Exit()
    {
    }
}