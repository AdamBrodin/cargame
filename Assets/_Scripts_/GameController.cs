#pragma warning disable CS0649
using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Variables
    public Action startGame;
    public Animation anim;

    //public Player pl;

    public GameObject go_Screen;
    private int increaseScoreInterval, scoreAmount;
    public float animSpeed;
    public float timeSpent;
    #endregion

    public void Start()
    {
        startGame += StartGame;
        //pl = GetComponent<Player>();
        anim = GetComponent<Animation>();
        anim["Cycle"].speed = animSpeed;
        AudioManager.Instance.SetState("Theme", true);
    }

    private void Update()
    {
        timeSpent += Time.deltaTime;

        if (GameObject.Find("Player").GetComponent<Player>().isDead == true)
        {
            go_Screen.SetActive(true);
        }
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
