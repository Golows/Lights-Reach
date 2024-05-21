using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerTest
{
    [UnityTest]
    public IEnumerator DashToTheRight()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1f);
        
        var characater = GameController.instance.character;

        var playerMovement = characater.GetComponent<PlayerMovement>();
        var playerCharacter = characater.GetComponent<PlayerCharacter>();

        float distance1 = 0;
        float distance2 = 0;
        playerCharacter.dashCooldown = 0f;
        
        for(int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(0.5f);
            playerMovement.PerformDash();
            yield return new WaitForSeconds(playerCharacter.dashTime);
            Debug.Log(characater.transform.position);
            Debug.Log((2.7f + distance1) + " " + (3.3f + distance2));
            Assert.IsTrue(characater.transform.position.x > (2.7f + distance1) && characater.transform.position.x < (3.3 + distance2));
            GameController.instance.upgradeManager.MoveSpeedUpgrade(0.1f);
            distance1 += 2.7f;
            distance2 += 3.3f;
        }    
    }

    [UnityTest]
    public IEnumerator DashToTheLeft()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1f);

        var characater = GameController.instance.character;
        var playerMovement = characater.GetComponent<PlayerMovement>();
        var playerCharacter = characater.GetComponent<PlayerCharacter>();

        playerMovement.facingRight = false;
        float distance1 = 0;
        float distance2 = 0;
        playerCharacter.dashCooldown = 0f;

        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(0.5f);
            playerMovement.PerformDash();
            yield return new WaitForSeconds(playerCharacter.dashTime);
            Debug.Log(characater.transform.position);
            Debug.Log((-2.7f - distance1) + " " + (-3.3f - distance2));
            Assert.IsTrue(characater.transform.position.x < (-2.7f - distance1) && characater.transform.position.x > (-3.3 - distance2));
            GameController.instance.upgradeManager.MoveSpeedUpgrade(0.1f);
            distance1 += 2.7f;
            distance2 += 3.3f;
        }
    }
}
