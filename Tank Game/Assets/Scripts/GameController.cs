using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public  GameObject gameOverScreen;
    public  GameObject greenTankWonText;
    public  GameObject redTankWonText;

    public GameObject pauseScreen;

    public GameObject player;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy == false)
            {
                PauseGame();
            } else
            {
                ContinueGame();
            }
        }
    }

    public void ContinueGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        player.GetComponent<PlayerMove>().isControllable = true;
    }

    public void PauseGame()
    {
        player.GetComponent<PlayerMove>().isControllable = false;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void GameOver(String loserTag)
    {
        switch (loserTag)
        {
            case "Enemy":
                greenTankWonText.SetActive(true);
                redTankWonText.SetActive(false);
                break;
            case "Player":
                greenTankWonText.SetActive(false);
                redTankWonText.SetActive(true);
                break;
            default:
                greenTankWonText.SetActive(false);
                redTankWonText.SetActive(false);
                break;
        }
        gameOverScreen.SetActive(true);
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
}
