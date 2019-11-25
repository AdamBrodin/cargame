#pragma warning disable CS0649
using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Variables
    private int increaseScoreInterval, scoreAmount;
    public Action startGame;
    public Animation anim;
    public float animSpeed;

    public float timeSpent;
    #endregion

    public void Start()
    {
        startGame += StartGame;
        anim = GetComponent<Animation>();
        anim["Cycle"].speed = animSpeed;
        AudioManager.Instance.SetState("Theme", true);
    }

    private void Update()
    {
        timeSpent += Time.deltaTime;
    }

    private void StartGame()
    {
        StartCoroutine(ScoreByTime());
    }

    private IEnumerator ScoreByTime()
    {
        ScoreSystem.Instance.IncreaseScore(scoreAmount);
        yield return new WaitForSeconds(increaseScoreInterval);
        StartCoroutine(ScoreByTime());
    }
}
