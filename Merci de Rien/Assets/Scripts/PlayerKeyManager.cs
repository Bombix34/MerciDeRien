using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyManager : MonoBehaviour
{
    [SerializeField]
    List<int> playerKeys;

    void Start()
    {
        playerKeys = new List<int>();
    }

    public void AddKey(int keyNb)
    {
        playerKeys.Add(keyNb);
    }

    public void RemoveKey(int keyNb)
    {
        playerKeys.Remove(keyNb);
    }

    public bool HasKey(int keyNb)
    {
        bool result = false;
        foreach(var item in playerKeys)
        {
            if (item == keyNb)
                result = true;
        }
        return result;
    }
}
