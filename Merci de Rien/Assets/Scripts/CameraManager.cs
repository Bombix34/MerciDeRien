using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera baseCamera;
    public CinemachineVirtualCamera zoomCamera;
    public CinemachineFreeLook dialogueCamera;

    public void SetNewCamera(CameraType newCameraType)
    {
        baseCamera.Priority = 10;
        zoomCamera.Priority = 10;
        dialogueCamera.Priority = 10;
        switch(newCameraType)
        {
            case CameraType.Base:
               // transform.rotation = Quaternion.Euler(45f, 0f, 0f);
                baseCamera.Priority = 20;
                break;
            case CameraType.Zoom:
               // transform.rotation = Quaternion.Euler(45f, 0f, 0f);
                zoomCamera.Priority = 20;
                break;
            case CameraType.Dialogue:
                dialogueCamera.Priority = 20;
                break;
        }
    }
    
    public enum CameraType
    {
        Base,
        Zoom,
        Dialogue
    }
}
