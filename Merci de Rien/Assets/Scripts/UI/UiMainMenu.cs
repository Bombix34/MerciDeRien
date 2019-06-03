using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiMainMenu : MonoBehaviour
{
    [SerializeField]
    SettingsManager settings;


    PlayerInputManager inputs;
    Animator animator;

    public List<Button> menuButtons;
    public List<Dialogue> buttonsText;
    int indexMenu = 0;

    float inputTimer = 0.2f;

    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    GameObject creditsPanel;
    public Dialogue creditsText;
    [SerializeField]
    GameObject mainLogo;

    [SerializeField]
    List<Sprite> languageLogos;
    

    [SerializeField]
    Image languageImg;

    MenuState menuState = MenuState.pressStart;
    

    void Start()
    {
        inputs = GetComponent<PlayerInputManager>();
        animator = GetComponent<Animator>();
        menuButtons[0].Select();
        creditsPanel.SetActive(false);
        menuPanel.SetActive(false);
        languageImg.sprite = languageLogos[0];
        settings.currentLanguage = SettingsManager.Language.francais;
        SwitchButtonLanguage();
    }
    
    void Update()
    {
        switch (menuState)
        {
            case MenuState.pressStart:
                if (inputs.GetStartInput())
                {
                    animator.SetBool("MainMenu", true);
                    menuState = MenuState.menu;
                    StartCoroutine(EndIntroMenu());
                }
                break;
            case MenuState.menu:
                UpdateButtonSelected();
                ApplyInput();
                if (inputTimer>0)
                {
                    inputTimer -= Time.deltaTime;
                    return;
                }
                if (inputs.GetMovementInputY() > 0.03f)
                {
                    IncrementIndex(false);
                    inputTimer = 0.2f;
                }
                else if(inputs.GetMovementInputY()<-0.03f)
                {
                    IncrementIndex(true);
                    inputTimer = 0.2f;
                }
                break;
            case MenuState.credits:
                if (inputTimer > 0)
                {
                    inputTimer -= Time.deltaTime;
                    return;
                }
                if (inputs.GetPressAnyButton())
                {
                    menuState = MenuState.menu;
                    mainLogo.SetActive(true);
                    menuPanel.SetActive(true);
                    creditsPanel.SetActive(false);
                    inputTimer = 0.2f;
                }
                break;
        }
    }

    void UpdateButtonSelected()
    {
        menuButtons[indexMenu].Select();
       /* for(int i = 0;i <menuButtons.Count;i++)
        {
            if (i == indexMenu)
                menuButtons[i].GetComponent<Image>().color = Color.green;
            else
                menuButtons[i].GetComponent<Image>().color = Color.white;
        }*/
    }

    void SwitchButtonLanguage()
    {
        if (settings.currentLanguage == SettingsManager.Language.francais)
        {
            menuButtons[0].GetComponentInChildren<Text>().text = buttonsText[0].frenchSentences[0];
            menuButtons[2].GetComponentInChildren<Text>().text = buttonsText[1].frenchSentences[0];
            menuButtons[3].GetComponentInChildren<Text>().text = buttonsText[2].frenchSentences[0];

            creditsPanel.GetComponentInChildren<Text>().text = creditsText.frenchSentences[0];
        }
        else
        {
            menuButtons[0].GetComponentInChildren<Text>().text = buttonsText[0].englishSentences[0];
            menuButtons[2].GetComponentInChildren<Text>().text = buttonsText[1].englishSentences[0];
            menuButtons[3].GetComponentInChildren<Text>().text = buttonsText[2].englishSentences[0];

            creditsPanel.GetComponentInChildren<Text>().text = creditsText.englishSentences[0];
        }
    }

    public void ApplyInput()
    {
        if(inputs.GetInteractInputDown())
        {
            switch(indexMenu)
            {
                case 0:
                    SceneManager.LoadScene("LevelDesign");
                    break;
                case 1:
                    SwitchLanguage();
                    SwitchButtonLanguage();
                    break;
                case 2:
                    inputTimer = 0.2f;
                    menuState = MenuState.credits;
                    mainLogo.SetActive(false);
                    menuPanel.SetActive(false);
                    creditsPanel.SetActive(true);
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
        }
    }

    void SwitchLanguage()
    {
        if(settings.currentLanguage==SettingsManager.Language.francais)
        {
            languageImg.sprite = languageLogos[1];
            settings.currentLanguage = SettingsManager.Language.english;
        }
        else
        {
            languageImg.sprite = languageLogos[0];
            settings.currentLanguage = SettingsManager.Language.francais;
        }
    }

    void IncrementIndex(bool IsPlus)
    {
        if(IsPlus)
        {
            indexMenu++;
            if (indexMenu >= menuButtons.Count)
                indexMenu = 0;
        }
        else
        {
            indexMenu--;
            if (indexMenu < 0)
                indexMenu = menuButtons.Count - 1;
        }
    }

    IEnumerator EndIntroMenu()
    {
        yield return new WaitForSeconds(1f);
        animator.enabled = false;
        menuPanel.SetActive(true);
    }

    public enum MenuState
    {
        pressStart,
        menu,
        credits
    }
}
