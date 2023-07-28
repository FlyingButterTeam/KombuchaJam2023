using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
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

    GameStateManager _myStateManager;
    GameStateManager MyStateManager
    {
        get
        {
            if (_myStateManager == null)
                _myStateManager = GameStateManager.instance;

            return _myStateManager;
        }
    }

    DialogueMapTransitionManager _myDialogueMapTransitionManager;
    DialogueMapTransitionManager MyDialogueMapTransitionManager
    {
        get
        {
            if (_myDialogueMapTransitionManager == null)
                _myDialogueMapTransitionManager = DialogueMapTransitionManager.instance;

            return _myDialogueMapTransitionManager;
        }
    }

    PauseButton _myPauseButton;
    PauseButton MyPauseButton
    {
        get
        {
            if (_myPauseButton == null)
                _myPauseButton = PauseButton.instance;

            return _myPauseButton;
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

        if (activeButtonShadowCoroutine != null)
            highlight.SetActive(false);
        else
            highlight.SetActive(true);
    }


    public void ClickMapButton()
    {
        if (MyStateManager.MyStateType != GameStateManager.StateMachineMode.exploreMap
            && MyStateManager.MyStateType != GameStateManager.StateMachineMode.pointAndClick)
        {
            return;
        }

        if (MyPauseButton.isGamePaused)
            return;

        PressButtonAnimation();
        MyDialogueMapTransitionManager.PressMapButton();
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
    const float pressInDuration = 0.05f;
    const float pressOutDuration = 0.2f;
    Color pressImageColor = new Color(0.7f, 0.7f, 0.7f);

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
