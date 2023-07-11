using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnveilOnClick : MonoBehaviour
{
    [SerializeField] bool clicked = false;
    bool wasClicked = false;

    private void Update()
    {
        if (clicked && wasClicked == false)
        {
            GetComponent<MapTile>().Visibility = MapTile.TileVisibility.visible;
            wasClicked = true;
        }
    }
}
