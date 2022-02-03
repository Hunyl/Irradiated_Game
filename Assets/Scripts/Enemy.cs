using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemySpawn enemySpawner;
    private GameObject Enemy1; //Spawn Mechanism

    private Vector3 EnemyMoveVector; //Enemy Movement
    public float enemyMoveSpeed = 1.0f;

    public GameManager gameManager;
    private RaycastHit barricadeHit;
    private Vector3 enemyPos;
    public float enemyMaxHP = 15.0f;
    public float enemyCurrentHP;
    public float enemyAttackCurrentCoolTime;
    private float enemyAttackCoolTime = 3.0f;
    private float wave; //Enemy Stats

    public Animator enemyAnimation; //Enemy Animation

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage; //HP Bar

    AudioSource audioSource;
    AudioSource BaudioSource;
    public AudioClip[] enemyAudio;

    private bool isMoving;
    private bool isDead;
    private bool isAttacking;

    void OnEnable()
    {
        wave = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().waveCount;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        this.gameObject.GetComponent<Collider>().enabled = true;
        enemyMaxHP = enemyMaxHP + (wave * 3.0f);
        enemyMoveSpeed = enemyMoveSpeed + (wave * 0.3f);
        enemyCurrentHP = enemyMaxHP;
    }

    void Start()
    {
        Enemy1 = gameObject;
        SetHPBar();
        EnemyMoveVector = new Vector3(0, 0, 1);
        enemySpawner = FindObjectOfType<EnemySpawn>();
        audioSource = GetComponent<AudioSource>();
        BaudioSource = GameObject.FindWithTag("ThudSound").GetComponent<AudioSource>();
    }

    void Update()
    {
        hpBarImage.fillAmount = enemyCurrentHP / enemyMaxHP;
        enemyPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        if (enemyCurrentHP <= 0)
        {
            EnemyMoveVector = Vector3.zero;
            this.gameObject.GetComponent<Collider>().enabled = false;
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            StartCoroutine("EnemyDead");
        }
        else
        {
            EnemyMoveVector = new Vector3(0, 0, 1);
            transform.forward = new Vector3(-1, 0, 0);
            transform.Translate(EnemyMoveVector * enemyMoveSpeed * Time.deltaTime);
            PlayWalkSound();
        }

        enemyAttackCurrentCoolTime -= Time.deltaTime;
    }

    IEnumerator EnemyDead()
    {
        enemyAnimation.SetTrigger("Enemy1Die");
        enemyAnimation.SetBool("Enemy1Move", true);
        PlayDeathSound();

        yield return new WaitForSecondsRealtime(3.0f);

        OnDie();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barricade"))
        {
            enemyMoveSpeed = 0;

            if (enemyAttackCurrentCoolTime <= 0)
            {
                enemyAnimation.SetBool("Enemy1Attack", true);
                enemyAnimation.SetBool("Enemy1Move", false);

                isAttacking = true;
                PlayAttackSound();
                BaudioSource.Play();

                gameManager.barricadeDamage = gameManager.barricadeDamage + 5;
                enemyAttackCurrentCoolTime = enemyAttackCoolTime;
            }
        }
    }

    public void OnDie()
    {
        Enemy1.SetActive(false);
        EnemySpawn.instance.InsertQueue(Enemy1);

        gameManager.score = (int)(gameManager.score + 100 + wave * 5f); 

        gameManager.waveEnemyKilled++;
        Debug.Log("Enemy Killed: " + gameManager.waveEnemyKilled);
    }

    void SetHPBar()
    {
        uiCanvas = GameObject.Find("HPCanvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpBar.GetComponent<EnemyHPSlider>();
        _hpbar.targetTr = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }

    void PlayWalkSound()
    { 
        if(EnemyMoveVector.magnitude != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if(isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = enemyAudio[0];
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void PlayDeathSound()
    {
        audioSource.clip = enemyAudio[2];
        audioSource.Play();
    }

    void PlayAttackSound()
    {
        if (isAttacking)
        {
            audioSource.clip = enemyAudio[1];
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}