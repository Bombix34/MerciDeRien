﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]

public class PlayerManager : ObjectManager
{
    public PlayerReglages reglages;

    PlayerInputManager inputs;

    CharacterController character;

    Vector3 currentVelocity;

    Transform mainCamera;

    Animator animPlaceholder;

    GameObject interactObject;

    void Awake()
    {
        inputs = GetComponent<PlayerInputManager>();
        mainCamera = Camera.main.transform;
        character = GetComponent<CharacterController>();

        //placeholder
        animPlaceholder = GetComponent<Animator>();
    }

    void Start()
    {
        ChangeState(new PlayerBaseState(this));
    }

    private void Update()
    {
        currentState.Execute();

        UpdateAnim();
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

   //MOVEMENT FUNCTIONS______________________________________________________________________________

    public void Move()
    {
        Vector3 directionController = inputs.GetMovementInput();
        if (directionController == Vector3.zero)
        {
            currentVelocity = Vector3.zero;
            return;
        }
        //init values
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        Vector3 right = Quaternion.Euler(0f, 90f, 0f) * forward;

        Vector3 rightMove = right * (10 * inputs.GetMovementInputX()) * Time.deltaTime;
        Vector3 upMove = forward * (10 * inputs.GetMovementInputY()) * Time.deltaTime;
        Vector3 heading = (rightMove + upMove);

        RotatePlayer(inputs.GetMovementInputX(), inputs.GetMovementInputY());
        currentVelocity = Vector3.zero;
        currentVelocity += heading * reglages.moveSpeed;
        character.Move(currentVelocity);
    }

    private void RotatePlayer(float x, float y)
    {
        Vector3 dir = new Vector3(x, 0, y);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), (reglages.rotationSpeed * 100) * Time.deltaTime);
    }

    private void UpdateAnim()
    {
        if (currentVelocity.magnitude == 0)
            animPlaceholder.SetBool("Grounded", character.isGrounded);

        animPlaceholder.SetFloat("MoveSpeed", currentVelocity.magnitude / 0.1f);
    }

    //RAYCAST OBJECTS___________________________________________________________________________________

    public GameObject RaycastObject()
    {
        bool isResult = false;
        GameObject raycastObject = null;
        Vector3 testPosition = GetFrontPosition();

        Collider[] hitColliders = Physics.OverlapSphere(testPosition, 0.3f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (CanInteract(hitColliders[i].tag))
            {
                isResult = true;
                interactObject = hitColliders[i].gameObject;
                interactObject.GetComponent<InteractObject>().UpdateFeedback(true);
            }
            i++;
        }
        if (!isResult)
        {
            if(interactObject!=null)
                interactObject.GetComponent<InteractObject>().UpdateFeedback(false);
            interactObject = null;
        }
        return raycastObject;
    }

    public bool IsObstacle()
    {
        Vector3 testPosition = GetFrontPosition();
        Collider[] hitColliders = Physics.OverlapSphere(testPosition, 0.3f,0);
        if(hitColliders.Length>0)
         Debug.Log(hitColliders[0]);
        return hitColliders.Length != 0;
    }

    public bool CanInteract( string tag)
    {
        //AJOUTER LES TAGS LIEE A LINTERACTION ICI
        //PNJ, OUTILS, PORTES...
        return (tag == "BringObject");
    }

    public Vector3 GetFrontPosition()
    {
        //FONCTION POUR POSER LES OBJETS DEVANT LUI
        Vector3 forwardPos = transform.TransformDirection(Vector3.forward) * 0.5f;
        Vector3 testPosition = new Vector3(transform.position.x + forwardPos.x,
            transform.position.y + forwardPos.y,
            transform.position.z + forwardPos.z);
        return testPosition;
    }

    //GET & SET_____________________________________________________________________________________

    public PlayerInputManager GetInputManager()
    {
        return inputs;
    }

    public GameObject GetNearInteractObject()
    {
        return interactObject;
    }

    public Animator GetAnimator()
    {
        return animPlaceholder;
    }

    public void SetNearInteractObject(GameObject newVal)
    {
        interactObject = newVal;
    }

}
