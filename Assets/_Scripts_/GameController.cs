#pragma warning disable CS0649
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    #region Variables
    public Action startGame;

    //public Player pl;

    public GameObject go_Screen;
    private int increaseScoreInterval, scoreAmount;
    public float animSpeed;
    public float timeSpent;
    #endregion

    public void Start()
    {
        startGame += StartGame;
        go_Screen.SetActive(false);
        AudioManager.Instance.SetState("Theme", true);
    }

    private void Update()
    {
        timeSpent += Time.deltaTime;
        if (GameObject.Find("Player").GetComponent<Player>().isDead == true)
        {
            go_Screen.SetActive(true);
            AudioManager.Instance.SetState("Theme", false);
        }
    }

    private void StartGame() => StartCoroutine(ScoreByTime());
    private IEnumerator ScoreByTime()
    {
        ScoreSystem.Instance.IncreaseScore(scoreAmount);
        yield return new WaitForSeconds(increaseScoreInterval);
        StartCoroutine(ScoreByTime());
    }

    public void LoadScene(string sceneToLoad)
    {
        //FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level");
        Time.timeScale = 1f;
    }
}
