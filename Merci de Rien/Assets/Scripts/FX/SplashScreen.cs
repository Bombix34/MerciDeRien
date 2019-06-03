using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float splashTime = 3f;
    public string nextScene;

    void Update()
    {
        splashTime -= Time.deltaTime;
        if(splashTime<=0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
