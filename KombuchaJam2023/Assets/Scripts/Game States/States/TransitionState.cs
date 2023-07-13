using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : KombuchaGameState
{
    public override void InitiateState()
    {
        ActionMapFunctions.EvaluateAndSwitchActionMap(stateManager.ActionsAsset.Transition,
            stateManager.MyPlayerInput, stateManager.ActionsAsset);
    }

    public TransitionState(GameStateManager manager) : base(manager)
    {

    }
}
