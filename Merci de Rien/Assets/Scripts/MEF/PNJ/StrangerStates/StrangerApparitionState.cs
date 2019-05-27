using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerApparitionState : State
{
    StrangerManager manager;
    PlayerManager player;
    State playerPrevState;
    Material curMat;
    float curTransparency = 0.55f;
    float curWidth = 0.05f;

    public StrangerApparitionState(ObjectManager curObject) : base(curObject)
    {
        stateName = "STRANGER_APPARITION_STATE";
        this.curObject = curObject;
        manager = (StrangerManager)curObject;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        player = EventManager.Instance.GetPlayer().GetComponent<PlayerManager>();
        manager.transform.position = player.GetStrangerPosition();
        Camera.main.GetComponent<CameraManager>().SetDialogueCamera(manager.gameObject);
        player.transform.LookAt(manager.transform.position);
        manager.transform.LookAt(player.transform.position);
        playerPrevState = player.GetCurrentState();
        player.ChangeState(new PlayerWaitState(player,player.GetCurrentState()));
        manager.GetAgent().SetDestination(manager.transform.position);
        curMat =manager.GetRenderer().material = manager.GetMaterial(false);

        manager.Enable(true);
        curMat.SetFloat("_DissolveAmount",curTransparency);
    }

    public override void Execute()
    {
        if(curTransparency<=-0.4f)
        {
            //FIN APPARITION
            manager.ChangeState(new PnjDialogueState(manager, player, new StrangerLeaveState(manager)));
            player.ChangeState(new PlayerDialogueState(player, manager.gameObject, playerPrevState));
        }
        else if(curTransparency<=0)
        {
            //APPARITION DES CHEVEUX
            curTransparency -= (Time.deltaTime * 0.3f);
            curMat = manager.GetRenderer().material = manager.GetMaterial(true);
            manager.GetAgent().enabled = true;
            manager.ActiveHairParticle(true);
        }
        else
        {
            if(curTransparency<=0.2)
                manager.ActiveParticle(true);
            else if (curTransparency <= 0.06f)
            {
                if (curWidth > 0.001)
                    curWidth -= (Time.deltaTime*0.5f);
                curMat.SetFloat("_DissolveWidth", curWidth);
            }
            curTransparency -= (Time.deltaTime*0.3f);
            curMat.SetFloat("_DissolveAmount", curTransparency);
        }
    }

    public override void Exit()
    {
    }
}
