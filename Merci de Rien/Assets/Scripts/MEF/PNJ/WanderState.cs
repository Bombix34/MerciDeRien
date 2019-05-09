using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : State
{
    private PnjManager curPnj;

    private float wanderRadius;
    private float wanderTimer;
    private float timer;

    public WanderState(ObjectManager curObject) : base(curObject)
    {
        stateName = "WANDER_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
        wanderTimer = Random.Range(2f,5f);
        wanderRadius = Random.Range(2f,3f);
        timer = wanderTimer;
    }

    //STATE GESTION______________________________________________________________________________

    public override void Enter()
    {
    }

    public override void Execute()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            SetWanderDestination();
        }
    }

    public override void Exit()
    {
    }

    //WANDER______________________________________________________________________________

    public void SetWanderDestination()
    {
        Vector3 newPos = RandomNavSphere(curObject.transform.position, wanderRadius, -1);
        curPnj.GetAgent().SetDestination(newPos);
        timer = 0;
        wanderTimer = Random.Range(2f, 5f);
        wanderRadius = Random.Range(2f, 3f);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}