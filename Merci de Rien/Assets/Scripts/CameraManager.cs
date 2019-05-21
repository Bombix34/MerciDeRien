using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CameraReglages reglages;
    public CinemachineVirtualCamera baseCamera;
    public CinemachineVirtualCamera zoomCamera;
    public CinemachineVirtualCamera dezoomCamera;
    public CinemachineVirtualCamera dialogueCamera;

    CameraType currentCamera;

    private void Start()
    {
        currentCamera = CameraType.Base;
        SetNewCamera(CameraType.Base);
        transform.rotation = Quaternion.Euler(reglages.cameraRotation, 90f, 0f);
    }

    private void Update()
    {
       // transform.rotation = Quaternion.Euler(reglages.cameraRotation, 0f, 0f);
    }

    public void SetNewCamera(CameraType newCameraType)
    {
        //SetPreviousCamera();
        baseCamera.Priority = 10;
        zoomCamera.Priority = 10;
        dialogueCamera.Priority = 10;
        switch(newCameraType)
        {
            case CameraType.Base:
                baseCamera.Priority = 20;
                break;
            case CameraType.Zoom:
                zoomCamera.Priority = 20;
                break;
            case CameraType.Dezoom:
                dezoomCamera.Priority = 20;
                break;
            case CameraType.Dialogue:
              //  transform.rotation = Quaternion.Euler(35f, -25f, 0f);
                dialogueCamera.Priority = 20;
                break;
        }
        currentCamera = newCameraType;
    }

    public void SetDialogueCamera(GameObject target)
    {
       // Debug.Log(dialogueCamera.m_Follow);
        dialogueCamera.m_Follow = target.transform;
        SetNewCamera(CameraType.Dialogue);
    }

    public CameraType GetCurrentCameraType()
    {
        return currentCamera;
    }

    public enum CameraType
    {
        Base,
        Zoom,
        Dezoom,
        Dialogue
    }
}
