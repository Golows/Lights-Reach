using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private PlayerInput playerInput;
    private bool canInteract = false;
    [SerializeField] private SpriteRenderer spriteRenderer;


    private void Start()
    {
        playerInput = GameController.instance.character.GetComponent<PlayerMovement>().playerInput;
        playerInput.actions.FindAction("Interact").performed += Interact;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(canInteract && !GameController.instance.uiManager.pogressUIOpen)
        {
            GameController.instance.uiManager.OpenProgressUi();
        }
    }

    public void Update()
    {
        if(GameController.instance.character.transform.position.y < transform.position.y && spriteRenderer.sortingOrder != 1)
        {
            spriteRenderer.sortingOrder = 1;
        }
        else if(spriteRenderer.sortingOrder != 10 && GameController.instance.character.transform.position.y > transform.position.y)
        {
            spriteRenderer.sortingOrder = 10;
        }
    }

    private void OnDestroy()
    {
        if (playerInput != null)
            playerInput.actions.FindAction("Interact").performed -= Interact;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.SetActive(true);
        TextMeshPro textMesh = text.transform.GetChild(0).GetComponent<TextMeshPro>();
        textMesh.text = "Press " + playerInput.currentActionMap.FindAction("Interact").GetBindingDisplayString(0) + " to check";
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        canInteract = false;
    }
}
