using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region Static Reference

    public static GameStateManager instance;

    void InitializeStaticReference()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }

        Debug.LogError("Duplicated static reference. There are multiple instances of GameStateManager. " +
            "Destroying this instance.");
        Destroy(this.gameObject);
    }

    #endregion




    private void Awake()
    {
        InitializeStaticReference();
    }
}
