using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCd : MonoBehaviour
{

    private float currentCd = 0;
    private PlayerMovement playerMovement;
    [SerializeField] private Image circle;
    [SerializeField] private Image icon;

    private void Start()
    {
        playerMovement = GameController.instance.character.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if(playerMovement.dashOnCd && currentCd < GameController.instance.playerCharacter.dashCooldown)
        {
            currentCd += Time.deltaTime;
            circle.fillAmount = currentCd / GameController.instance.playerCharacter.dashCooldown;
            icon.enabled = false;
        }
        else if(!playerMovement.dashOnCd && currentCd != 0)
        {
            icon.enabled = true;
            currentCd = 0;
        }
    }
}
