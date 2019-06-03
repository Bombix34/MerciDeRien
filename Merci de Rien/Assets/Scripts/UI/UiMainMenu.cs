using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMainMenu : MonoBehaviour
{

    PlayerInputManager inputs;
    Animator animator;

    List<Button> menuButtons;
    int indexMenu = 0;

    [SerializeField]
    GameObject menuPanel;

    MenuState menuState = MenuState.pressStart;

    void Start()
    {
        inputs = GetComponent<PlayerInputManager>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(inputs.GetStartInput())
        {
            animator.SetBool("MainMenu", true);
            menuState = MenuState.menu;
        }
    }

    void UpdateButtonSelected()
    {
        if (menuState != MenuState.menu)
            return;
        menuButtons[indexMenu].Select();
    }

    public enum MenuState
    {
        pressStart,
        menu,
        credits
    }
}
