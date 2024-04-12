using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void TryAgain()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene +1);
    }

    public void Bye()
    {
        Application.Quit();
    }

    public void RESTART()
    {
        SceneManager.LoadScene(1);
    }    
}
