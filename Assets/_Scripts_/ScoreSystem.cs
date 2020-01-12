#pragma warning disable CS0649
using System;
using System.Collections;
using TMPro;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class ScoreSystem : MonoBehaviour
{
    #region Singleton
    private static ScoreSystem instance;
    public static ScoreSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreSystem>();
            }
            return instance;
        }
    }
    #endregion
    #region Variables
    public Action<int> IncreaseScore;
    public int currentScore;
    private bool isAlive = true;
    [SerializeField] private int autoScoreIncrease;
    [SerializeField] private float autoScoreIncreaseTime;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject scorePopupText;
    #endregion

    private void Start()
    {
        IncreaseScore += AddScore;
        Player.Instance.OnPlayerDeath += SetHighScore;
        StartCoroutine(ScoreByTime());
    }

    private void UpdateScore() => scoreText.text = currentScore.ToString();
    private void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScore();
    }

    public IEnumerator ScoreRewardEffect(Color color, float blinkTime, int rewardAmount, Vector3 pos)
    {
        if (scorePopupText != null)
        {
            GameObject g = Instantiate(scorePopupText, pos, Quaternion.identity, GameObject.Find("Canvas").transform);
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, g.transform.position);
            Vector2 spawnPos = (screenPoint - GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta / 2f);

            TextMeshProUGUI textObject = scorePopupText.GetComponent<TextMeshProUGUI>();
            textObject.rectTransform.anchoredPosition = spawnPos;
            textObject.text = $"+{rewardAmount}";
        }

        StartCoroutine(ColorBlink(scoreText, color, blinkTime));
        yield return new WaitForSeconds(blinkTime);
        if (scorePopupText != null) Destroy(scorePopupText);
    }

    private IEnumerator ColorBlink(TextMeshProUGUI textObject, Color fadeColor, float blinkTime)
    {
        Color startColor = textObject.color;
        textObject.color = fadeColor;
        yield return new WaitForSeconds(blinkTime);
        textObject.color = startColor;
    }

    private IEnumerator ScoreByTime()
    {
        if (autoScoreIncrease > 0 && autoScoreIncreaseTime > 0 && isAlive)
        {
            yield return new WaitForSecondsRealtime(autoScoreIncreaseTime);
            AddScore(autoScoreIncrease);
            StartCoroutine(ScoreByTime());
        }
    }

    private void SetHighScore(int value)
    {
        isAlive = false;
        int currentHighscore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        if (currentScore > currentHighscore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", value);
        }
    }

}
