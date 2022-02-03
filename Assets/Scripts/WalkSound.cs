using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    private AudioSource audioSource;
    private PlayerMove player;
    private bool isMoving;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<PlayerMove>();
    }

    void Update()
    {
        PlayMoveSFX();
    }

    void PlayMoveSFX()
    {
        if (player.playerDir.magnitude != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
