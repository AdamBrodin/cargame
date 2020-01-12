using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    #region Variables
    public GameObject mainMenu;
    private float timer;
    [SerializeField] private TextMeshProUGUI highscoreText;
    #endregion

    void Start()
    {
        if (Debug.isDebugBuild) { Cursor.visible = false; }
        //FindObjectOfType<AudioManager>().Play("Theme");

        float currentHighscore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        if (currentHighscore > 0)
        {
            highscoreText.text = "HIGHSCORE: " + currentHighscore.ToString();
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        //FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        //FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }
}
