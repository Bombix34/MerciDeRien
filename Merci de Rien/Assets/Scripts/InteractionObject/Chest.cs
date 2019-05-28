using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractObject
{
    Animator animator;
    [SerializeField]
    GameObject particle;

    [SerializeField]
    PnjOwnerArea area;

    [SerializeField]
    List<Transform> targetPatounesPos;

    bool HasBeenOpened = false;

    int patouneNb = 0;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        particle.SetActive(false);
        objectType = ObjectType.Coffre;
        patouneNb = (int)Random.Range(0f, 7f);
    }

    public override void StartInteraction()
    {
        if((HasBeenOpened)||(CanInteract==false))
        {
            return;
        }
        base.StartInteraction();
        animator.SetBool("IsOpen", true);
        if(patouneNb>0)
            particle.SetActive(true);
        StartCoroutine(InstantiatePatoune());
        CheckSteal();
    }

    public override void EndInteraction()
    {
     //  animator.SetBool("IsOpen", false);
        particle.SetActive(false);
        HasBeenOpened = true;
        CanInteract = false;
    }

    public void CheckSteal()
    {
        if((area!=null)&&(!HasBeenOpened)&&(!CanTakeObject))
        {
            area.IncrementOtherObjectStealed();
        }
    }

    IEnumerator InstantiatePatoune()
    {
        
        Vector3 directionForce;
        GameObject curPatoune;
        yield return new WaitForSeconds(0.4f);
        for(int i = 0; i < patouneNb;i++)
        {
            directionForce = GetPatouneProjectilePos().position;
            curPatoune = Instantiate(GameManager.Instance.GetPatounePrefab(), transform, true) as GameObject;
            GameObject patouneInteract = curPatoune.GetComponentInChildren<Patoune>().gameObject;
            patouneInteract.SetActive(false);
            curPatoune.transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
            curPatoune.GetComponent<Rigidbody>().AddForce((directionForce+(Vector3.up*8f) *170f));
            yield return new WaitForSeconds(0.2f);
            patouneInteract.SetActive(true);
        }
    }

    public Transform GetPatouneProjectilePos()
    {
        return targetPatounesPos[(int)Random.Range(0f, targetPatounesPos.Count)];
    }
}
