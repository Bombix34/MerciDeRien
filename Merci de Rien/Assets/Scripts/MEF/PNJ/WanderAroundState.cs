using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAroundState : State
{
    private PnjManager curPnj;

    private float wanderRadius;
    private float wanderTimer;
    private float timer;

    private Vector3 target;
    private float maxDistanceFromTarget;

    public WanderAroundState(ObjectManager curObject) : base(curObject)
    {
        stateName = "WANDER_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
        wanderTimer = Random.Range(2f, 5f);
        wanderRadius = Random.Range(2f, 3f);
        timer = wanderTimer;
    }

    public WanderAroundState(ObjectManager curObject, Vector3 target) : base(curObject)
    {
        stateName = "WANDER_STATE";
        this.curObject = curObject;
        curPnj = (PnjManager)this.curObject;
        wanderTimer = Random.Range(2f, 5f);
        wanderRadius = Random.Range(2f, 3f);
        timer = wanderTimer;
        this.target = target;
        maxDistanceFromTarget = wanderRadius * 1.5f;
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
        Vector3 newPos = Vector3.zero;
        if (!IsFarFromTarget())
        {
            newPos = RandomNavSphere(curObject.transform.position, wanderRadius, -1);
        }
        else
        {
            Debug.Log("je me rapproche");
            newPos = GetPositionNearTarget(curObject.transform.position, target, wanderRadius);
        }
        curPnj.GetAgent().SetDestination(newPos);
        timer = 0;
        wanderTimer = Random.Range(2f, 5f);
        wanderRadius = Random.Range(2f, 3f);
    }

    public bool IsFarFromTarget()
    {
        float distFromTarget = Mathf.Sqrt(Mathf.Pow(target.x - curPnj.transform.position.x, 2) +
            Mathf.Pow(target.y - curPnj.transform.position.y, 2) +
            Mathf.Pow(target.z - curPnj.transform.position.z, 2));
        return distFromTarget > maxDistanceFromTarget;
    }

    public static Vector3 GetPositionNearTarget(Vector3 origin, Vector3 target, float radius)
    {
        Vector3 newPos = new Vector3(target.x - origin.x,
                                    target.x - origin.y,
                                    target.x - origin.x);
        newPos.Normalize();
        newPos *= radius;
        return newPos;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}