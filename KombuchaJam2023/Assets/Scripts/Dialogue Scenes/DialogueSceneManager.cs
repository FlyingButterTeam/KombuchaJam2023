using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSceneManager : MonoBehaviour
{
    #region Static Reference

    public static DialogueSceneManager instance;

    void InitializeStaticReference()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }

        Debug.LogError("Duplicated static reference. There are multiple instances of DialogueSceneManager. " +
            "Destroying this instance.");
        Destroy(this.gameObject);
    }

    #endregion

    Dictionary<Vector2, TileDialogueScene> myDialogueSceneDictionary = new();

    public Vector2 currentlyActiveTileScene = new Vector2(1, 1);

    private void Awake()
    {
        InitializeStaticReference();
        CollectAllDialogueScenes();
        HideAllButActiveScene();

        // DEBUG:
        gameObject.SetActive(false);
    }


    /// <summary>
    /// Scouts the Children of this gameObject and stores them in the myTileDialogueScenes array.
    /// </summary>
    private void CollectAllDialogueScenes()
    {
        TileDialogueScene[] myDialogueScenes = GetComponentsInChildren<TileDialogueScene>();

        foreach (TileDialogueScene dialogueScene in myDialogueScenes)
        {
            myDialogueSceneDictionary.Add(dialogueScene.position, dialogueScene);
        }
    }

    private void HideAllButActiveScene()
    {
        foreach(TileDialogueScene scene in myDialogueSceneDictionary.Values)
        {
            if(scene.position != currentlyActiveTileScene)
                scene.gameObject.SetActive(false);
        }
    }

    public void ActivateTileScene(Vector2 position)
    {
        if (currentlyActiveTileScene == position)
            return;

        if (!myDialogueSceneDictionary.ContainsKey(position))
        {
            Debug.LogError("DialogueSceneManager does not contain a scene with a position ("
                + position.x + "," + position.y + ").");
            return;
        }

        myDialogueSceneDictionary[currentlyActiveTileScene].gameObject.SetActive(false);
        myDialogueSceneDictionary[position].gameObject.SetActive(true);

        currentlyActiveTileScene = position;
        myDialogueSceneDictionary[position].OpenTile();
    }

    public void SetActiveClickableObjectsOnActiveScene(bool isActive)
    {
        myDialogueSceneDictionary[currentlyActiveTileScene].SetActiveClickableObjects(isActive);
    }
}
