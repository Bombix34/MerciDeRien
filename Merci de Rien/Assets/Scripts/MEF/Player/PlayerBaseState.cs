using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    PlayerManager curPlayer;

    public PlayerBaseState(ObjectManager curObject):base(curObject)
    {
        stateName = "PLAYER_BASE_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
    }

//STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
    }

    public override void Execute()
    {
        curPlayer.Move();
        curPlayer.RaycastObject();
        if (curPlayer.GetInputManager().GetInteractInput())
        {
            if (curPlayer.GetNearInteractObject() != null)
            {
                curPlayer.ChangeState(new PlayerBringObjectState(curPlayer, curPlayer.GetNearInteractObject()));
                curPlayer.GetNearInteractObject().GetComponent<InteractObject>().UpdateFeedback(false);
                curPlayer.SetNearInteractObject(null);
            }
            else
            {
                curPlayer.GetAnimator().SetTrigger("Wave");
            }
        }
    }

    public override void Exit()
    {
    }
}
