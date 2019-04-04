using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBringObjectState : State
{
    PlayerManager curPlayer;

    GameObject bringingObject;

    public PlayerBringObjectState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_BASE_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
    }

    public PlayerBringObjectState(ObjectManager curObject, GameObject bringingObject) : base(curObject)
    {
        stateName = "PLAYER_BASE_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
        this.bringingObject = bringingObject;
    }


    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        this.bringingObject.transform.parent = curPlayer.gameObject.transform;
        this.bringingObject.layer = 2;
        this.bringingObject.transform.position = new Vector3(curPlayer.transform.position.x, 1.17f, curPlayer.transform.position.z);
    }

    public override void Execute()
    {
        curPlayer.Move();
        if (curPlayer.GetInputManager().GetInteractInput())
        {
            if (curPlayer.IsObstacle())
                return;
            Vector3 posePosition = curPlayer.GetFrontPosition();
            this.bringingObject.transform.parent = null;
            this.bringingObject.transform.position = posePosition;
            curPlayer.ChangeState(new PlayerBaseState(curPlayer));
        }
    }

    public override void Exit()
    {

    }
}
