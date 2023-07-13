using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    PlayerInput _myPlayerInput;
    public PlayerInput MyPlayerInput
    {
        get
        {
            if(_myPlayerInput == null)
            {
                if (TryGetComponent<PlayerInput>(out PlayerInput input))
                    _myPlayerInput = input;
                else
                    Debug.LogError("Could not find a PlayerInput Component attached to the same GameObject " +
                        "as the GameStateManager.");
            }

            return _myPlayerInput;
        }
    }

    PlayerActionsAsset _actionsAsset;
    public PlayerActionsAsset ActionsAsset
    {
        get
        {
            if(_actionsAsset == null)
            {
                _actionsAsset = new PlayerActionsAsset();
                _actionsAsset.PointAndClick.Enable();
            }
            
            return _actionsAsset;
        }
    }

    #endregion

    // Undertermined StateType should only be used as a default type for the GameStateManager.
    public enum StateMachineMode { undertermined, pointAndClick, exploreMap, transition }
    StateMachineMode _myStateType = GameStateManager.StateMachineMode.undertermined;
    public StateMachineMode MyStateType
    {
        get { return _myStateType;}
        set
        {
            if (_myStateType == value)
                return;

            if(value == StateMachineMode.undertermined)
            {
                Debug.LogError("Undertermined is exclusively the default type for the GameStateManager." +
                    "\nGameStateManager StateType cannot be set to default.");
                return;
            }

            switch(value)
            {
                case StateMachineMode.pointAndClick:
                    State = new PointAndClickState(this);
                    break;

                case StateMachineMode.exploreMap:
                    State = new MapExploringState(this);
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
        MyStateType = StateMachineMode.exploreMap;
    }
}
