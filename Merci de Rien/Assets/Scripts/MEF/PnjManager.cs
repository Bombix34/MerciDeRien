using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PnjDialogueManager))]
public class PnjManager : ObjectManager
{
    protected NavMeshAgent navAgent;
    Animator anim;

    [SerializeField]
    CharacterType character;

    [SerializeField]
    public Mood CurrentMood { get; set; } = Mood.neutral;

    public PnjDialogueManager dialogueManager{get;set;}

    protected InteractObject interactionManager;

    protected virtual void Awake()
    {
        dialogueManager = GetComponent<PnjDialogueManager>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        interactionManager = GetComponent<InteractObject>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        ChangeState(new WanderAroundState(this,this.transform.position));
        InitInteractScript();
    }

    protected virtual void Update()
    {
        currentState.Execute();
        UpdateAnim();
    }

    protected virtual void InitInteractScript()
    {
        interactionManager.CanTakeObject = true;
        interactionManager.CanInteract = true;
        interactionManager.characterOwner = character;
        interactionManager.objectType = InteractObject.ObjectType.PNJ;
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    protected virtual void OnDrawGizmos()
    {
        //TOOL DEBUG
        if (!Application.isPlaying)
            return;
        if (currentState.stateName == "WANDER_AROUND_STATE")
        {
            Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
            WanderAroundState curtState = (WanderAroundState)currentState;
            Gizmos.DrawSphere(curtState.InitPosition, curtState.Radius);
        }
    }

    //ANIM_______________________________________________________

    public Animator GetAnimator()
    {
        return anim;
    }

    public void UpdateAnim()
    {
        anim.SetFloat("MoveSpeed", (navAgent.velocity.magnitude / 0.1f)*0.03f);
    }

    //RAYCAST________________________________________________________

    public bool RaycastPlayer()
    {
        bool result = false;
        Vector3 testPosition = GetFrontPosition();

        Collider[] hitColliders = Physics.OverlapSphere(testPosition, 0.55f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Player")
            {
                result = true;
                i = hitColliders.Length;
            }
            i++;
        }
        return result;
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

    //EVENT_____________________________________________________________

    public void HurtingEvent()
    {
        EventManager.Instance.UpdateCharacterEvent(EventDatabase.EventType.violenceTotal, GetCharacterType(), 1);
    }

    public void TalkingEvent()
    {
        EventManager.Instance.UpdateCharacterEvent(EventDatabase.EventType.conversationTotal, GetCharacterType(), 1);
    }

    //AGENT_________________________________________________________

    public NavMeshAgent GetAgent()
    {
        return navAgent;
    }

    public InteractObject GetInteractionManager()
    {
        return interactionManager;
    }

    //CHARACTER____________________________________________________

    public CharacterType GetCharacterType()
    {
        return character;
    }

    public enum CharacterType
    {
       Artisan,
       Paysan,
       Pecheur,
       Responsable,
       Healer,
       Troubadour,
       Etranger,
       none
    }

    public enum Mood
    {
        neutral,
        aggressive
    }
}
