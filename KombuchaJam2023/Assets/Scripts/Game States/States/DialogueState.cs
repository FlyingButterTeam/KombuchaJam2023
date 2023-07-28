using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueState : KombuchaGameState
{
    public override void InitiateState()
    {
        stateManager.SetActiveMapButtonAndInventoryInterface(false);
        stateManager.SetActiveClickableObjectsInScene(false);
    }

    public override void EndState()
    {
        stateManager.SetActiveMapButtonAndInventoryInterface(true);
        stateManager.SetActiveClickableObjectsInScene(true);
    }


    public DialogueState(GameStateManager manager) : base(manager)
    {
    }
}
