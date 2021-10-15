using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageManager_Rio : MonoBehaviour
{
    public AudioSource player;
    public void GoToScene(string scenename)
    {
        player.Play();
        SceneManager.LoadScene(scenename);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
