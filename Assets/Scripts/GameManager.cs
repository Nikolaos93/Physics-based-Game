using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Texts
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerCountdownText;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI levelResultsText;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI gameOverText;

    // Buttons 
    public Button nextLevelButton;
    public Button continueButton;
    public Button leaveButton;
    public Button restartButton;

    // Variables for tracking game stats and states
    private int score;
    private float timer;
    public bool isGameActive;
    public int hintClicks;

    // Title Screen (Main Menu) and Player Controller script reference
    public GameObject titleScreen;
    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the timer and get the PlayerController script
        timer = 60;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOver();
        }
        
        CountdownTimer();
    }

    public void StartGame() // This method will be called when the player clicks the "Play" button
    {
        isGameActive = true;
        score = 0;
        timer = 60;

        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);

        //CountdownTimer();
        //timerCountdownText.text = "Time: " + Mathf.Round(timer);
    }

    public void PauseGame()
    {
        if (isGameActive)
        {
            Debug.Log("Escape key pressed");
            pauseText.gameObject.SetActive(true);
            leaveButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
            isGameActive = false;
        }
        else if (!isGameActive)
        {
            Debug.Log("Escape key pressed");
            pauseText.gameObject.SetActive(false);
            leaveButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
            isGameActive = true;
        }
    }
    public void UnpauseGame() // This method will be called when the player clicks the "Continue" button
    {
        isGameActive = true;
    }

    public void LevelComplete() // This method will be called when the player reaches the finish line
    {
        Debug.Log("Level Complete!");
        UpdateScore(100 - (int)playerController.totalDistance + (int)timer - hintClicks * 30);
        nextLevelButton.gameObject.SetActive(true);
        levelCompleteText.gameObject.SetActive(true);
        levelResultsText.gameObject.SetActive(true);
        levelResultsText.text = "Score: level(100) - " + " distance(" + (int)playerController.totalDistance + ") + " + " timer(" + (int)timer + ") - " + "hints(30*" + hintClicks + ") = " + score;
        Debug.Log("Score is: " + score);
        isGameActive = false;
    }

    public void UpdateScore(int scoreToAdd) // Method to update the score
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver() // This method will be called when the timer reaches 0
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame() // This method will be called when the player clicks the "Restart" button
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel() // This method will be called when the player clicks the "Restart" button
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() // This method will be called when the player clicks the "Exit" button
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void CountdownTimer() // This method will countdown the timer
    {
        if (isGameActive && timer > 0)
        {
            timer -= Time.deltaTime;
            timerCountdownText.text = "Time: " + Mathf.Round(timer);
        }
        if (timer <= 0)
        {
            GameOver(); // Call the GameOver method when the timer reaches 0
        }
    }

    public void hintClicking() // Method for tracking the number of hints used
    {
        hintClicks++;
    }

}
