using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Well : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private PlayerInput playerInput;
    [SerializeField] private LevelLoader levelLoader;
    private bool canStart = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnInteract()
    {
        if (canStart)
        {
            levelLoader.LoadNextScene(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.SetActive(true);
        TextMeshPro textMesh = text.transform.GetChild(0).GetComponent<TextMeshPro>();
        textMesh.text = "Press " + playerInput.currentActionMap.FindAction("Interact").GetBindingDisplayString(0) + " to start";
        canStart = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        canStart = false;
    }
}
