using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStickSpawner : MonoBehaviour
{
    bool hasSpawn = false;

    [SerializeField]
    GameObject stickPrefab;

    public void SpawnStick()
    {
        if (hasSpawn)
            return;
        hasSpawn = true;
        GameObject stick = Instantiate(stickPrefab, transform.position, Quaternion.identity) as GameObject;
        stick.transform.position = this.transform.position;
    }
}
