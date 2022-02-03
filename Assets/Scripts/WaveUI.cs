using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
	public RectTransform newWaveBanner;
	public RectTransform gameOverUI;
	public Image fadePlane;
	public Text newWaveTitle;
	public Text newWaveEnemyCount;
	public Text finalScore;

	public GameManager gameManager;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnNewWaveUI(int waveNumber)
	{
		newWaveTitle.text = "Wave " + waveNumber;
		newWaveEnemyCount.text = ("Required Kills To Next Wave: " + (gameManager.enemyNeedToKill + (waveNumber * 5)));

		newWaveBanner.gameObject.SetActive(true);

		StopCoroutine("AnimateNewWaveBanner");
		StartCoroutine("AnimateNewWaveBanner");
	}

	public void OnGameOver()
	{
		fadePlane.gameObject.SetActive(true);
		StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, .95f), 1));
		gameOverUI.gameObject.SetActive(true);
		finalScore.text = ("Final Score: " + (gameManager.score + gameManager.waveCount * 1000));
	}

	IEnumerator AnimateNewWaveBanner()
	{

		float delayTime = 1.5f;
		float speed = 3f;
		float animatePercent = 0;
		int dir = 1;

		float endDelayTime = Time.time + 1 / speed + delayTime;

		while (animatePercent >= 0)
		{
			animatePercent += Time.deltaTime * speed * dir;

			if (animatePercent >= 1)
			{
				animatePercent = 1;
				if (Time.time > endDelayTime)
				{
					dir = -1;
				}
			}

			newWaveBanner.anchoredPosition = Vector2.down * Mathf.Lerp(-170, 60, animatePercent);
			yield return null;
		}

	}

	IEnumerator Fade(Color from, Color to, float time)
	{
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1)
		{
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp(from, to, percent);
			yield return null;
		}
	}
}
