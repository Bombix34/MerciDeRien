using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBringObjectState : State
{
    PlayerManager curPlayer;

    GameObject bringingObject;

    //temps de pression du bouton
    float interactInputLenght=0;

    //pour empêcher le prendre/déposer dans la même frame
    float tempoTime = 0.15f;


    bool endState = false;
    float chronoEnd = 0.3f;

    public PlayerBringObjectState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_BRING_OBJECT_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
    }

    public PlayerBringObjectState(ObjectManager curObject, GameObject bringingObject) : base(curObject)
    {
        stateName = "PLAYER_BRING_OBJECT_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
        this.bringingObject = bringingObject;
    }

    public void TryPoseObject()
    {
        Vector3 posePosition = Vector3.zero;
       // GameObject blockingObject = curPlayer.IsObstacle(curPlayer.GetFrontPosition());
        posePosition = curPlayer.GetFrontPosition();
        this.bringingObject.transform.parent = null;
        this.bringingObject.GetComponent<Rigidbody>().useGravity = true;
        this.bringingObject.transform.position = posePosition;
        this.bringingObject.GetComponent<BringObject>().ResetMass();
        curPlayer.ChangeState(new PlayerBaseState(curPlayer));
    }

    public void ShootObject()
    {
        this.bringingObject.transform.parent = null;
        Rigidbody body=this.bringingObject.GetComponent<Rigidbody>();
        body.useGravity = true;
        body.mass = 50f;
        Vector3 launchDirection = curPlayer.GetHeadingDirection();
        launchDirection.Normalize();
        body.AddForce(launchDirection*200f,ForceMode.Impulse);
        this.bringingObject.GetComponent<BringObject>().LaunchObject();
        endState = true;
      //  curPlayer.ChangeState(new PlayerBaseState(curPlayer));
    }

    public void InteractInput()
    {
        if(curPlayer.GetInputManager().GetInteractInput())
        {
            interactInputLenght += Time.deltaTime;
        }

        if (curPlayer.GetInputManager().GetInteractInputUp())
        {
            if(interactInputLenght>0.3f)
            {
                ShootObject();
            }
            else
            {
                TryPoseObject();
            }
            interactInputLenght = 0;
        }
        
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        this.bringingObject.transform.parent = curPlayer.gameObject.transform;
        this.bringingObject.GetComponent<Rigidbody>().useGravity = false;
        this.bringingObject.GetComponent<Rigidbody>().mass = 1;
        this.bringingObject.transform.position = new Vector3(curPlayer.transform.position.x, 1.17f, curPlayer.transform.position.z);
    }

    public override void Execute()
    {
        if(!endState)
            curPlayer.Move();

        if (tempoTime > 0)
        {
            tempoTime -= Time.deltaTime;
            return;
        }
        InteractInput();


        if (endState)
        {
            chronoEnd -= Time.deltaTime;
            if (chronoEnd <= 0)
                curPlayer.ChangeState(new PlayerBaseState(curPlayer));
        }

        /* if (curPlayer.GetInputManager().GetInteractInput())
          {
             TryPoseObject();
          }*/
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
    */
}
