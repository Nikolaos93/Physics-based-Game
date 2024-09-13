using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;

    public TextMeshProUGUI levelCompleteText;
    public Button nextLevelButton;

    private float timer;
    public TextMeshProUGUI timerCountdownText;

    public TextMeshProUGUI pauseText;
    public Button leaveButton;
    public Button continueButton;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        timer = 60;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            Debug.Log("Escape key pressed");
            pauseText.gameObject.SetActive(true);
            leaveButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
            isGameActive = false;
        } else if (Input.GetKeyDown(KeyCode.Escape) && !isGameActive)
        {
            Debug.Log("Escape key pressed");
            pauseText.gameObject.SetActive(false);
            leaveButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
            isGameActive = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOver();
        }
        
        CountdownTimer();
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void LevelComplete() // This method will be called when the player reaches the finish line
    {
        nextLevelButton.gameObject.SetActive(true);
        levelCompleteText.gameObject.SetActive(true);
        Debug.Log("Level Complete!");
        UpdateScore(100 - (int)playerController.totalDistance);
        Debug.Log("Score is: " + score);
        isGameActive = false;   
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        timer = 60;

        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);

        //CountdownTimer();
        //timerCountdownText.text = "Time: " + Mathf.Round(timer);
    }

    public void UnpauseGame()
    {
        isGameActive = true;
    }

    public void CountdownTimer()
    {
        if (isGameActive && timer > 0)
        {
            timer -= Time.deltaTime;
            timerCountdownText.text = "Time: " + Mathf.Round(timer);
        }
        if (timer <= 0)
        {
            GameOver();
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
