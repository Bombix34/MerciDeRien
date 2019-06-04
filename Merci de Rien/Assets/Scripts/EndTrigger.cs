using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public float waitTime = 5f;
    float timeToChange = 5f;

    [SerializeField]
    GameObject endPanel;

    void Start()
    {
        endPanel.SetActive(false);
    }
    
    void Update()
    {
        waitTime -= Time.deltaTime;
        if(waitTime<=0)
        {
            endPanel.SetActive(true);
            timeToChange -= Time.deltaTime;
            if(timeToChange<=0)
            {
                SceneManager.LoadScene("SplashScreen");
            }
        }
    }
}
