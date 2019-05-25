using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseToolState : State
{
    PlayerManager curPlayer;

    ToolObject toolObject;

    //temps de pression du bouton
    float interactInputLenght = 0;

    //pour empêcher le prendre/déposer dans la même frame
    float tempoTime = 0.3f;

    public bool CanMove { get; set; } = true;

    bool endState = false;
    float chronoEnd = 0.3f;

    public PlayerUseToolState(ObjectManager curObject) : base(curObject)
    {
        stateName = "PLAYER_USE_TOOL_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
    }

    public PlayerUseToolState(ObjectManager curObject, GameObject toolObject) : base(curObject)
    {
        stateName = "PLAYER_USE_TOOL_STATE";
        this.curObject = curObject;
        curPlayer = (PlayerManager)this.curObject;
        this.toolObject = toolObject.GetComponent<ToolObject>();
        this.toolObject.curStatePlayer = this;
        this.toolObject.IsUsingObject = false;
    }

    public void TryPoseObject()
    {
        Vector3 posePosition = Vector3.zero;
        toolObject.EndInteraction();
        this.toolObject.transform.parent = null;
        toolObject.EndInteraction();
        endState = true;
        curPlayer.ResetVelocity();
    }

    public void ShootObject()
    {
        this.toolObject.transform.parent = null;
        Rigidbody body = this.toolObject.GetComponent<Rigidbody>();
        toolObject.EndInteraction();

        Vector3 launchDirection = curPlayer.GetHeadingDirection();
        launchDirection.Normalize();
        body.AddForce(launchDirection * 300f, ForceMode.Impulse);
        this.toolObject.GetComponent<BringObject>().LaunchObject();
        endState = true;
        curPlayer.ResetVelocity();
    }

    public void UseTool()
    {
        toolObject.IsUsingObject = true;
    }

    public void EndUseTool()
    {
        toolObject.IsUsingObject = false;
    }

    public void InteractInput()
    {
        if (curPlayer.GetInputManager().GetCancelInput())
        {
            curPlayer.GetAnimator().SetTrigger("UseTool");
            CanMove = false;
            return;
        }
        if (curPlayer.GetInputManager().GetInteractInput())
        {
            interactInputLenght += Time.deltaTime;
        }

        if (curPlayer.GetInputManager().GetInteractInputUp())
        {
            curPlayer.ResetVelocity();
            if (interactInputLenght > 0.3f)
            {
                curPlayer.GetAnimator().SetTrigger("LaunchTool");
            }
            else
            {
                TryPoseObject();
            }
            interactInputLenght = 0;
        }
    }

    public void EndUseObjectOnCollision()
    {
        Animator anim = curPlayer.GetAnimator();
        anim.SetTrigger("ToolTrigger");
    }

    public IEnumerator EndUseObjectOnCollisiTEStn()
    {
        EndUseTool();
        Animator anim = curPlayer.GetAnimator();
        anim.SetTrigger("ToolTrigger");
        yield return new WaitForSeconds(0.5f);
        CanMove = true;

    }

    public ToolObject GettoolObject()
    {
        return toolObject;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        this.toolObject.transform.parent = curPlayer.handTool;
        CanMove = true;
        this.toolObject.StartInteraction();
        this.toolObject.transform.position = toolObject.transform.parent.position;
        this.toolObject.transform.rotation = toolObject.transform.parent.rotation;
        tempoTime = 0.3f;
        chronoEnd = 0.3f;
    }

    public override void Execute()
    {
        if (!CanMove)
        {
            interactInputLenght = 0;
            return;
        }
        if (!endState)
        {
            curPlayer.Move();
            // this.toolObject.transform.position = new Vector3(curPlayer.transform.position.x, curPlayer.transform.position.y + 1.7f, curPlayer.transform.position.z);

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