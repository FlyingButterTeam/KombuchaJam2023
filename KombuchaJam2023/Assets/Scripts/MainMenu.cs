using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int docksSceneNumber = 1;
    
    public void PlayDocksScene()
    {
        SceneManager.LoadScene(docksSceneNumber);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
