using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : InteractObject
{
    Animator animator;

    [SerializeField]
    Dialogue closedPortailText;


    public int KeyNeeded { get; set; } = -1;

    public bool IsOpen { get; set; } = false;
    
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        objectType = ObjectType.Portail;
        CanTakeObject = true;
    }
    

    public override void StartInteraction()
    {
        base.StartInteraction();
        PlayerManager player = EventManager.Instance.GetPlayer().GetComponent<PlayerManager>();
        if (IsOpen)
        {
            animator.SetBool("IsOpen", false);
            IsOpen = false;
        }
        else
        {
            bool hasKey = false;
            if (KeyNeeded == -1)
                hasKey = true;
            else
                hasKey = player.HasKey(KeyNeeded);
            if(hasKey)
            {
                animator.SetBool("IsOpen", true);
                IsOpen = true;
            }
            else
            {
                animator.SetTrigger("Closed");
            }
        }
        StartCoroutine(IsMovingCoroutine());
        player.ChangeState(new PlayerBaseState(player));
    }

    public override void EndInteraction()
    {
        //  animator.SetBool("IsOpen", false);
    }

    public override string GetInteractText(bool isStealing)
    {
        string returnVal = "";
        SettingsManager settings = GameManager.Instance.settings;
        if (settings.currentLanguage == SettingsManager.Language.francais)
        {
            if (IsOpen)
                returnVal = closedPortailText.frenchSentences[0];
            else
                returnVal = interactText.frenchSentences[0];
        }
        else if (settings.currentLanguage == SettingsManager.Language.english)
        {
            if (IsOpen)
                returnVal = closedPortailText.englishSentences[0];
            else
                returnVal = interactText.englishSentences[0];
        }
        return returnVal;
    }

    IEnumerator IsMovingCoroutine()
    {
        CanInteract = false;
        yield return new WaitForSeconds(0.5f);
        CanInteract = true;
    }

}