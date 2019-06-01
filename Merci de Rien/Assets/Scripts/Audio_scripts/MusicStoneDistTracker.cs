using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStoneDistTracker : MonoBehaviour
{

    GameObject player=null;
    Game_Music_Manager musicManager;

    MusicStone musicStone;

    bool playerIsNear=false;
    public float distPlayer;


    private void Start()
    {
        musicStone = transform.parent.GetComponent<MusicStone>();
        musicManager = Game_Music_Manager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !playerIsNear)
        {
            if (player == null)
                player = other.gameObject;
            playerIsNear = true;
            Debug.Log("spotted");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerIsNear)
            playerIsNear = false;
        Debug.Log("left");
    }

    private void Update()
    {
        if (playerIsNear)
        {
            distPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            musicManager.MusicStoneDistance(musicStone.stoneID, distPlayer);
        }
    }
}
