using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMapTransitionManager : MonoBehaviour
{
    #region Static Reference

    public static DialogueMapTransitionManager instance;

    void InitializeStaticReference()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }

        Debug.LogError("Duplicated static reference. There are multiple instances of DialogueMapTransitionManager. " +
            "Destroying this instance.");
        Destroy(this.gameObject);
    }

    #endregion

    #region Self-Initialized References

    GameStateManager _myGameStateManager;
    GameStateManager MyGameStateManager
    {
        get
        {
            if(_myGameStateManager == null)
                _myGameStateManager = GameStateManager.instance;

            return _myGameStateManager;
        }
    }

    BlackoutController _myBlackoutController;
    BlackoutController MyBlackoutController
    {
        get
        {
            if (_myBlackoutController == null)
                _myBlackoutController = BlackoutController.instance;

            return _myBlackoutController;
        }
    }

    DialogueSceneManager _myDialogueSceneManager;
    DialogueSceneManager MyDialogueSceneManager
    {
        get
        {
            if (_myDialogueSceneManager == null)
                _myDialogueSceneManager = DialogueSceneManager.instance;
        
            return _myDialogueSceneManager;
        }
    }

    #endregion

    [Header("Configuration")]
    [SerializeField] [Range(0.1f, 4)] float transitionDuration = 1;
    [SerializeField] [Range(0, 1)] float fadeInPercentageTransition = 0.45f;
    [SerializeField] [Range(0, 1)] float middleWaitPercentageTransition = 0.1f;

    [Header("References")]
    [SerializeField] GameObject mapParentGameobject;
    [SerializeField] GameObject dialogueParentGameobject;
    [SerializeField] AudioSource mapSoundSource;
    [SerializeField] AudioSource footstepsSoundSource;


    bool _areWeCurrentlyInMapMode = false;
    public bool AreWeCurrentlyInMapMode
    {
        get => _areWeCurrentlyInMapMode;
        private set => _areWeCurrentlyInMapMode = value;
    }


    private void Awake()
    {
        InitializeStaticReference();
    }

    public void PressMapButton()
    {
        if (AreWeCurrentlyInMapMode)
            CloseMapOpenDialogue();
        else
            CloseDialogueOpenMap();

        mapSoundSource.Play();
    }

    void CloseDialogueOpenMap()
    {
        if (AreWeCurrentlyInMapMode)
        {
            Debug.LogError("Trying to close dialogue but we are in Map Mode.");
            return;
        }
        if(fadeInPercentageTransition + middleWaitPercentageTransition >= 1)
        {
            Debug.LogWarning("The percentage of the transition allocated to the Fade In (" + 
                fadeInPercentageTransition + ") and Middle Wait (" + middleWaitPercentageTransition + ") " +
                "of a Blackout cannot equal or exceed 100%. Setting the Middle Wait % to 0.");
            middleWaitPercentageTransition = 0;
        }

        MyGameStateManager.ChangeStateWithTransition
            (GameStateManager.StateMachineMode.exploreMap, transitionDuration);

        MyBlackoutController.ActivateBlackoutAnimation
            (transitionDuration * fadeInPercentageTransition,
             transitionDuration * middleWaitPercentageTransition,
             transitionDuration * (1 - fadeInPercentageTransition - middleWaitPercentageTransition));

        // We swap when we are in the middle of our Middle Wait for an excellent transition.
        WaitAndToggleMapDialogue(transitionDuration
            * (fadeInPercentageTransition + middleWaitPercentageTransition * 0.5f));
    }

    void CloseMapOpenDialogue()
    {
        if (!AreWeCurrentlyInMapMode)
        {
            Debug.LogError("Trying to close map but we are in Dialogue Mode.");
            return;
        }
        if (fadeInPercentageTransition + middleWaitPercentageTransition >= 1)
        {
            Debug.LogWarning("The percentage of the transition allocated to the Fade In (" +
                fadeInPercentageTransition + ") and Middle Wait (" + middleWaitPercentageTransition + ") " +
                "of a Blackout cannot equal or exceed 100%. Setting the Middle Wait % to 0.");
            middleWaitPercentageTransition = 0;
        }

        MyGameStateManager.ChangeStateWithTransition
            (GameStateManager.StateMachineMode.pointAndClick, transitionDuration);

        MyBlackoutController.ActivateBlackoutAnimation
            (transitionDuration * fadeInPercentageTransition,
             transitionDuration * middleWaitPercentageTransition,
             transitionDuration * (1 - fadeInPercentageTransition - middleWaitPercentageTransition));

        // We swap when we are in the middle of our Middle Wait for an excellent transition.
        WaitAndToggleMapDialogue(transitionDuration
            * (fadeInPercentageTransition + middleWaitPercentageTransition * 0.5f));
    }

    public void CloseMapOpenDialogue(Vector2 newTileToExplore)
    {
        if (!AreWeCurrentlyInMapMode)
        {
            Debug.LogError("Trying to close map but we are in Dialogue Mode.");
            return;
        }
        if (fadeInPercentageTransition + middleWaitPercentageTransition >= 1)
        {
            Debug.LogWarning("The percentage of the transition allocated to the Fade In (" +
                fadeInPercentageTransition + ") and Middle Wait (" + middleWaitPercentageTransition + ") " +
                "of a Blackout cannot equal or exceed 100%. Setting the Middle Wait % to 0.");
            middleWaitPercentageTransition = 0;
        }
        if(newTileToExplore == MyDialogueSceneManager.currentlyActiveTileScene)
        {
            CloseMapOpenDialogue();
            mapSoundSource.Play();
            return;
        }

        MyGameStateManager.ChangeStateWithTransition(GameStateManager.StateMachineMode.inDialogue, 
            transitionDuration * (fadeInPercentageTransition + middleWaitPercentageTransition));

        WaitAndActivateTileScene(newTileToExplore, transitionDuration *
            (fadeInPercentageTransition + middleWaitPercentageTransition));

        MyBlackoutController.ActivateBlackoutAnimation
            (transitionDuration * fadeInPercentageTransition,
             transitionDuration * middleWaitPercentageTransition,
             transitionDuration * (1 - fadeInPercentageTransition - middleWaitPercentageTransition));

        // We swap when we are in the middle of our Middle Wait for an excellent transition.
        WaitAndToggleMapDialogue(transitionDuration
            * (fadeInPercentageTransition + middleWaitPercentageTransition * 0.5f));

        footstepsSoundSource.Play();
    }

    #region Toggle Map-Dialogue Coroutine

    Coroutine activeMapDialogueToggleCoroutine = null;

    void WaitAndToggleMapDialogue(float waitForThisLongBeforeToggle)
    {
        StopMapDialogueCoroutine();

        activeMapDialogueToggleCoroutine = StartCoroutine(WaitAndToggle(waitForThisLongBeforeToggle));
    }

    void StopMapDialogueCoroutine()
    {
        if (activeMapDialogueToggleCoroutine == null)
            return;

        Debug.LogWarning("You shouldn't need to cancel any Coroutine that toggles Map and Dialogue." +
            "What are you doing? :(");

        StopCoroutine(activeMapDialogueToggleCoroutine);
        activeMapDialogueToggleCoroutine = null;
    }


    IEnumerator WaitAndToggle(float waitForThisLongBeforeToggle)
    {
        if(waitForThisLongBeforeToggle != 0)
        {
            Timer waitTimer = new Timer(waitForThisLongBeforeToggle);

            while (!waitTimer.IsComplete)
            {
                waitTimer.Update();
                yield return null;
            }
        }

        if (AreWeCurrentlyInMapMode)
        {
            dialogueParentGameobject.SetActive(true);
            mapParentGameobject.SetActive(false);

            AreWeCurrentlyInMapMode = false;
        }
        else
        {
            dialogueParentGameobject.SetActive(false);
            mapParentGameobject.SetActive(true);

            AreWeCurrentlyInMapMode = true;
        }

        activeMapDialogueToggleCoroutine = null;
    }

    #endregion

    #region Activate TileScene After Time Coroutine

    Coroutine activeTileSceneCoroutine = null;

    void WaitAndActivateTileScene(Vector2 newTileToExplore, float waitForThisLongBeforeToggle)
    {
        StopActivateTileSceneCoroutine();

        activeTileSceneCoroutine = 
            StartCoroutine(WaitAndActivateScene(newTileToExplore, waitForThisLongBeforeToggle));
    }

    void StopActivateTileSceneCoroutine()
    {
        if (activeTileSceneCoroutine == null)
            return;

        Debug.LogWarning("You shouldn't need to cancel any Coroutine that activates a Tile Scene." +
            "What are you doing? :(");

        StopCoroutine(activeTileSceneCoroutine);
        activeTileSceneCoroutine = null;
    }


    IEnumerator WaitAndActivateScene(Vector2 newTileToExplore, float waitForThisLongBeforeToggle)
    {
        if (waitForThisLongBeforeToggle == 0)
        {
            Debug.LogWarning("WaitAndActivateScene Coroutine should have a duration bigger than 0 to avoid " +
                "unexpected behaviors.");
        }
        else
        {
            Timer waitTimer = new Timer(waitForThisLongBeforeToggle);

            while (!waitTimer.IsComplete)
            {
                waitTimer.Update();
                yield return null;
            }
        }

        MyDialogueSceneManager.ActivateTileScene(newTileToExplore);

        activeTileSceneCoroutine = null;
    }

    #endregion
}
