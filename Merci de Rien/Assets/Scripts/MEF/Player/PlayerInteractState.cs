using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerTransitionState
{
    InteractObject interactObject;

    public PlayerInteractState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_INTERACT_STATE";
        this.curObject = curObject;
        manager = (PlayerManager)this.curObject;
    }

    public PlayerInteractState(ObjectManager curObject, GameObject curObj, State playerCurState) : base(curObject)
    {
        stateName = "PLAYER_INTERACT_STATE";
        this.curObject = curObject;
        manager = (PlayerManager)this.curObject;
        this.interactObject = curObj.GetComponent<InteractObject>();
        prevState = playerCurState;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        Camera.main.GetComponent<CameraManager>().SetDialogueCamera(interactObject.gameObject);
        manager.Move(false);
        InitBringObject();
        manager.transform.LookAt(interactObject.transform.position);
        manager.GetAnimator().SetFloat("MoveSpeed", 0f);
        interactObject.StartInteraction();
    }

    public override void Execute()
    {
        UpdateBringingObjectPosition();
    }

    public override void Exit()
    {
        Camera.main.GetComponent<CameraManager>().SetNewCamera(CameraManager.CameraType.Base);
        interactObject.EndInteraction();
    }
}