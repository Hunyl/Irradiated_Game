using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int waveEnemyKilled = 0;
    public int waveCount = 1;
    public int enemyNeedToKill = 1;

    public EnemySpawn enemySpawner;
    public Enemy enemyStats;

    public PlayerMove playerStats;

    public bool isNewWaveStarted;

    public float barricadeDamage;
    private float barricadeDamagedMax;
    public Rigidbody barricadeRigidBody;
    public GameObject barricade;
    public Image barricadeHPBar;

    public int score;
    public Text scoreText;

    public WaveUI newWaveUI;

    public objectSupplySpawner supplySpawner;

    void Start()
    {
        
    }

    void Update()
    {
        if(waveEnemyKilled >= enemyNeedToKill)
        {
            OnNewWave();

            enemyNeedToKill = enemyNeedToKill + (waveCount * 5);
            waveEnemyKilled = 0;
        }

        if (supplySpawner.isItemSpawned == false && isNewWaveStarted == true)
        {
            supplySpawner.OnItemSpawn();
        }

        isNewWaveStarted = false;

        barricadeDamagedMax = (1.0f - (barricadeDamage / 100f));
        barricadeHPBar.fillAmount = barricadeDamagedMax;

        OnBarricadeDestroy();

        scoreText.text = ("Score: " + score);
    }

    public void OnNewWave()
    {
        enemySpawner.subCoolTime = enemySpawner.subCoolTime + (waveCount * 0.3f);
        waveCount++;
        isNewWaveStarted = true;
        Debug.Log("New Wave: " + waveCount);
        newWaveUI.OnNewWaveUI(waveCount);
    }

    public void OnBarricadeDestroy()
    {
        if(barricadeDamagedMax <= 0)
        {
            barricadeRigidBody.AddExplosionForce(100, transform.position, 10, 10);
            barricade.SetActive(false);

            newWaveUI.OnGameOver();
        }
    }
}