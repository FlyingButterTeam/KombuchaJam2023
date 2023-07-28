using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : StateMachine
{
    #region Static Reference

    public static GameStateManager instance;

    void InitializeStaticReference()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }

        Debug.LogError("Duplicated static reference. There are multiple instances of GameStateManager. " +
            "Destroying this instance.");
        Destroy(this.gameObject);
    }

    #endregion

    #region Self-Initiated References

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

    [SerializeField] GameObject mapButtonAndInventoryInterface;

    // Undertermined StateType should only be used as a default type for the GameStateManager.
    public enum StateMachineMode { undertermined, pointAndClick, inDialogue, exploreMap, transition }
    StateMachineMode _myStateType = GameStateManager.StateMachineMode.undertermined;
    public StateMachineMode MyStateType
    {
        get { return _myStateType; }
        set
        {
            if (_myStateType == value)
                return;

            if (value == StateMachineMode.undertermined)
            {
                Debug.LogError("Undertermined is exclusively the default type for the GameStateManager." +
                    "\nGameStateManager StateType cannot be set to default.");
                return;
            }

            switch (value)
            {
                case StateMachineMode.pointAndClick:
                    State = new PointAndClickState(this);
                    break;

                case StateMachineMode.exploreMap:
                    State = new MapExploringState(this);
                    break;

                case StateMachineMode.inDialogue:
                    State = new DialogueState(this);
                    break;

                case StateMachineMode.transition:
                    State = new TransitionState(this);
                    break;

                default:
                    Debug.LogError("StateMachineMode not yet implemented in GameStateManager.");
                    break;
            }

            _myStateType = value;
        }
    }


    private void Awake()
    {
        InitializeStaticReference();

        // DEBUG CODE
        MyStateType = StateMachineMode.inDialogue;
    }

    public void SetActiveMapButtonAndInventoryInterface(bool isActive)
    {
        mapButtonAndInventoryInterface.SetActive(isActive);
    }
    public void SetActiveClickableObjectsInScene(bool isActive)
    {
        MyDialogueSceneManager.SetActiveClickableObjectsOnActiveScene(isActive);
    }

    public void EndDialogue()
    {
        if (MyStateType != StateMachineMode.inDialogue)
        {
            Debug.LogWarning("Trying to End Dialogue when we are not currently in Dialogue Mode.");
            return;
        }

        MyStateType = StateMachineMode.pointAndClick;
    }


    #region State Change Transition

    public void ChangeStateWithTransition(StateMachineMode stateToTransitionInto, float transitionDuration)
    {
        if (stateToTransitionInto == StateMachineMode.undertermined
            || stateToTransitionInto == StateMachineMode.transition)
        {
            Debug.LogError("Cannot transition into state " + stateToTransitionInto + ".");
            return;
        }

        StopTransitionCoroutine();

        activeTransitionCoroutine = StartCoroutine(StateTranstion(stateToTransitionInto, transitionDuration));
    }

    Coroutine activeTransitionCoroutine;

    void StopTransitionCoroutine()
    {
        if (activeTransitionCoroutine == null)
            return;

        StopCoroutine(activeTransitionCoroutine);
        activeTransitionCoroutine = null;
    }

    IEnumerator StateTranstion(StateMachineMode stateToTransitionInto, float transitionDuration)
    {
        MyStateType = StateMachineMode.transition;

        yield return new WaitForSeconds(transitionDuration);

        MyStateType = stateToTransitionInto;

        activeTransitionCoroutine = null;
    }

    #endregion
}
