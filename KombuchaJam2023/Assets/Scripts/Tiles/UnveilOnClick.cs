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

        if(PlayerActions.MapControls.Click.WasPressedThisFrame())
        {
            MyMapTile.Visibility = MapTile.TileVisibility.visible;

            BlackoutController.instance.ActivateBlackoutAnimation(0.5f, 0.2f, 0.3f);
        }
    }

    bool isMouseOverTile = false;

    private void OnMouseOver() => isMouseOverTile = true;
    private void OnMouseExit() => isMouseOverTile = false;
}
