using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStoneDistTracker : MonoBehaviour
{
    // public GameObject player;
    // public Game_Music_Manager musicManager;

    GameObject player;
    Game_Music_Manager musicManager;

    [SerializeField]
    MusicStone musicStone;

    bool playerIsNear;
    public float distPlayer;


    private void Start()
    {
        playerIsNear = false;
        player = GameObject.FindGameObjectWithTag("Player");
        musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<Game_Music_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !playerIsNear)
            playerIsNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && playerIsNear)
            playerIsNear = false;
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
