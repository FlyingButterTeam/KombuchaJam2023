using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExploringState : KombuchaGameState
{
    public override void InitiateState()
    {
        ActionMapFunctions.EvaluateAndSwitchActionMap(stateManager.ActionsAsset.MapControls,
            stateManager.MyPlayerInput, stateManager.ActionsAsset);
    }

    public MapExploringState(GameStateManager manager) : base(manager)
    {

    }
}
