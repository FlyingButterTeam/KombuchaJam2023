using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KombuchaGameState : StateBase
{
    protected GameStateManager stateManager;

    public KombuchaGameState(GameStateManager manager)
    {
        stateManager = manager;
    }
}
