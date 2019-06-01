using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStone : BringObject
{
    private Game_Music_Manager musicManager;

    public int stoneID;
    private int stoneHP;

    CapsuleCollider capsule;
    MusicStoneDistTracker tracker;

    

    protected override void Start()
    {
        base.Start();
        CanInteract = false;
        capsule = GetComponent<CapsuleCollider>();
        objectType = InteractObject.ObjectType.MusicStone;
        musicManager = Game_Music_Manager.Instance;

        stoneHP = 3;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BringObject" || collision.gameObject.tag == "InteractToolObject")
        {
            BreakMusicStone();
        }
    }

    public void BreakMusicStone()
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

    public override void StartInteraction()
    {
        base.StartInteraction();
    }

    public override void EndInteraction()
    {
    }
}
