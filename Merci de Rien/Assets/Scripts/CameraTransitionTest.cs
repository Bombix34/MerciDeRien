﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTransitionTest : MonoBehaviour
{

    public List<CinemachineVirtualCamera> cameras;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("ok");
            for(int i = 0; i < cameras.Count;i++)
            {
            }
        }
    }
}
