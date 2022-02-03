using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatNReload : MonoBehaviour
{
    Button buttonReload;
    public PlayerMove playerValue;
    public float reloadingCurrentTime;
    public bool isReloading;
    AudioSource audioSource;
    private float reloadTime = 4;
    private float currentReloadTime;
    public Image reloadProgress;

    void OnButtonClicked()
    {
        StartCoroutine("ReloadingProgress");
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        buttonReload = GetComponent<Button>();
        buttonReload.onClick.AddListener(OnButtonClicked);
    }

    void Update()
    {
        currentReloadTime -= Time.deltaTime;
        reloadProgress.fillAmount = currentReloadTime / 4f;
    }

    IEnumerator ReloadingProgress()
    {
        if(isReloading == false)
        {
            isReloading = true;
            playerValue.isAttackCoolTime = true;
            audioSource.Play();
            currentReloadTime = reloadTime;
            yield return new WaitForSeconds(reloadTime);

            playerValue.overheatValue = 0;
            playerValue.overheatGauge.fillAmount = 1.0f;
            playerValue.isAttackCoolTime = false;
            isReloading = false;
            playerValue.isAttackCoolTime = false;
        }
    }
}
