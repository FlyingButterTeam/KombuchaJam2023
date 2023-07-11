using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square2D : Tile2D
{
    protected override bool IsDirectionValid(GridDirections.Directions direction)
    {
        switch (direction)
        {
            case GridDirections.Directions.right: return true;
            case GridDirections.Directions.left : return true;
            case GridDirections.Directions.up : return true;
            case GridDirections.Directions.down : return true;
            case GridDirections.Directions.diagonal_right_up : return true;
            case GridDirections.Directions.diagonal_right_down : return true;
            case GridDirections.Directions.diagonal_left_up : return true;
            case GridDirections.Directions.diagonal_left_down : return true;

            default: return false;
        }
    }

    public Square2D(Vector2 position)
    {
        Position = position;
    }
}
