using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDemo : MonoBehaviour
{
    public int speed=1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);
    }
}
