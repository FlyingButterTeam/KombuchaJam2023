using UnityEngine;
using UnityEngine.InputSystem;

public static class ActionMapActions
{
    public static void EvaluateAndSwitchActionMap(InputActionMap newActionMap, PlayerInput playerInput, 
                                    PlayerActionsAsset actionsAsset)
    {
        if (newActionMap == playerInput.currentActionMap)
            return;
        
        ReallyDisableCurrentActionMap(playerInput, actionsAsset);

        
        playerInput.SwitchCurrentActionMap(newActionMap.name);
        
        switch(newActionMap.name)
        {
            case "RegularPointAndClick":
                {
                    actionsAsset.RegularPointAndClick.Enable();
                    break;
                }
            case "MapControls":
                {
                    actionsAsset.MapControls.Enable();
                    break;
                }
            case "Transition":
                {
                    actionsAsset.Transition.Enable();
                    break;
                }
            default:
                {
                    Debug.LogError("Action map " + newActionMap.name
                        + "is not accounted for in ReallyDisableCurrentActionMap().");
                    break;
                }


        }
    }


    // This function disables all actions inside the current action map.
    // Apparently the switching option does not work.
    static void ReallyDisableCurrentActionMap(PlayerInput playerInput, PlayerActionsAsset actionsAsset)
    {        
        switch (playerInput.currentActionMap.name)
        {
            case "RegularPointAndClick":
                {
                    actionsAsset.RegularPointAndClick.Disable();
                    break;
                }
            case "MapControls":
                {
                    actionsAsset.MapControls.Disable();
                    break;
                }
            case "Transition":
                {
                    actionsAsset.Transition.Disable();
                    break;
                }
            default:
                {
                    Debug.LogError("Action map " + playerInput.currentActionMap.name
                        + "is not accounted for in ReallyDisableCurrentActionMap().");
                    break;
                }
        }
    }
}
