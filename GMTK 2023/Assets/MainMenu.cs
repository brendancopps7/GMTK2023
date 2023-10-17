using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructionsMenu;
    public GameObject loreMenu;

    public void Start()
    {
        activateMain();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activateMain();
        }
    }
    
    public void activateMain()
    {
        instructionsMenu.SetActive(false);
        loreMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void activateInstructions()
    {
        instructionsMenu.SetActive(true);
        loreMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void activateLore()
    {
        instructionsMenu.SetActive(false);
        loreMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void StartGame()
    {
        //Main Game has scene index 1
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
