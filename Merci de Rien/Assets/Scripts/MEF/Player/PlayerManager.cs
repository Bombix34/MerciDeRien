using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]

public class PlayerManager : ObjectManager
{
    public PlayerReglages reglages;

    PlayerInputManager inputs;
    // Rigidbody rigidbody;
    CharacterController character;

    Vector3 currentVelocity;

    Transform mainCamera;


    Animator animPlaceholder;
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

        if (inputs.GetInteractInput())
            animPlaceholder.SetTrigger("Wave");
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

//MOVEMENT FUNCTIONS__________________________________

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

        Vector3 rightMove = right * (10*inputs.GetMovementInputX()) * Time.deltaTime;
        Vector3 upMove = forward * (10*inputs.GetMovementInputY()) * Time.deltaTime;
        Vector3 heading = (rightMove + upMove);

        RotatePlayer(inputs.GetMovementInputX(), inputs.GetMovementInputY());
        currentVelocity = Vector3.zero;
        currentVelocity += heading * reglages.moveSpeed;
        character.Move(currentVelocity);
    }

    private void RotatePlayer(float x, float y)
    {
        Vector3 dir = new Vector3(x, 0, y);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), (reglages.rotationSpeed*100) * Time.deltaTime);
    }

    private void UpdateAnim()
    {
        if(currentVelocity.magnitude==0)
            animPlaceholder.SetBool("Grounded", character.isGrounded);

        animPlaceholder.SetFloat("MoveSpeed", currentVelocity.magnitude/0.1f);
    }

//RAYCAST OBJECTS___________________________________________________________________________________

    public GameObject RaycastObject()
    {
        GameObject raycastObject = null;
        RaycastHit hit;
      //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward));
        Vector3 testPosition = new Vector3(transform.position.x + transform.TransformDirection(Vector3.forward).x,
            transform.position.y + transform.TransformDirection(Vector3.forward).y,
            transform.position.z + transform.TransformDirection(Vector3.forward).z);
        Ray ray = new Ray(testPosition, transform.TransformDirection(Vector3.forward));
        Debug.DrawLine(transform.position, testPosition);

        if(Physics.SphereCast(ray,5f,out hit ))
        {
            Debug.Log(hit.transform.gameObject);
        }
       /* if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit, 10f))
        {
            if(hit.transform.gameObject.tag!="BringObject")
            {
                return raycastObject;
            }
            else
            {
                raycastObject = hit.transform.gameObject;
                Debug.Log(hit.transform.gameObject.name);
            }
        }*/
        return raycastObject;
    }

}
