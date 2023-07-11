using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class MapTile : BaseTile
{
    [Header("Tile GameObjects")]
    [SerializeField] GameObject foggedTile;
    [SerializeField] GameObject regularTile;
    
    
    #region Self-Initiated Properties

    Grid _myGrid;
    Grid MyGrid
    {
        get
        {
            if (_myGrid == null)
            {
                if (transform.parent.TryGetComponent<Grid>(out Grid grid))
                    _myGrid = grid;
                else
                    Debug.LogError("Grid Class could not be found in the parent GameObject to " + gameObject.name
                        + ". The Grid class should be placed as a parent of all MapTiles.");
            }

            return _myGrid;
        }
    }

    #endregion

    public enum TileVisibility { visible, fogged, hidden }

    [SerializeField] TileVisibility visibility = TileVisibility.hidden;

    public TileVisibility Visibility 
    { 
        get { return Visibility; } 
    
        set { ChangeVisibility(value); }
    }


    void ChangeVisibility(TileVisibility newVisibility)
    {
        if (visibility == newVisibility)
            return;

        // Revealing adjacent tiles as fogged if we just revealed this one.
        if (newVisibility == TileVisibility.visible)
        {
            MapTile[] adjacentTiles = MyGrid.ObtainAllTilesAdjacentToPosition(position);

            if(adjacentTiles.Length != 0)
            {
                foreach (var tile in adjacentTiles)
                {
                    if(tile.visibility == TileVisibility.hidden)
                        tile.Visibility = TileVisibility.fogged;
                }
            }
        }


        // Revealing this tile.
        switch (newVisibility)
        {
            case TileVisibility.hidden:
                break;

            case TileVisibility.fogged:
                foggedTile.SetActive(true);
                regularTile.SetActive(false);
                break;

            case TileVisibility.visible:
                foggedTile.SetActive(false);
                regularTile.SetActive(true);
                break;
        }
        

        visibility = newVisibility;
    }

    MapTile ObtainTileInDirection(GridDirections.Directions direction)
    {
        if (!IsDirectionValid(direction))
        {
            Debug.LogError("Direction " + direction + " is not valid. Cannot calculate relative position.");
            return null;
        }

        Vector2 newTilePosition = ObtainRelativePosition(direction);

        return MyGrid.ObtainMapTileAtPosition(newTilePosition);
    }
}
