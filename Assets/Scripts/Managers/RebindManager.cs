using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindManager : MonoBehaviour
{
    [SerializeField] private InputActionReference moveRed, dashRef, interactRef;

    private void OnEnable()
    {
        moveRed.action.Disable();
        dashRef.action.Disable();
        interactRef.action.Disable();
    }

    private void OnDisable()
    {
        moveRed.action.Enable();
        dashRef.action.Enable();
        interactRef.action.Enable();

        var rebinds = GameController.instance.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }
}
