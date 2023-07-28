using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    #region Static Reference

    public static PauseButton instance;

    void InitializeStaticReference()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }

        Debug.LogError("Duplicated static reference. There are multiple instances of PauseButton. " +
            "Destroying this instance.");
        Destroy(this.gameObject);
    }

    #endregion

    #region Self-Initialized References

    Image _myImage;
    Image MyImage
    {
        get
        {
            if (_myImage == null)
                _myImage = GetComponent<Image>();

            return _myImage;
        }
    }

    #endregion

    [Header("References")]
    [SerializeField] GameObject highlight;
    [SerializeField] GameObject pauseMenu;

    [HideInInspector] public bool isGamePaused = false;

    private void Update()
    {
        if (!MouseOver)
        {
            highlight.SetActive(false);
            return;
        }

        // Else
        highlight.SetActive(true);
    }

    private void Awake()
    {
        InitializeStaticReference();
    }

    public void ClickPauseButton()
    {
        if (isGamePaused)
        {
            Debug.LogError("What the hell man.");
            return;
        }

        isGamePaused = true;
        pauseMenu.SetActive(true);
        MyImage.enabled = false;
    }

    public void ExitPause()
    {
        if (!isGamePaused)
        {
            Debug.LogError("What the hell man.");
            return;
        }

        isGamePaused = false;
        pauseMenu.SetActive(false);
        MyImage.enabled = true;
    }

    public void ExitToMainMenu()
    {
        if (!isGamePaused)
        {
            Debug.LogError("What the hell man.");
            return;
        }

        SceneManager.LoadScene(0); // Main Menu
    }


    bool _mouseOver = false;
    bool MouseOver
    {
        get { return _mouseOver; }
        set
        {
            if (_mouseOver == value)
                return;

            _mouseOver = value;
        }
    }

    public void MouseEnterButton()
    {
        MouseOver = true;
    }

    public void MouseExitButton()
    {
        MouseOver = false;
    }

    private void OnDisable()
    {
        MouseOver = false;
    }
}
