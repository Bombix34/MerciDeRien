using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PnjManager : ObjectManager
{
    NavMeshAgent navAgent;
    Animator anim;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        anim.SetBool("Grounded", true);
        ChangeState(new WanderAroundState(this,this.transform.position));
    }

    private void Update()
    {
        currentState.Execute();
        UpdateAnim();
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    public void UpdateAnim()
    {
        anim.SetFloat("MoveSpeed", (navAgent.velocity.magnitude / 0.1f)*0.03f);
    }

    private void OnDrawGizmos()
    {
        //TOOL DEBUG
        if (!Application.isPlaying)
            return;
        if (currentState.stateName == "WANDER_AROUND_STATE")
        {
            Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
            WanderAroundState curtState = (WanderAroundState)currentState;
            Gizmos.DrawSphere(curtState.InitPosition, curtState.Radius);
        }

    }

    //RAYCAST________________________________________________________

    public bool RaycastPlayer()
    {
        bool result = false;
        GameObject raycastObject = null;
        Vector3 testPosition = GetFrontPosition();

        Collider[] hitColliders = Physics.OverlapSphere(testPosition, 0.3f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Player")
            {
                result = true;
                i = hitColliders.Length;
            }
            i++;
        }
        return result;
    }

    public Vector3 GetFrontPosition()
    {
        //FONCTION POUR OBTENIR LA POSITION DEVANT LE PERSONNAGE
        //POSITION OU INTERAGIR ET POSER LES OBJETS
        Vector3 forwardPos = transform.TransformDirection(Vector3.forward) * 0.5f;
        Vector3 testPosition = new Vector3(transform.position.x + forwardPos.x,
            transform.position.y + forwardPos.y,
            transform.position.z + forwardPos.z);
        return testPosition;
    }

    //AGENT_________________________________________________________

    public NavMeshAgent GetAgent()
    {
        return navAgent;
    }

    public Animator GetAnimator()
    {
        return anim;
    }

}
