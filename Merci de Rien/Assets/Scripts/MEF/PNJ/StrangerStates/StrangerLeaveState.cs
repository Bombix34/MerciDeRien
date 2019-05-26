﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerLeaveState : State
{
    StrangerManager manager;
    PlayerManager player;
   //State playerPrevState;
    Material curMat;
    float curTransparency = 0f;

    public StrangerLeaveState(ObjectManager curObject) : base(curObject)
    {
        stateName = "STRANGER_LEAVE_STATE";
        this.curObject = curObject;
        manager = (StrangerManager)curObject;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
        curMat = manager.GetMaterial(false);
        manager.GetRenderer().material = curMat;
        curMat.SetFloat("_DissolveAmount", 0f);
    }

    public override void Execute()
    {
        if (curTransparency >= 0.8f)
        {
            curTransparency = 1;
            manager.GetRenderer().material= manager.GetMaterial(true);
            manager.Enable(false);
            manager.ChangeState(new StrangerWaitState(manager));
        }
        else
        {
            curTransparency += (Time.deltaTime*0.8f);
            curMat.SetFloat("_DissolveAmount", curTransparency);
        }
    }

    public override void Exit()
    {
    }
}