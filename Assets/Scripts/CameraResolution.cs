using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    float originalRatio = 16 / 9f;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        transform.localScale *= Camera.main.aspect / originalRatio;
    }
}