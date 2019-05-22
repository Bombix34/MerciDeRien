using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStone : BringObject
{
    [SerializeField]
    private Game_Music_Manager musicManager;

    [SerializeField]
    private int stoneID;

    CapsuleCollider capsule;
    int stoneHP;

    protected override void Start()
    {
        base.Start();
        CanInteract = false;
        capsule = GetComponent<CapsuleCollider>();
        objectType = InteractObject.ObjectType.MusicStone;
        stoneHP = 3;
    }


    private new void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BringObject" || collision.gameObject.tag == "InteractToolObject")
        {
            stoneHP -= 1;

            switch (stoneHP)
            {
                case 3:
                case 2:
                case 1:
                    musicManager.MusicStoneHurt(stoneID);
                    break;
                case 0:
                    musicManager.MusicStoneDestroyed(stoneID);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
    }

    public override void EndInteraction()
    {
    }
}
