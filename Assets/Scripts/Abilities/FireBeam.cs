using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    private string playerTag = "Player";
    private bool inRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            inRange = true;
            StartCoroutine(KeepTakingDamage());

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            inRange = false;
            StopCoroutine(KeepTakingDamage());
        }
    }

    IEnumerator KeepTakingDamage()
    {
        while (inRange)
        {
            GameController.instance.playerCharacter.TakeDamage(20f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
