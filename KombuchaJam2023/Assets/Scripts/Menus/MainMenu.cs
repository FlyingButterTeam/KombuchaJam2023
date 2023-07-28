using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int docksSceneNumber = 1;

    [Header("References")]
    [SerializeField] GameObject mainMenuObject;
    [SerializeField] GameObject configurationMenuObject;


    public void PlayDocksScene()
    {
        SceneManager.LoadScene(docksSceneNumber);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenConfiguration()
    {
        mainMenuObject.SetActive(false);
        configurationMenuObject.SetActive(true);
    }

    public void CloseConfiguration()
    {
        mainMenuObject.SetActive(true);
        configurationMenuObject.SetActive(false);
    }
}
