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
    float tempoTime = 0.3f;


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
       // GameObject blockingObject = curPlayer.IsObstacle(curPlayer.GetFrontPosition());
        Vector3 posePosition = curPlayer.GetFrontPosition();
        posePosition = new Vector3(posePosition.x, posePosition.y + 0.6f, posePosition.z);
        this.bringingObject.transform.parent = null;
        this.bringingObject.GetComponent<Rigidbody>().useGravity = true;
        this.bringingObject.transform.position = posePosition;
        this.bringingObject.GetComponent<BringObject>().ResetMass();
        endState = true;
        curPlayer.ResetVelocity();
        curPlayer.GetAnimator().SetBool("Carrying", false);

        //SFX
        AkSoundEngine.PostEvent("ENV_pot_put_down_play", this.bringingObject);
    }

    public void ShootObject()
    {
        this.bringingObject.transform.parent = null;
        Rigidbody body=this.bringingObject.GetComponent<Rigidbody>();
        body.useGravity = true;
        body.mass = 80f;
        
        Vector3 launchDirection = curPlayer.GetHeadingDirection();
        launchDirection.Normalize();
        body.AddForce(launchDirection*400f,ForceMode.Impulse);
        this.bringingObject.GetComponent<BringObject>().LaunchObject();
        endState = true;
        curPlayer.ResetVelocity();
        curPlayer.GetAnimator().SetBool("Carrying", false);

        //SFX
        AkSoundEngine.PostEvent("MC_throw_play", this.bringingObject);
    }

    public void InteractInput()
    {
        if(curPlayer.GetInputManager().GetInteractInput())
        {
            interactInputLenght += Time.deltaTime;
        }

        if (curPlayer.GetInputManager().GetInteractInputUp())
        {
            curPlayer.ResetVelocity();
            if (interactInputLenght>0.3f)
            {
                curPlayer.GetAnimator().SetTrigger("Throw");
            }
            else
            {
                TryPoseObject();
            }
            interactInputLenght = 0;
        }
        
    }

    public BringObject GetBringingObject()
    {
        if (bringingObject.GetComponent<BringObject>() == null)
            return null;
        return bringingObject.GetComponent<BringObject>();
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        this.bringingObject.transform.parent = curPlayer.GetBringPosition();
        this.bringingObject.GetComponent<BringObject>().IsLaunch = false;
        curPlayer.GetAnimator().SetBool("Carrying", true);
        this.bringingObject.GetComponent<Rigidbody>().useGravity = false;
        this.bringingObject.GetComponent<Rigidbody>().mass = 1;
       //this.bringingObject.transform.position = curPlayer.GetBringPosition().position;
        // this.bringingObject.transform.rotation.setlo
        this.bringingObject.transform.LookAt(bringingObject.transform.position, Vector3.up);
        tempoTime = 0.3f;
        chronoEnd = 0.3f;
        //SFX
        AkSoundEngine.PostEvent("MC_pick_big_item_play", this.bringingObject);
    }

    public override void Execute()
    {
        if (!endState)
        {
            curPlayer.Move(true);
            this.bringingObject.transform.position = curPlayer.GetBringPosition().position;
            if (tempoTime > 0)
            {
                tempoTime -= Time.deltaTime;
                return;
            }
            InteractInput();
        }


        else
        {
            chronoEnd -= Time.deltaTime;
            curPlayer.Move(false);
            curPlayer.ResetVelocity();
            if (chronoEnd <= 0)
                curPlayer.ChangeState(new PlayerBaseState(curPlayer));
        }
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
