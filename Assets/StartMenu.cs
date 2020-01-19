using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void PlayGame()
    {
        NetworkGUI.showGUI = true;
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public static class NetworkGUI
{
    public static bool showGUI { get; set; }

}
