using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectItem3 : MonoBehaviour
{
    public objectSupplySpawner supplySpawner;

    public PlayerMove playerValue;

    void Start()
    {
        supplySpawner = GameObject.FindWithTag("SupplySpawner").GetComponent<objectSupplySpawner>();
        playerValue = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerModel"))
        {
            Destroy(gameObject);
        }
        playerValue.isItem3Acquired = true;
        playerValue.itemDuration = 15.0f;
        supplySpawner.isItemSpawned = false;
    }
}
