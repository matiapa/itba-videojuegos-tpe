using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{

    public void LoadLevel(string nombreNivel) 
    {
        SceneManager.LoadScene(nombreNivel);
    }

    public void Help() 
    {
        SceneManager.LoadScene("HelpScene");
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("MenuScene");
    }


    public void Exit() 
    {
        Application.Quit();
    }
}
