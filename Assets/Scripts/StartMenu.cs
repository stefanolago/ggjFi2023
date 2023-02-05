using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Livello Completo");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
