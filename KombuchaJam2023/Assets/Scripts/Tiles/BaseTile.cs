using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public Vector2 position;

    public string Name { get { return new string(position.x + "x" + position.y); } }


    protected Vector2 ObtainRelativePosition(GridDirections.Directions direction)
    {
        if (IsDirectionValid(direction))
            return GridDirections.RelativePosition(position, direction);

        // If the direction is not valid.
        Debug.LogError("It is impossible to calculate the " + direction + " for a tile of these characteristics.");
        return position;
    }


    // This method is used to know whether a direction is valid for the tile.
    protected bool IsDirectionValid(GridDirections.Directions direction)
    {
        switch (direction)
        {
            case GridDirections.Directions.right: return true;
            case GridDirections.Directions.left: return true;
            case GridDirections.Directions.up: return true;
            case GridDirections.Directions.down: return true;
            case GridDirections.Directions.diagonal_right_up: return true;
            case GridDirections.Directions.diagonal_right_down: return true;
            case GridDirections.Directions.diagonal_left_up: return true;
            case GridDirections.Directions.diagonal_left_down: return true;

            default: return false;
        }
    }
}
