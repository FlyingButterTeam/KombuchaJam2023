using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapTile))]
public class SetTileVisibilityOnStart : MonoBehaviour
{
    [SerializeField] MapTile.TileVisibility setVisibility = MapTile.TileVisibility.hidden;

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


    private void Awake()
    {
        // We set the hidden Visibility before the rest to ensure proper progation of visibility.
        if (setVisibility == MapTile.TileVisibility.hidden)
            MyMapTile.Visibility = setVisibility;

        if (setVisibility == MapTile.TileVisibility.undertermined)
            Debug.LogError("No tile should not be set as undetermined. " +
                "Change the visibility of tile (" + MyMapTile.position + ").");
    }

    private void Start()
    {
        if(setVisibility != MapTile.TileVisibility.hidden)
            MyMapTile.Visibility = setVisibility;
    }
}
