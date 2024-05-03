using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject escapeMenue;
    [SerializeField] private Image xpBar;
    [SerializeField] private Image hpBar;

    private PlayerCharacter playerCharacter;
    [SerializeField] private PlayerMovement playerMovement;

    private Vector2 scale1 = new Vector3(0.7f, 0.7f, 0.7f);
    private Vector2 scale2 = new Vector3(0.9f, 0.9f, 0.9f);
    private Vector2 scale3 = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector2 scale4 = new Vector3(1.3f, 1.3f, 1.3f);

    [SerializeField] private float minMultiplier = 0.93f;
    [SerializeField] private float maxMultiplier = 1.07f;

    private bool inMenue = false;
    public bool startingArea = false;

    private void Start()
    {
        if(!startingArea)
        {
            playerCharacter = GameController.instance.playerCharacter;
            playerMovement = GameController.instance.character.GetComponent<PlayerMovement>();
            UpdateHealth();
            UpdateExperience();
            UpdateLevel();
        }
            
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenEscapeMenue();
        }
    }

    private void OpenEscapeMenue()
    {
        if(inMenue)
        {
            BackToGame();
        }
        else
        {
            Time.timeScale = 0;
            inMenue = true;
            playerMovement.enabled = false;
            escapeMenue.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToGame()
    {
        if (!playerCharacter.dead)
        {
            Time.timeScale = 1.0f;
        }
        inMenue = false;
        playerMovement.enabled = true;
        escapeMenue.SetActive(false);
    }

    public void UpdateHealth()
    {
        hpBar.fillAmount = playerCharacter.currentHealth / playerCharacter.health;
        int roundedHp = (int)playerCharacter.currentHealth;
        int roundedMaxHp = (int)playerCharacter.health;
        hpText.SetText(roundedHp.ToString() + "/" + roundedMaxHp.ToString());
    }

    public void UpdateExperience()
    {
        xpBar.fillAmount = GameController.instance.levelManager.currentXpNotAddative / GameController.instance.levelManager.requiredXp;
        xpText.SetText(Mathf.RoundToInt(GameController.instance.levelManager.currentXpNotAddative).ToString() + "/" + Mathf.RoundToInt(GameController.instance.levelManager.requiredXp).ToString());
    }

    public void UpdateLevel()
    {
        levelText.SetText(GameController.instance.levelManager.currentLevel.ToString());
    }

    IEnumerator DelayDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPoolManager.RemoveObjectToPool(obj);
    }

    public void ShowDamage(Transform _transform, int damage, bool crit, float damageMultiplier)
    {
        GameObject text = ObjectPoolManager.SpawnObject(damageText, _transform.position + new Vector3(Random.Range(-0.4f,0.4f), Random.Range(-0.4f, 0.4f), 0), Quaternion.identity, ObjectPoolManager.PoolType.UI);

        TextMeshPro textMesh = text.transform.GetChild(0).GetComponent<TextMeshPro>();
        textMesh.text = damage.ToString();

        if (crit)
        {
            text.transform.localScale = scale4;
            textMesh.color = new Color(0.5f, 0.01f, 0f);
        }
        else
        {
            if (playerCharacter.damage * damageMultiplier * minMultiplier > damage) 
            {
                text.transform.localScale = scale1;
                textMesh.color = Color.white;
            }
            if (playerCharacter.damage * damageMultiplier * minMultiplier <= damage && playerCharacter.damage * damageMultiplier * maxMultiplier >= damage)
            {
                text.transform.localScale = scale2;
                textMesh.color = Color.white;
            }
            if (playerCharacter.damage * damageMultiplier * maxMultiplier < damage)
            {
                text.transform.localScale = scale3;
                textMesh.color = Color.white;
            }
        }
        
        StartCoroutine(DelayDestroy(text));
    }
}
