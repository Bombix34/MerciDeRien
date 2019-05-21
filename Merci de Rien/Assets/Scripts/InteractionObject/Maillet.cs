using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maillet : BringObject
{
    CapsuleCollider capsule;

    protected override void Start()
    {
        base.Start();
        objectType = ObjectType.Maillet;
        body = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        body.useGravity = false;
        capsule.isTrigger = false;
        body.isKinematic = true;
    }

    public override void EndInteraction()
    {
        body.useGravity = true;
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
        else if(collision.gameObject.name=="Terrain")
        {
            StartCoroutine(StopOnGround());
        }
    }

    IEnumerator StopOnGround()
    {
        yield return new WaitForSeconds(0.7f);
        body.isKinematic = true;
    }
}