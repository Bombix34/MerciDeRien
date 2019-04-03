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
    }

    public override void Exit()
    {
    }
}
