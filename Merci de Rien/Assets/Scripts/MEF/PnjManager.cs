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
