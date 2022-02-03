using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;

    public JoyStick joystick;
    public Vector3 playerDir;
    public float moveSpeed;
    public bool isMoving; //Movement

    public JoyStickRotation jRotation;
    public Vector3 rotation;
    public Transform playerRotation; //Rotation

    private Animator playerAnimator; //Player Animation

    public float playerAttackPoint = 2.0f;
    private RaycastHit enemyHit;
    private Transform playerModel;
    private Vector3 playerPos;
    private bool isCoolTime;
    private float coolTime = 0.1f;
    public Transform casing;
    public Transform casingEject;
    public float overheatValue = 0;
    private float overheatMax;
    public Image overheatGauge;
    public bool isAttackCoolTime;
    public MuzzleFlash muzzleFlash;
    public Text buffText;
    private int wave; //Player Attack Behavior

    public bool isItem1Acquired;
    public bool isItem3Acquired;

    public AudioClip audioWalk;
    public AudioClip audioShoot;
    AudioSource audioSource;
    public float delay;

    public float itemDuration;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        playerAnimator = GameObject.FindWithTag("PlayerModel").GetComponent<Animator>();
        playerModel = GameObject.FindWithTag("PlayerModel").GetComponent<Transform>();
    }

    void Update()
    {
        playerPos = new Vector3(playerModel.position.x, playerModel.position.y + 1, playerModel.position.z);
        wave = gameManager.waveCount;
        itemDuration -= Time.deltaTime;

        buffDuration();

        playerDir = joystick.InputControlVector();
        transform.Translate(playerDir * moveSpeed * Time.deltaTime);

        rotation = jRotation.InputControlVector();
        playerRotation.forward = rotation;

        PlayerAnimatorValue();

        if(isCoolTime == false && jRotation.isRInput == true && isAttackCoolTime == false)
        {
            isCoolTime = true;
            StartCoroutine("PlayerAttack");
        }

        
    }

    private void PlayerAnimatorValue()
    {
        playerAnimator.SetFloat("velocityX", playerDir.x / Mathf.Abs(rotation.x));
        playerAnimator.SetFloat("velocityZ", playerDir.z / Mathf.Abs(rotation.z));

        if(jRotation.isRInput == true && isAttackCoolTime == false)
        {
            playerAnimator.SetBool("isPlayerShooting", true);
        }
        else
        {
            playerAnimator.SetBool("isPlayerShooting", false);
        }
    }

    IEnumerator PlayerAttack()
    {
        if(isItem3Acquired == true)
        {
            StartCoroutine("ExtendedMag");
        }
        else
        {
            overheatMax = (1.0f - (overheatValue / 30));
        }

        if (jRotation.isRInput == true && isAttackCoolTime == false)
        {
            if(isItem1Acquired == true)
            {
                StartCoroutine("PlayerAttackType2");
            }
            else
            {
                StartCoroutine("PlayerAttackType1");
            }
            overheatValue++;
            overheatGauge.fillAmount = overheatMax; 
            PlaySound("Shoot");
            muzzleFlash.Activate();
            if (overheatMax == 0)
            {
                isAttackCoolTime = true;
            }
        }
        Instantiate(casing, casingEject.position, casingEject.rotation);

        yield return new WaitForSeconds(coolTime);
        isCoolTime = false;
    }

    IEnumerator PlayerAttackType1()
    {
        if (Physics.Raycast(playerPos, playerModel.forward, out enemyHit, 5000))
        {
            if (enemyHit.collider.tag == "Enemy1Model")
            {
                var enemy = enemyHit.collider.GetComponent<Enemy>();
                enemy.enemyCurrentHP = enemy.enemyCurrentHP - (playerAttackPoint + wave * 0.2f);
                gameManager.score = (int)(gameManager.score + 1 + wave * 0.2f);
            }
        }
        yield return null;
    }

    IEnumerator PlayerAttackType2()
    {
        RaycastHit[] enemyAllHit = Physics.RaycastAll(playerPos, playerModel.forward, 5000);

        for (int i = 0; i < enemyAllHit.Length; i++)
        {
            RaycastHit enemyAHit = enemyAllHit[i];

            if (enemyAHit.collider.tag == "Enemy1Model")
            {
                var enemy = enemyAHit.collider.GetComponent<Enemy>();
                enemy.enemyCurrentHP = enemy.enemyCurrentHP - (playerAttackPoint + wave * 1f) * 10.0f;
                gameManager.score = (int)(gameManager.score + 1 + wave * 0.2f);
            }
        }

        if (itemDuration <= 0)
        {
            isItem1Acquired = false;
        }

        yield return null;
    }

    IEnumerator ExtendedMag()
    {
        overheatMax = (1.0f - (overheatValue / 99999));

        if (itemDuration <= 0)
        {
            isItem3Acquired = false;
            overheatValue = 0;
        }

        yield return null;
    }

    void buffDuration()
    {
        if(isItem1Acquired == true)
        {
            isItem3Acquired = false;
            buffText.text = ("Armor-Piercing Rounds: " + (int)(itemDuration));
        }
        else if(isItem3Acquired == true)
        {
            isItem1Acquired = false;
            buffText.text = ("Extended Mag: " + (int)(itemDuration));
        }
        else
        {
            buffText.text = "";
        }
    }

    void PlaySound(string action)
    {
        switch(action)
        {
            case "Move":
                audioSource.clip = audioWalk;
                break;
            case "Shoot":
                audioSource.clip = audioShoot;
                break;
        }
        audioSource.Play();
    }
}