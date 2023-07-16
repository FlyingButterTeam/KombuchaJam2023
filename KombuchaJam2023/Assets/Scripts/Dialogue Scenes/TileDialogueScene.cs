using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Flowchart))]
public class TileDialogueScene : MonoBehaviour
{
    #region Self-Initiated Properties

    Flowchart _myFlowchart;
    Flowchart MyFlowchart
    {
        get
        {
            if(_myFlowchart == null)
                _myFlowchart = GetComponent<Flowchart>();

            return _myFlowchart;
        }
    }

    #endregion

    public Vector2 position = Vector2.zero;

    [SerializeField] GameObject clickableObjects;

    const string blockToExecuteName = "PlayScene";


    public void OpenTile()
    {
        if (MyFlowchart.HasBlock(blockToExecuteName))
            MyFlowchart.ExecuteBlock(blockToExecuteName);
        else
            Debug.LogError("Could not Begin scene because the Executing Block was not found. " +
                "Remember to name the executing block [" + blockToExecuteName + "].");
    }

    public void SetActiveClickableObjects(bool isActive)
    {
        clickableObjects.SetActive(isActive);
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
