using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGem : MonoBehaviour
{
    private bool collected = false;
    public GameObject arrow;

    private void Start()
    {
        arrow = Instantiate(arrow, GameController.instance.character.transform.position, Quaternion.identity);
        arrow.GetComponent<ArrowPointer>().targetPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collected)
        {
            GameController.instance.levelManager.CollectAllOrbs();
            collected = true;
            Destroy(arrow);
            Destroy(gameObject);
        }
        
    }
}
