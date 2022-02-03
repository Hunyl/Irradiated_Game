using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectItem2 : MonoBehaviour
{
    public objectSupplySpawner supplySpawner;
    public GameManager gameManager;

    void Start()
    {
        supplySpawner = GameObject.FindWithTag("SupplySpawner").GetComponent<objectSupplySpawner>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.Rotate(-60 * Time.deltaTime, 0, 60 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerModel"))
        {
            Destroy(gameObject);
        }

        gameManager.barricadeDamage = 0f;
        supplySpawner.isItemSpawned = false;
    }
}
