using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using static GameStateManager;

public class MapButton : MonoBehaviour
{
    #region Self-Initialized References

    Image _myImage;
    Image MyImage
    {
        get
        {
            if(_myImage == null)
                _myImage = GetComponent<Image>();

            return _myImage;
        }
    }

    PlayerActionsAsset _playerActions;
    PlayerActionsAsset PlayerActions
    {
        get
        {
            if (_playerActions == null)
                _playerActions = GameStateManager.instance.ActionsAsset;

            return _playerActions;
        }
    }

    #endregion

    [Header("References")]
    [SerializeField] GameObject highlight;


    private void Update()
    {
        if (!MouseOver)
        {
            highlight.SetActive(false);
            return;
        }


        if (!buttonClicked)
        {
            if (activeButtonShadowCoroutine != null)
                highlight.SetActive(false);
            else
                highlight.SetActive(true);

            return;
        }

        // Button Clicked Logic
        buttonClicked = false;

    }

    bool buttonClicked = false;

    public void ClickMapButton()
    {
        PressButtonAnimation();
        buttonClicked = true;
    }


    bool _mouseOver = false;
    bool MouseOver
    {
        get { return _mouseOver; }
        set
        {
            if(_mouseOver == value)
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


    #region Button Shadow when Clicked

    Coroutine activeButtonShadowCoroutine;
    const float pressInDuration = 0.1f;
    const float pressOutDuration = 0.3f;
    Color pressImageColor = new Color(0.8f, 0.8f, 0.8f);

    void PressButtonAnimation()
    {
        StopButtonShadowCoroutine();

        activeButtonShadowCoroutine = StartCoroutine(ButtonPressedAnimation());
    }

    void StopButtonShadowCoroutine()
    {
        if (activeButtonShadowCoroutine == null)
            return;

        StopCoroutine(activeButtonShadowCoroutine);
        activeButtonShadowCoroutine = null;

        MyImage.color = Color.white;
    }

    IEnumerator ButtonPressedAnimation()
    {
        // Press IN
        Timer animationTimer = new Timer(pressInDuration);

        while (!animationTimer.IsComplete)
        {
            animationTimer.Update();
            MyImage.color = Color.Lerp(Color.white, pressImageColor, animationTimer.PercentageComplete);

            yield return null;
        }

        // Press OUT
        animationTimer = new Timer(pressOutDuration, pressOutDuration);

        while (animationTimer.Time != 0)
        {
            animationTimer.NegativeUpdate();
            MyImage.color = Color.Lerp(Color.white, pressImageColor, animationTimer.PercentageComplete);

            yield return null;
        }

        activeButtonShadowCoroutine = null;
    }

    #endregion
}
