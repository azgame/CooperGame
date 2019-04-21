using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayRyan()
    {
        SceneManager.LoadScene("Dev");
    }

    public void PlayAiden()
    {
        SceneManager.LoadScene("Scene01");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
