using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject UI;

    private PlayerCharacter playerCharacter;

    private Vector2 scale1 = new Vector3(0.7f, 0.7f, 0.7f);
    private Vector2 scale2 = new Vector3(0.9f, 0.9f, 0.9f);
    private Vector2 scale3 = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector2 scale4 = new Vector3(1.3f, 1.3f, 1.3f);

    [SerializeField] private float minMultiplier = 0.93f;
    [SerializeField] private float maxMultiplier = 1.07f;


    private void Start()
    {
        playerCharacter = GameController.instance.playerCharacter;
    }
    public void UpdateHealth(float health)
    {
        int roundedHp = (int)health;
        HP.SetText(roundedHp.ToString());
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
