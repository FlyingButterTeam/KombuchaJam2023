using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickState : KombuchaGameState
{
    public override void InitiateState()
    {
        ActionMapFunctions.EvaluateAndSwitchActionMap(stateManager.ActionsAsset.PointAndClick,
            stateManager.MyPlayerInput, stateManager.ActionsAsset);
    }

    public PointAndClickState(GameStateManager manager) : base(manager)
    {

    }
}
