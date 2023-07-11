using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTile
{
    Vector2 _position;
    public Vector2 Position 
    {   
        get { return _position; } 
        protected set { _position = value; } 
    }

    public string Name { get { return new string(Position.x + "x" + Position.y); } }


    public Vector2 ObtainRelativePosition(GridDirections.Directions direction)
    {
        if (IsDirectionValid(direction))
            return GridDirections.RelativePosition(Position, direction);

        // If the direction is not valid.
        Debug.LogError("It is impossible to calculate the " + direction + " for a tile of these characteristics.");
        return Position;
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


    public SquareTile(Vector2 position)
    {
        Position = position;
    }
}
