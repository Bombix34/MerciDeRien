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

    public void ChangeState(GameObject interactedObject)
    {
        //ATTENTION -> interactedObject ne doit pas être null
        switch(interactedObject.tag)
        {
            case "BringObject":
                curPlayer.ChangeState(new PlayerBringObjectState(curPlayer, curPlayer.GetNearInteractObject()));
                break;
            case "PNJ":
                curPlayer.ChangeState(new PlayerDialogueState(curPlayer, curPlayer.GetNearInteractObject(),curPlayer.GetCurrentState()));
                break;
        }
        interactedObject.GetComponent<InteractObject>().UpdateFeedback(false);
        curPlayer.SetNearInteractObject(null);
    }

//STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
    }

    public override void Execute()
    {
        curPlayer.Move();
        curPlayer.RaycastObject();
        if (curPlayer.GetInputManager().GetInteractInputDown())
        {
            if (curPlayer.GetNearInteractObject() != null)
            {
                ChangeState(curPlayer.GetNearInteractObject());
            }
            else
            {
               // curPlayer.GetAnimator().SetTrigger("Wave");
            }
        }

        //Faudra que je te pose une question à ce sujet Hugo o/ 
        //-Abdoul
        //AkSoundEngine.PostEvent("UI_item_highlight_play", ??)
    }

    public override void Exit()
    {
    }
}
