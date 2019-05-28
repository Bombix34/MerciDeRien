using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTerrainDetection : MonoBehaviour
{
    public TerrainDetection.GroundLayer groundType;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag=="Player"||collision.gameObject.tag=="PNJ")
        {
            TerrainDetection managerSound = collision.gameObject.GetComponentInChildren<TerrainDetection>();
            if (!managerSound.IsPlayingFootstepSound)
                return;
            managerSound.CharIsOnSpecialSurface = true;
            managerSound.currentPlayerGroundLayer = groundType;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PNJ")
        {
            TerrainDetection managerSound = collision.gameObject.GetComponentInChildren<TerrainDetection>();
            if (!managerSound.IsPlayingFootstepSound)
                return;
            managerSound.CharIsOnSpecialSurface = false;
        }
    }
}
