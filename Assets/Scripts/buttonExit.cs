using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonExit : MonoBehaviour
{
    Button buttonPlay;

    void OnButtonClicked()
    {
        Application.Quit();
    }
    void Start()
    {
        buttonPlay = GetComponent<Button>();
        buttonPlay.onClick.AddListener(OnButtonClicked);
    }
}
