using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile2D : TileBase
{
    Vector2 _position;
    public Vector2 Position 
    {   
        get { return _position; } 
        protected set { _position = value; } 
    }

    public override string Name { get { return new string(Position.x + "x" + Position.y); } }


    public Vector2 ObtainRelativePosition(GridDirections.Directions direction)
    {
        if (IsDirectionValid(direction))
            return GridDirections.RelativePosition(Position, direction);

        // If the direction is not valid.
        Debug.LogError("It is impossible to calculate the " + direction + " for a tile of these characteristics.");
        return Position;
    }


    // This method is used to know whether a direction is valid for the tile. (Ex. Hex_directions are not valid for Square Tiles.)
    protected abstract bool IsDirectionValid(GridDirections.Directions direction);
}
