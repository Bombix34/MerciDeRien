using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseToolState : State
{
    PlayerManager curPlayer;

    InteractObject bringingObject;

    //temps de pression du bouton
    float interactInputLenght = 0;

    //pour empêcher le prendre/déposer dans la même frame
    float tempoTime = 0.3f;


    bool endState = false;
    float chronoEnd = 0.3f;

    public PlayerUseToolState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_USE_TOOL_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
    }

    public PlayerUseToolState(ObjectManager curObject, GameObject bringingObject) : base(curObject)
    {
        stateName = "PLAYER_USE_TOOL_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
        this.bringingObject = bringingObject.GetComponent<InteractObject>();
    }

    public void TryPoseObject()
    {
        Vector3 posePosition = Vector3.zero;
        bringingObject.EndInteraction();
        this.bringingObject.transform.parent = null;
        bringingObject.EndInteraction();
        endState = true;
        Debug.Log(bringingObject.GetComponent<BringObject>().IsLaunch);
        curPlayer.ResetVelocity();
    }

    public void ShootObject()
    {
        this.bringingObject.transform.parent = null;
        Rigidbody body = this.bringingObject.GetComponent<Rigidbody>();
        bringingObject.EndInteraction();

        Vector3 launchDirection = curPlayer.GetHeadingDirection();
        launchDirection.Normalize();
        body.AddForce(launchDirection * 400f, ForceMode.Impulse);
        this.bringingObject.GetComponent<BringObject>().LaunchObject();
        endState = true;
        curPlayer.ResetVelocity();
    }

    public void InteractInput()
    {
        if (curPlayer.GetInputManager().GetInteractInput())
        {
            interactInputLenght += Time.deltaTime;
        }

        if (curPlayer.GetInputManager().GetInteractInputUp())
        {
            curPlayer.ResetVelocity();
            if (interactInputLenght > 0.3f)
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

    public BringObject GetBringingObject()
    {
        if (bringingObject.GetComponent<BringObject>() == null)
            return null;
        return bringingObject.GetComponent<BringObject>();
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        this.bringingObject.transform.parent = curPlayer.handTool;
        this.bringingObject.StartInteraction();
        this.bringingObject.transform.position = bringingObject.transform.parent.position;
        this.bringingObject.transform.rotation = bringingObject.transform.parent.rotation;
        tempoTime = 0.3f;
        chronoEnd = 0.3f;
    }

    public override void Execute()
    {
        if (!endState)
        {
            curPlayer.Move();
            // this.bringingObject.transform.position = new Vector3(curPlayer.transform.position.x, curPlayer.transform.position.y + 1.7f, curPlayer.transform.position.z);

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
            curPlayer.ResetVelocity();
            if (chronoEnd <= 0)
                curPlayer.ChangeState(new PlayerBaseState(curPlayer));
        }
    }

    public override void Exit()
    {
    }
}