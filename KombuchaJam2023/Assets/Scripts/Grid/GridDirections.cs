using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GridDirections
{
    [Serializable]
    public enum Directions
    {
        right,                  left,
        up,                     down,
        hex_right_up,           hex_left_up,
        hex_right_down,         hex_left_down,
        above,                  below,
        diagonal_right_up,      diagonal_left_up,
        diagonal_right_down,    diagonal_left_down
    }


    /// <summary>
    /// Returns the 2D Grid position of the tile in the direction provided relative to another position.
    /// </summary>
    /// <param name="originalPosition">The original position.</param>
    /// <param name="direction">The 2D direction of the desired position.</param>
    /// <returns></returns>
    public static Vector2 RelativePosition(Vector2 originalPosition, Directions direction)
    {
        switch (direction)
        {
            case Directions.right:
                return originalPosition + new Vector2(1, 0);
                
            case Directions.left:
                return originalPosition + new Vector2(-1, 0);

            case Directions.up:
                return originalPosition + new Vector2(0, 1);

            case Directions.down:
                return originalPosition + new Vector2(0, -1);

            case Directions.hex_right_up:
                return originalPosition + new Vector2(0.5f, 0.5f);

            case Directions.hex_left_up:
                return originalPosition + new Vector2(-0.5f, 0.5f);

            case Directions.hex_right_down:
                return originalPosition + new Vector2(0.5f, -0.5f);

            case Directions.hex_left_down:
                return originalPosition + new Vector2(-0.5f, -0.5f);

            case Directions.diagonal_right_up:
                return originalPosition + new Vector2(1, 1);

            case Directions.diagonal_left_up:
                return originalPosition + new Vector2(-1, 1);

            case Directions.diagonal_right_down:
                return originalPosition + new Vector2(1, -1);

            case Directions.diagonal_left_down:
                return originalPosition + new Vector2(-1, -1);


            default:
                Debug.LogError("Direction " + direction + " not recognized. It might not be implemented for 2D directions.");
                return originalPosition;
        }
    }


    /// <summary>
    /// Returns the 3D Grid position of the tile in the direction provided relative to another position.
    /// </summary>
    /// <param name="originalPosition">The original position.</param>
    /// <param name="direction">The 3D direction of the desired position.</param>
    /// <returns></returns>
    public static Vector3 RelativePosition(Vector3 originalPosition, Directions direction)
    {
        switch (direction)
        {
            case Directions.right:
                return originalPosition + new Vector3(1, 0, 0);

            case Directions.left:
                return originalPosition + new Vector3(-1, 0, 0);

            case Directions.up:
                return originalPosition + new Vector3(0, 1, 0);

            case Directions.down:
                return originalPosition + new Vector3(0, -1, 0);

            case Directions.hex_right_up:
                return originalPosition + new Vector3(0.5f, 0.5f, 0);

            case Directions.hex_left_up:
                return originalPosition + new Vector3(-0.5f, 0.5f, 0);

            case Directions.hex_right_down:
                return originalPosition + new Vector3(0.5f, -0.5f, 0);

            case Directions.hex_left_down:
                return originalPosition + new Vector3(-0.5f, -0.5f, 0);

            case Directions.above:
                return originalPosition + new Vector3(0, 0, 1);

            case Directions.below:
                return originalPosition + new Vector3(0, 0, -1);

            case Directions.diagonal_right_up:
                return originalPosition + new Vector3(1, 1, 0);

            case Directions.diagonal_left_up:
                return originalPosition + new Vector3(-1, 1, 0);

            case Directions.diagonal_right_down:
                return originalPosition + new Vector3(1, -1, 0);

            case Directions.diagonal_left_down:
                return originalPosition + new Vector3(-1, -1, 0);


            default:
                Debug.LogError("Direction " + direction + " not recognized. It might not be implemented for 3D directions.");
                return originalPosition;
        }
    }
}
