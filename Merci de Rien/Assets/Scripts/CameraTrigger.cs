using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    public CameraManager.CameraType cameraInZone;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Camera.main.GetComponent<CameraManager>().SetNewCamera(cameraInZone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Camera.main.GetComponent<CameraManager>().SetNewCamera(CameraManager.CameraType.Base);
        }
    }
}
