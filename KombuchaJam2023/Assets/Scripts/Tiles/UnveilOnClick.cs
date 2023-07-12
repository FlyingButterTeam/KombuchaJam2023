using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    #endregion

    private void Update()
    {
        if (MyMapTile.Visibility != MapTile.TileVisibility.hidden)
            return;


    }
}
