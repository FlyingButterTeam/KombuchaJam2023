using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScene : MonoBehaviour
{
    [SerializeField] Vector2 position = Vector2.zero;


    private void OnEnable()
    {
        
    }


#if UNITY_EDITOR

    Vector2 oldPosition = Vector2.zero;

    private void OnDrawGizmosSelected()
    {
        if (oldPosition != position)
        {
            SetSceneName(position);

            oldPosition = position;
        }
    }

    void SetSceneName(Vector2 newPosition)
    {
        gameObject.name = new string("Scene_" + newPosition.x + "x" + newPosition.y);
    }

#endif
}
