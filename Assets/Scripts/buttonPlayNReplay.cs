using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonPlayNReplay : MonoBehaviour
{
    Button buttonPlay;

    void OnButtonClicked()
    {
        SceneManager.LoadScene("GamePhase");
    }
    void Start()
    {
        buttonPlay = GetComponent<Button>();
        buttonPlay.onClick.AddListener(OnButtonClicked);
    }
}
