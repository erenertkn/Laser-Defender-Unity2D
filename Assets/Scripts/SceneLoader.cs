using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int thisScene;
    public void ChangeNextScene()
    {
        thisScene = SceneManager.GetActiveScene().buildIndex;
        if(isLastScene())
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(thisScene++);
        }
        
    }
    private bool isLastScene()
    {
        if(thisScene== SceneManager.GetActiveScene().buildIndex)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void GoLastScene()
    {
        SceneManager.LoadScene(3);
    }
}
