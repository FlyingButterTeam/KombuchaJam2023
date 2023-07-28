using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnveilOnClick : MonoBehaviour
{
    #region Self-Initiated Properties

    MapTile _myMapTile;
    MapTile MyMapTile
    {
        get
        {
            if (_myMapTile == null)
                _myMapTile = GetComponent<MapTile>();

            return _myMapTile;
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

    #endregion

    [SerializeField] GameObject selectedFrame;

    private void Update()
    {
        if (!(MyMapTile.Visibility == MapTile.TileVisibility.fogged 
              || MyMapTile.Visibility == MapTile.TileVisibility.visible))
            return;

        if (!isMouseOverTile)
        {
            selectedFrame.SetActive(false);
            return;
        }
            
        // Else
        selectedFrame.SetActive(true);

        if(Input.GetKeyDown(KeyCode.Mouse0) && 
            (MyStateManager.MyStateType == GameStateManager.StateMachineMode.exploreMap ||
             MyStateManager.MyStateType == GameStateManager.StateMachineMode.pointAndClick))
        {
            MyMapTile.Visibility = MapTile.TileVisibility.visible;

            isMouseOverTile = false;
            selectedFrame.SetActive(false);

            MyDialogueMapTransitionManager.CloseMapOpenDialogue(MyMapTile.position);
        }
    }

    bool isMouseOverTile = false;

    private void OnMouseOver() => isMouseOverTile = true;
    private void OnMouseExit() => isMouseOverTile = false;

    private void OnDisable()
    {
        selectedFrame.SetActive(false);
        isMouseOverTile = false;
    }
}
