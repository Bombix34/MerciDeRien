using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerApparitionState : State
{
    StrangerManager manager;
    PlayerManager player;
    State playerPrevState;
    Material curMat;
    float curTransparency = 1f;

    public StrangerApparitionState(ObjectManager curObject) : base(curObject)
    {
        stateName = "STRANGER_APPARITION_STATE";
        this.curObject = curObject;
        manager = (StrangerManager)curObject;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        Camera.main.GetComponent<CameraManager>().SetDialogueCamera(manager.gameObject);
        player = EventManager.Instance.GetPlayer().GetComponent<PlayerManager>();
        player.transform.LookAt(manager.transform.position);
        manager.transform.LookAt(player.transform.position);
        playerPrevState = player.GetCurrentState();
        player.ChangeState(new PlayerWaitState(player,player.GetCurrentState()));
        Vector3 playerPos = player.GetFrontPosition()*1.1f;
        manager.transform.position = playerPos;
        manager.GetAgent().enabled = false;
        curMat=manager.GetRenderer().material = manager.GetMaterial(false);
        curMat.SetFloat("_DissolveAmount", 1f);
        manager.Enable(true);
    }

    public override void Execute()
    {
        if(curTransparency<=0)
        {
            curTransparency = 0;
            curMat = manager.GetRenderer().material = manager.GetMaterial(true);
            manager.GetAgent().enabled = true;
            manager.ChangeState(new PnjDialogueState(manager, player, new StrangerLeaveState(manager)));
            player.ChangeState(new PlayerDialogueState(player,manager.gameObject,playerPrevState));
        }
        else
        {
            curTransparency -= (Time.deltaTime*0.9f);
            curMat.SetFloat("_DissolveAmount", curTransparency);
        }
    }

    public override void Exit()
    {
    }
}
