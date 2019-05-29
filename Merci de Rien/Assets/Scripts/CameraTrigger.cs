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

           // Debug.Log("Enter : " + cameraInZone);
            Camera.main.GetComponent<CameraManager>().SetNewCamera(cameraInZone);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if((other.gameObject.tag=="Player"))
        {
            if (Camera.main.GetComponent<CameraManager>().GetCurrentCameraType() == CameraManager.CameraType.Dialogue)
                return;
           // Debug.Log("stay : " + cameraInZone);
            Camera.main.GetComponent<CameraManager>().SetNewCamera(cameraInZone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           // Debug.Log("Exit : " + cameraInZone);
            Camera.main.GetComponent<CameraManager>().SetNewCamera(CameraManager.CameraType.Base);
        }
    }
}
