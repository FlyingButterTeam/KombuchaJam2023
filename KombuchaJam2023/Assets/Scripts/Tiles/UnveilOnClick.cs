using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    PlayerActionsAsset _playerActions;
    PlayerActionsAsset PlayerActions
    {
        get
        {
            if (_playerActions == null)
                _playerActions = GameStateManager.instance.ActionsAsset;

            return _playerActions;
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

    PlayerInput _myPlayerInput;
    PlayerInput MyPlayerInput
    {
        get
        {
            if (_myPlayerInput == null)
                _myPlayerInput = GameStateManager.instance.MyPlayerInput;

            return _myPlayerInput;
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

        if(PlayerActions.MapControls.Click.WasPressedThisFrame() 
            && MyPlayerInput.currentActionMap.name == "MapControls")
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
