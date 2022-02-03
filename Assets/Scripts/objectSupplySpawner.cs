using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSupplySpawner : MonoBehaviour
{
    public float itemRandomValue;

    public objectItem1 item1;
    public objectItem2 item2;
    public objectItem3 item3;

    public bool isItemSpawned;
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        OnItemSpawn();
    }

    void Update()
    {
        
    }

    public void OnItemSpawn()
    {
        itemRandomValue = (int)Random.Range(0, 3);

        switch (itemRandomValue)
        {
            case 0:
                Instantiate(item1, gameObject.transform);
                break;
            case 1:
                Instantiate(item2, gameObject.transform);
                break;
            case 2:
                Instantiate(item3, gameObject.transform);
                break;
        }

        isItemSpawned = true;
    }
}
