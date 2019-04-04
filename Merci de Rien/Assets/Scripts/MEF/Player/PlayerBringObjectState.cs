using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBringObjectState : State
{
    PlayerManager curPlayer;

    GameObject bringingObject;
    GameObject blockingObject;

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

    public void TryPoseObject()
    {
        Vector3 posePosition = Vector3.zero;
        blockingObject = curPlayer.IsObstacle(curPlayer.GetFrontPosition());
        if (blockingObject != null)
        {
            return;
        }
        posePosition = curPlayer.GetFrontPosition();
        this.bringingObject.transform.parent = null;
        this.bringingObject.transform.position = posePosition;
        curPlayer.ChangeState(new PlayerBaseState(curPlayer));
    }

    public Vector3 GetValidPosition(Vector3 blockingObj)
    {
        Vector3 result = Vector3.zero;
        float radius = Mathf.Sqrt(Mathf.Pow(blockingObj.x - curPlayer.transform.position.x, 2) 
            + Mathf.Pow(blockingObj.z - curPlayer.transform.position.z,2));
        float newX = (curPlayer.transform.position.x + radius * Mathf.Cos(35));
        float newZ = (curPlayer.transform.position.z + radius * Mathf.Sin(35));
        result = new Vector3(newX, curPlayer.transform.position.y, newZ);
        blockingObject = curPlayer.IsObstacle(result);
        if (blockingObject != null)
            result = Vector3.zero;
        return result;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        this.bringingObject.transform.parent = curPlayer.gameObject.transform;
        //  this.bringingObject.layer = 2;
        this.bringingObject.transform.position = new Vector3(curPlayer.transform.position.x, 1.17f, curPlayer.transform.position.z);
    }

    public override void Execute()
    {
        curPlayer.Move();
        if (curPlayer.GetInputManager().GetInteractInput())
            TryPoseObject();
    }

    public override void Exit()
    {

    }





    /* WIP FUNCTION
    public void TryPoseObject()
    {
        Vector3 posePosition = Vector3.zero;
        blockingObject = curPlayer.IsObstacle(curPlayer.GetFrontPosition());
        if (blockingObject != null)
        {
            while (posePosition == Vector3.zero)
            {
                posePosition = GetValidPosition(blockingObject.transform.position);
            }
        }
        else
        {
            posePosition = curPlayer.GetFrontPosition();
        }
        this.bringingObject.transform.parent = null;
        this.bringingObject.transform.position = posePosition;
        curPlayer.ChangeState(new PlayerBaseState(curPlayer));
    }
    */
}
