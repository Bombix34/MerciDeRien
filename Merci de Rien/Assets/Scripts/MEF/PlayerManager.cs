using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]

public class PlayerManager : ObjectManager
{
    public PlayerReglages reglages;

    public Transform handTool;

    PlayerInputManager inputs;

    CharacterController character;

    PlayerSoundManager soundManager;

    Vector3 currentVelocity;

    Transform mainCamera;

    Animator animator;

    GameObject interactObject;

    [SerializeField]
    GameObject bringPosition;

    BringObject bringObject = null;
    ToolObject toolObject = null;
   

    void Awake()
    {
        inputs = GetComponent<PlayerInputManager>();
        mainCamera = Camera.main.transform;
        character = GetComponent<CharacterController>();
        soundManager = GetComponent<PlayerSoundManager>();

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        EventManager.Instance.SetPlayer(this.gameObject);
        ChangeState(new PlayerBaseState(this));
    }

    private void Update()
    {
        //Debug.Log(currentState.stateName);
        currentState.Execute();
    }

    private void FixedUpdate()
    {
        if (CanMove)
            DoMove();
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,Vector3.down);
        //TOOL DEBUG
        if (interactObject != null)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.yellow;
      //  Gizmos.DrawSphere(GetFrontPosition(),reglages.raycastRadius);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.GetComponent<Terrain>()!=null)
        {
            Terrain ter = hit.gameObject.GetComponent<Terrain>();
        }
    }

    //MOVEMENT FUNCTIONS______________________________________________________________________________

    bool CanMove = true;

    public void Move(bool IsOn)
    {
        CanMove = IsOn;
    }

    public void DoMove()
    {
        Vector3 directionController = inputs.GetMovementInput();
        GravitySpeed();
        if (directionController == Vector3.zero)
        {
            currentVelocity = Vector3.zero;
            UpdateAnim();
            return;
        }
        //init values
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        Vector3 right = Quaternion.Euler(0f, 90f, 0f) * forward;

        Vector3 rightMove = right * (10 * inputs.GetMovementInputX()) * Time.deltaTime;
        Vector3 upMove = forward * (10 * inputs.GetMovementInputY()) * Time.deltaTime;
        Vector3 heading = (rightMove + upMove).normalized;

        float amplitude = new Vector2(inputs.GetMovementInputX(), inputs.GetMovementInputY()).magnitude;

        RotatePlayer(inputs.GetMovementInputY(), -inputs.GetMovementInputX());
        currentVelocity = Vector3.zero;
        currentVelocity += heading * amplitude * (reglages.moveSpeed / 5f);
        character.Move(currentVelocity);
        UpdateAnim();
    }

    public void GravitySpeed()
    {
        if (!character.isGrounded)
        {
            currentVelocity = Vector3.zero;
            float gravity = reglages.gravity * Time.deltaTime;
            currentVelocity = new Vector3(currentVelocity.x, currentVelocity.y - gravity, currentVelocity.z);
            character.Move(currentVelocity);
        }
    }

    public void ResetVelocity()
    {
        character.Move(Vector3.zero);
        animator.SetFloat("MoveSpeed", 0f);
    }

    private void RotatePlayer(float x, float y)
    {
        Vector3 dir = new Vector3(x, 0, y);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), (reglages.rotationSpeed * 100) * Time.deltaTime);
    }

    private void UpdateAnim()
    {
        if (animator == null)
            return;
        animator.SetFloat("MoveSpeed", currentVelocity.magnitude / 0.1f);
    }

    //RAYCAST OBJECTS___________________________________________________________________________________

    public GameObject RaycastObject(bool interactOnly)
    {
        bool isResult = false;
        GameObject raycastObject = null;
        Vector3 testPosition = GetFrontPosition();

        Collider[] hitColliders = Physics.OverlapSphere(testPosition, reglages.raycastRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(!interactOnly)
            {
                if (CanInteract(hitColliders[i].gameObject))
                {
                    isResult = true;
                    if (interactObject != null)
                        interactObject.GetComponent<InteractObject>().UpdateFeedback(false);
                    interactObject = hitColliders[i].gameObject;
                    interactObject.GetComponent<InteractObject>().UpdateFeedback(true);
                    i = hitColliders.Length;
                    soundManager.PlayInteractFeedbackSound(interactObject);
                }
            }
            else
            {
                if (IsInteractionWhileCarying(hitColliders[i].gameObject))
                {
                    isResult = true;
                    if (interactObject != null)
                        interactObject.GetComponent<InteractObject>().UpdateFeedback(false);
                    interactObject = hitColliders[i].gameObject;
                    interactObject.GetComponent<InteractObject>().UpdateFeedback(true);
                    i = hitColliders.Length;
                    soundManager.PlayInteractFeedbackSound(interactObject);
                }
            }
            i++;
        }
        if (!isResult)
        {
            if(interactObject!=null)
                interactObject.GetComponent<InteractObject>().UpdateFeedback(false);
            interactObject = null;
            soundManager.ResetInteractObject();
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

    public bool IsPNJ(GameObject concerned)
    {
        return concerned.GetComponent<PnjManager>()!=null && concerned.GetComponent<InteractObject>().CanInteract;
    }

    public bool IsInteractionWhileCarying(GameObject concerned)
    {
        //POUR INTERAGIR AVEC LES PORTAILS/COFFRES ALORS QUON PORTE UN ITEM
        bool result = false;
        result = concerned.GetComponent<PnjManager>() != null && concerned.GetComponent<InteractObject>().CanInteract;
        if(!result)
        {
            result = concerned.GetComponent<InteractObject>() != null && concerned.GetComponent<InteractObject>().CanInteract
                && (concerned.GetComponent<InteractObject>().objectType==InteractObject.ObjectType.Portail || concerned.GetComponent<InteractObject>().objectType == InteractObject.ObjectType.Coffre 
                || concerned.GetComponent<InteractObject>().objectType==InteractObject.ObjectType.LittleAxo || concerned.GetComponent<InteractObject>().objectType==InteractObject.ObjectType.Pancarte);
        }
        return result;
    }

    public bool CanInteract(GameObject concerned)
    {
        return concerned.GetComponent<InteractObject>() != null && concerned.GetComponent<InteractObject>().CanInteract;
        //AJOUTER LES TAGS LIEE A LINTERACTION ICI
        //PNJ, OUTILS, PORTES...
        // return ((tag == "BringObject")||(tag=="PNJ"));
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

    public void ResetNearInteractObject()
    {
        if (interactObject != null)
            interactObject.GetComponent<InteractObject>().UpdateFeedback(false);
        interactObject = null;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetNearInteractObject(GameObject newVal)
    {
        interactObject = newVal;
    }

    public Transform GetBringPosition()
    {
        return bringPosition.transform;
    }

    public bool IsBringingWaitingObject(PnjManager pnj, InteractObject obj)
    {
        return (pnj.SpecialObjectEvent(obj));
    }

    public BringObject IsBringingObject()
    {
        /*BringObject returnVal = null;
        if(currentState.stateName== "PLAYER_BRING_OBJECT_STATE")
        {
            PlayerBringObjectState curState = (PlayerBringObjectState)currentState;
            returnVal = curState.GetBringingObject();
        }*/
        return bringObject;
    }

    public ToolObject IsBringingTool()
    {
        /*ToolObject returnVal = null;
        if (currentState.stateName == "PLAYER_USE_TOOL_STATE")
        {
            PlayerUseToolState curState = (PlayerUseToolState)currentState;
            returnVal = curState.GetToolObject();
        }*/
        return toolObject;
    }

    public void SetBringingObject(BringObject obj)
    {
        toolObject = null;
        bringObject = obj;
    }
    public void SetToolObject(ToolObject tool)
    {
        bringObject = null;
        toolObject = tool;
    }

    public bool IsOccuped()
    {
        return (currentState.stateName == "PLAYER_DIALOGUE_STATE" || currentState.stateName == "PLAYER_WAIT_STATE");
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
