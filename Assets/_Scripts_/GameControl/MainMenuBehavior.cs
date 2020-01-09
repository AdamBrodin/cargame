using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    #region Variables
    public GameObject mainMenu;
    private float timer;
    #endregion

    void Start()
    {
        //Cursor.visible = false;
        //FindObjectOfType<AudioManager>().Play("Theme");
    }

    public void LoadScene(string scenetoLoad)
    {
        //FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("sceneName to load: " + scenetoLoad);
        SceneManager.LoadScene(scenetoLoad);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        //FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("UUUUUUUUT!!!!");
        Application.Quit();
    }
}
