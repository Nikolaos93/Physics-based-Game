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

    /*public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;*/

    public TextMeshProUGUI checkpointText;
    public TextMeshProUGUI checkpointAdviceText;
    public TextMeshProUGUI checkpointAdvice2Text;

    // Buttons 
    public Button nextLevelButton;
    public Button continueButton;
    public Button leaveButton;
    public Button restartButton;

    public Button proceedButton;

    // Variables for tracking game stats and states
    private int score;
    public float timer;
    public bool isGameActive;
    public int hintClicks;

    private int highscore;
    public TMP_Text highscoreText;

    // Title Screen (Main Menu) and Player Controller script reference
    public GameObject titleScreen;
    private PlayerController playerController;

    public GameObject pauseScreen;
    public GameObject levelFinishedScreen;
    public GameObject statsScreen;
    public GameObject gameOverScreen;

    public GameObject checkpointScreen;


    // Start is called before the first frame update
    void Start()
    {
        /*highscore = PlayerPrefs.GetInt("SavedHighScore", 0);
        highscoreText.text = "" + highscore.ToString();*/

        // Initialize the timer and get the PlayerController script
        if (SceneManager.GetActiveScene().name == "Level 5")
        {
            timer = 180;
        }
        else
        {
            timer = 60;
        }
        //timer = 60;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOver();
        }*/
        
        CountdownTimer();
    }

    public void StartGame() // This method will be called when the player clicks the "Play" button
    {
        isGameActive = true;
        if (DataManager.Instance != null)
        {
            score = DataManager.Instance.scoreOverall;
        } else
        {
            score = 0;
        }

        if (SceneManager.GetActiveScene().name == "Level 5")
        {
            timer = 180;
        }
        else
        {
            timer = 60;
        }
        //timer = 60;

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
            pauseScreen.gameObject.SetActive(true);
            pauseText.gameObject.SetActive(true);
            leaveButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
            isGameActive = false;
        }
        else if (!isGameActive)
        {
            Debug.Log("Escape key pressed");
            pauseScreen.gameObject.SetActive(false);
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

    public void CheckpointReached() // This method will be called when the player reaches a checkpoint
    {
        if (isGameActive)
        {
            checkpointScreen.gameObject.SetActive(true);
            checkpointText.gameObject.SetActive(true);
            if (playerController.checkpointReached == 1)
            {
                checkpointAdviceText.gameObject.SetActive(true);
            }
            else if (playerController.checkpointReached == 2)
            {
                checkpointAdviceText.gameObject.SetActive(false);
                checkpointAdvice2Text.gameObject.SetActive(true);
            }
            //checkpointAdviceText.gameObject.SetActive(true);
            proceedButton.gameObject.SetActive(true);
            isGameActive = false;
        }
        else if (!isGameActive)
        {
            checkpointScreen.gameObject.SetActive(false);
            checkpointText.gameObject.SetActive(false);
            if (playerController.checkpointReached == 1)
            {
                checkpointAdviceText.gameObject.SetActive(false);
            }
            else if (playerController.checkpointReached == 2)
            {
                checkpointAdvice2Text.gameObject.SetActive(false);
            }
            //checkpointAdviceText.gameObject.SetActive(false);
            proceedButton.gameObject.SetActive(false);
            isGameActive = true;
        }
    }

    public void LevelComplete() // This method will be called when the player reaches the finish line
    {
        Debug.Log("Level Complete!");
        UpdateScore(100 - (int)playerController.totalDistance + (int)timer - hintClicks * 30);
        DataManager.Instance.scoreOverall = score;
        levelFinishedScreen.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);
        levelCompleteText.gameObject.SetActive(true);
        levelResultsText.gameObject.SetActive(true);
        levelResultsText.text = "Score: level(100) - " + " distance(" + (int)playerController.totalDistance + ") + " + " timer(" + (int)timer + ") - " + "hints(30*" + hintClicks + ") = " + score;
        Debug.Log("Score is: " + score);
        isGameActive = false;
        if (SceneManager.GetActiveScene().name == "Level 6")
        {
            ScoreManager.instance.HighScoreCheck(score);
            RestartGame();
        }
    }

    public void UpdateScore(int scoreToAdd) // Method to update the score
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver() // This method will be called when the timer reaches 0
    {
        ScoreManager.instance.HighScoreCheck(score);
        gameOverScreen.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame() // This method will be called when the player clicks the "Restart" button
    {
        DataManager.Instance.scoreOverall = 0;
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel() // This method will be called when the player clicks the "Restart" button
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
