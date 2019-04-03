using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera baseCamera;
    public CinemachineVirtualCamera zoomCamera;
    public CinemachineVirtualCamera dialogueCamera;

    public Quaternion currentRotation;

    private void Start()
    {
        SetNewCamera(CameraType.Base);
    }

    private void Update()
    {
    }

    public void SetNewCamera(CameraType newCameraType)
    {
        baseCamera.Priority = 10;
        zoomCamera.Priority = 10;
        dialogueCamera.Priority = 10;
        switch(newCameraType)
        {
            case CameraType.Base:
                transform.rotation = Quaternion.Euler(45f, 0f, 0f);
                baseCamera.Priority = 20;
                break;
            case CameraType.Zoom:
                transform.rotation = Quaternion.Euler(45f, 0f, 0f);
                zoomCamera.Priority = 20;
                break;
            case CameraType.Dialogue:
                transform.rotation = Quaternion.Euler(35f, -25f, 0f);
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
