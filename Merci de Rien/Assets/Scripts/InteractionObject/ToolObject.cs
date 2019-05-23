using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToolObject : BringObject
{
    CapsuleCollider capsule;

    public bool IsUsingObject { get; set; } = false;
    public PlayerUseToolState curStatePlayer { get; set; } = null;

    NavMeshObstacle navObstacle;

    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        navObstacle = GetComponent<NavMeshObstacle>();
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        navObstacle.enabled = false;
        body.useGravity = false;
        capsule.isTrigger = true;
        body.isKinematic = true;
    }

    public override void EndInteraction()
    {
        body.useGravity = true;
        navObstacle.enabled = true;
        capsule.isTrigger = false;
        body.isKinematic = false;
        body.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        //quand l'objet est touché par un autre objet lancé
        if (collision.gameObject.tag == "BringObject")
        {
            BringObject otherObject = collision.gameObject.GetComponent<BringObject>();
            if (otherObject.GetObjectReglages().IsBreakingThings && otherObject.IsLaunch)
                StartBreaking();
        }
        //quand on lance l'objet
        if (!IsLaunch)
            return;
        StartBreaking();
        if (collision.gameObject.tag == "PNJ")
        {
            PnjManager pnj = collision.gameObject.GetComponent<PnjManager>();
            StartHurting(pnj);
        }
        else if (collision.gameObject.name == "Terrain")
        {
            StartCoroutine(StopOnGround());
        }
    }

    protected void OnTriggerEnter(Collider collision)
    {
        if (!IsUsingObject)
            return;
        //quand l'objet est touché par un autre objet lancé
        if (collision.gameObject.tag == "BringObject")
        {
            BringObject otherObject = collision.gameObject.GetComponent<BringObject>();
            if (reglages.IsBreakingThings&&otherObject.GetReglages().IsBreaking)
                otherObject.StartBreaking();
        }
        StartBreaking();
        if (collision.gameObject.tag == "PNJ")
        {
            PnjManager pnj = collision.gameObject.GetComponent<PnjManager>();
            StartHurting(pnj);
        }
        if((collision.gameObject.name!="Terrain")&&(collision.gameObject.tag!="Player") && (collision.gameObject.tag != "TOOL"))
            EndUseObjectOnCollision();
    }

    public void EndUseObjectOnCollision()
    {
        if (curStatePlayer == null)
            return;
        IsUsingObject = false;
       curStatePlayer.EndUseObjectOnCollision();
    }

    IEnumerator StopOnGround()
    {
        yield return new WaitForSeconds(0.7f);
        body.isKinematic = true;
    }
}