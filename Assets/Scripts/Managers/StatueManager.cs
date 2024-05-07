using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueManager : MonoBehaviour
{
    [SerializeField] private GameObject statue;
    [SerializeField] private GameObject arrow;
    [SerializeField] private List<Vector3> locations = new List<Vector3>();

    private int random;

    private void Start()
    {
        
        for(int i = 0; i < 2; i++)
        {
            random = Random.Range(0, locations.Count);
            GameObject newStatue = Instantiate(statue, locations[random], Quaternion.identity);
            GameObject pointer = Instantiate(arrow, GameController.instance.character.transform.position, Quaternion.identity);
            pointer.GetComponent<ArrowPointer>().targetPos = locations[random];
            newStatue.GetComponent<Statue>().arrow = pointer;
            locations.RemoveAt(random);
        }

    }
}
