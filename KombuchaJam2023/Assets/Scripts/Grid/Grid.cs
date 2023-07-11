using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Dictionary<Vector2, MapTile> myTileDictionary = new();
    
    private void Start()
    {
        CollectAllMapTiles();
    }

    /// <summary>
    /// Scouts the Children of this gameObject and stores them in the myMapTiles array.
    /// </summary>
    private void CollectAllMapTiles()
    {
        MapTile[] myTiles = GetComponentsInChildren<MapTile>();

        foreach (MapTile tile in myTiles)
        {
            myTileDictionary.Add(tile.position, tile);
        }
    }


    public MapTile ObtainMapTileAtPosition(Vector2 position)
    {
        if (!DoesTileExistAtPosition(position))
        {
            Debug.LogError("There is no tile at position (" + position.x + ";" + position.y + ").");
            return null;
        }

        return myTileDictionary[position];
    }

    /// <summary>
    /// Returns an array with all Tiles adjacent to the desired location.
    /// </summary>
    /// <param name="position">The position around which we want to check for other tiles.</param>
    /// <returns></returns>
    public MapTile[] ObtainAllTilesAdjacentToPosition(Vector2 position)
    {
        if(!DoesTileExistAtPosition(position))
        {
            Debug.LogWarning("There is no Tile in position (" + position.x + ";" + position.y + ") "
                + "yet you are trying to obtain its adjacent tiles.");
        }

        GridDirections.Directions[] cardinalDirections = new GridDirections.Directions[] 
            { GridDirections.Directions.down, GridDirections.Directions.up,
              GridDirections.Directions.left, GridDirections.Directions.right };

        List<MapTile> resultTiles = new List<MapTile>();

        foreach(GridDirections.Directions direction in cardinalDirections)
        {
            Vector2 newPossibleTilePosition = GridDirections.RelativePosition(position, direction);

            if (!DoesTileExistAtPosition(newPossibleTilePosition))
                continue;

            resultTiles.Add(myTileDictionary[newPossibleTilePosition]);
        }

        return resultTiles.ToArray();
    }

    public bool DoesTileExistAtPosition(Vector2 position)
    {
        if (myTileDictionary.Keys.Contains<Vector2>(position))
            return true;
        
        // Else
        return false;
    }
}
