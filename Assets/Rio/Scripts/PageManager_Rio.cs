using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageManager_Rio : MonoBehaviour
{
    public AudioSource player;
    public void GoToScene(int index)
    {
        player.Play();
        SceneManager.LoadScene(index);
    }
}
