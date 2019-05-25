using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : State
{
    PlayerManager curPlayer;

    InteractObject interactObject;

    State prevState;

    public PlayerInteractState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_INTERACT_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
    }

    public PlayerInteractState(ObjectManager curObject, GameObject curObj, State playerCurState) : base(curObject)
    {
        stateName = "PLAYER_INTERACT_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
        this.interactObject = curObj.GetComponent<InteractObject>();
        prevState = playerCurState;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        Camera.main.GetComponent<CameraManager>().SetDialogueCamera(interactObject.gameObject);
        curPlayer.Move(false);
        curPlayer.transform.LookAt(interactObject.transform.position);
        curPlayer.GetAnimator().SetFloat("MoveSpeed", 0f);
        interactObject.StartInteraction();
    }

    public override void Execute()
    {
        if (curPlayer.GetInputManager().GetInteractInputDown())
        {
            curPlayer.ChangeState(prevState);
        }
    }

    public override void Exit()
    {
        Camera.main.GetComponent<CameraManager>().SetNewCamera(CameraManager.CameraType.Base);
        interactObject.EndInteraction();
    }
}