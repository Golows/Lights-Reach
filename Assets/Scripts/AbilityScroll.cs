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

    private void Start()
    {
        upgradeManager = GameController.instance.upgradeManager;
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnInteract()
    {
        if(canCollect)
        {
            Time.timeScale = 0f;
            upgradeManager.GetAbilities();
            Destroy(gameObject);
        }
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
