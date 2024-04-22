using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject UI;

    private Vector2 scale1 = new Vector3(0.7f, 0.7f, 0.7f);
    private Vector2 scale2 = new Vector3(0.9f, 0.9f, 0.9f);
    private Vector2 scale3 = new Vector3(1.1f, 1.1f, 1.1f);

    public void UpdateHealth(float health)
    {
        HP.SetText(health.ToString());
    }

    IEnumerator DelayDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPoolManager.RemoveObjectToPool(obj);
    }

    public void ShowDamage(Transform _transform, int damage)
    {
        GameObject text = ObjectPoolManager.SpawnObject(damageText, _transform.position, Quaternion.identity, ObjectPoolManager.PoolType.UI);

        text.transform.GetChild(0).GetComponent<TextMeshPro>().text = damage.ToString();
        if(damage <= 50)
        {
            text.transform.localScale = scale1;
        }
        if(damage <= 100 && damage > 50)
        {
            text.transform.localScale = scale2;
        }
        if(damage > 100)
        {
            text.transform.localScale = scale3;
        }
        StartCoroutine(DelayDestroy(text));
    }
}
