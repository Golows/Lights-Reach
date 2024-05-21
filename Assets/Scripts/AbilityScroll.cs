using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityScroll : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private bool canCollect = false;
    private UpgradeManager upgradeManager;
    private PlayerInput playerInput;

    public GameObject arrow;

    private void Start()
    {
        upgradeManager = GameController.instance.upgradeManager;
        playerInput = GameController.instance.character.GetComponent<PlayerMovement>().playerInput;
        playerInput.actions.FindAction("Interact").performed += Interact;

        arrow = Instantiate(arrow, GameController.instance.character.transform.position, Quaternion.identity);
        arrow.GetComponent<ArrowPointer>().targetPos = transform.position;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (canCollect && !GameController.instance.upgradeManager.pickingPowers)
        {
            Time.timeScale = 0f;
            upgradeManager.GetAbilities();
            Destroy(arrow);
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if(playerInput != null)
            playerInput.actions.FindAction("Interact").performed -= Interact;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.SetActive(true);
        TextMeshPro textMesh = text.transform.GetChild(0).GetComponent<TextMeshPro>();
        textMesh.text = "Press " + playerInput.currentActionMap.FindAction("Interact").GetBindingDisplayString(0) + " to collect";
        canCollect = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        canCollect = false;
    }
}
