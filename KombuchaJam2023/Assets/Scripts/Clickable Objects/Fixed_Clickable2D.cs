using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fixed_Clickable2D : Clickable2D
{
    #region Self-Initiated Variables

    GameStateManager _myGameStateManager;
    GameStateManager MyGameStateManager
    {
        get
        {
            if (_myGameStateManager == null)
                _myGameStateManager = GameStateManager.instance;

            return _myGameStateManager;
        }
    }

    #endregion

    protected override void DoPointerClick()
    {
        base.DoPointerClick();

        MyGameStateManager.MyStateType = GameStateManager.StateMachineMode.inDialogue;
    }
}
