using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIScript : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayLv1()
    {
        SceneManager.LoadSceneAsync("Level1");
    }
    public void PlayLv2()
    {
        SceneManager.LoadSceneAsync("Level2");
    }
    public void PlayLv3()
    {
        SceneManager.LoadSceneAsync("Level3");
    }
    public void PlayLv4()
    {
        SceneManager.LoadSceneAsync("Level4");
    }

}
