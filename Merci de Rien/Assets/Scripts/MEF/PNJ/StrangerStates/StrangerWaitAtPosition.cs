using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerWaitAtPosition : State
{
    StrangerManager manager;
    PlayerManager player;
    Material curMat;
    float curTransparency = 0.55f;
    float curWidth = 0.05f;

    Vector3 targetPos;

    public StrangerWaitAtPosition(ObjectManager curObject) : base(curObject)
    {
        stateName = "STRANGER_WAIT_STATE";
        this.curObject = curObject;
        manager = (StrangerManager)curObject;
    }

    public StrangerWaitAtPosition(ObjectManager curObject, Vector3 pos) : base(curObject)
    {
        stateName = "STRANGER_WAIT_STATE";
        this.curObject = curObject;
        manager = (StrangerManager)curObject;
        targetPos = pos;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        manager.transform.position = targetPos;
        manager.GetAgent().SetDestination(manager.transform.position);
        curMat = manager.GetRenderer().material = manager.GetMaterial(false);

        manager.Enable(true);
        curMat.SetFloat("_DissolveAmount", curTransparency);
    }

    public override void Execute()
    {
        if (curTransparency <= -0.4f)
        {
            //FIN APPARITION
            Collider[] hitColliders = Physics.OverlapSphere(targetPos, 6f);
            foreach(var item in hitColliders)
            {
                if(item.gameObject.tag=="Player")
                {
                    manager.ChangeState(new StrangerLeaveState(manager));
                }
            }
        }
        else if (curTransparency <= 0)
        {
            //APPARITION DES CHEVEUX
            curTransparency -= (Time.deltaTime * 0.3f);
            curMat = manager.GetRenderer().material = manager.GetMaterial(true);
            manager.GetAgent().enabled = true;
            manager.ActiveHairParticle(true);
        }
        else
        {
            if (curTransparency <= 0.2)
                manager.ActiveParticle(true);
            else if (curTransparency <= 0.06f)
            {
                if (curWidth > 0.001)
                    curWidth -= (Time.deltaTime * 0.5f);
                curMat.SetFloat("_DissolveWidth", curWidth);
            }
            curTransparency -= (Time.deltaTime * 0.3f);
            curMat.SetFloat("_DissolveAmount", curTransparency);
        }
    }

    public override void Exit()
    {
    }
}
