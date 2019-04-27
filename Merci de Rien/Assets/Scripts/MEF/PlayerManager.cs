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

    bool isWalkingPlaceholeder;

    void Awake()
    {
        inputs = GetComponent<PlayerInputManager>();
        mainCamera = Camera.main.transform;
        character = GetComponent<CharacterController>();

        //placeholder
        animPlaceholder = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeState(new PlayerBaseState(this));

        //PLACEHOLDER SON.
        //- Abdoul
        isWalkingPlaceholeder = false;
    }

    private void Update()
    {
        currentState.Execute();
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    private void OnDrawGizmos()
    {
        //TOOL DEBUG
        if (interactObject != null)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GetFrontPosition(), 0.3f);

    }

    //MOVEMENT FUNCTIONS______________________________________________________________________________

    public void Move()
    {
        Vector3 directionController = inputs.GetMovementInput();
        if (directionController == Vector3.zero)
        {
            currentVelocity = Vector3.zero;
            UpdateAnim();

            //PLACEHOLDER SON.
            //- Abdoul
            AkSoundEngine.PostEvent("MC_walk_end_PH_play", gameObject);
            isWalkingPlaceholeder = false;

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
        UpdateAnim();

        //PLACEHOLDER SON.
        //- Abdoul
        if (!isWalkingPlaceholeder)
        {
            isWalkingPlaceholeder = true;
            AkSoundEngine.PostEvent("MC_walk_PH_play", gameObject);
        }
    }




    public void ResetVelocity()
    {
        character.Move(Vector3.zero);
        animPlaceholder.SetFloat("MoveSpeed", 0f);
    }

    private void RotatePlayer(float x, float y)
    {
        Vector3 dir = new Vector3(x, 0, y);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), (reglages.rotationSpeed * 100) * Time.deltaTime);
    }

    private void UpdateAnim()
    {
        animPlaceholder.SetBool("Grounded", true);
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
                if (interactObject != null)
                    interactObject.GetComponent<InteractObject>().UpdateFeedback(false);
                interactObject = hitColliders[i].gameObject;
                interactObject.GetComponent<InteractObject>().UpdateFeedback(true);
                i = hitColliders.Length;
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

    public GameObject IsObstacle(Vector3 testPosition)
    {
        //NON UTILISE
        List<GameObject> finalList = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(testPosition, 0.3f);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag=="BringObject")
            {
                finalList.Add(hitColliders[i].gameObject);
            }
        }
        if (finalList.Count > 0)
            return finalList[0];
        else
            return null;
    }

    public bool CanInteract(string tag)
    {
        //AJOUTER LES TAGS LIEE A LINTERACTION ICI
        //PNJ, OUTILS, PORTES...
        return ((tag == "BringObject")||(tag=="PNJ"));
    }

    public Vector3 GetFrontPosition()
    {
        //FONCTION POUR OBTENIR LA POSITION DEVANT LE PERSONNAGE
        //POSITION OU INTERAGIR ET POSER LES OBJETS
        Vector3 forwardPos = transform.TransformDirection(Vector3.forward) * 0.5f;
        Vector3 testPosition = new Vector3(transform.position.x + forwardPos.x,
            transform.position.y + forwardPos.y,
            transform.position.z + forwardPos.z);
        return testPosition;
    }

    public Vector3 GetHeadingDirection()
    {
        return transform.TransformDirection(Vector3.forward);
    }

    //GET & SET________________________________________________________________________________________

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

    //SINGLETON________________________________________________________________________________________________

    private static PlayerManager s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static PlayerManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                Debug.Log("error");
                GameObject obj = new GameObject("Error");
                s_Instance = obj.AddComponent(typeof(PlayerManager)) as PlayerManager;
            }

            return s_Instance;
        }
    }
}
