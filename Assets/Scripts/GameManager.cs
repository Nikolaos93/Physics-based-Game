using System.Collections;
using System.Collections.Generic;
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
    public Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameObject.transform.Find("Welcome Background").gameObject.activeSelf)
        {
            Debug.Log("Escape key pressed");
            gameObject.transform.Find("Welcome Background").gameObject.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape) && gameObject.transform.Find("Welcome Background").gameObject.activeSelf)
        {
            Debug.Log("Escape key pressed");
            gameObject.transform.Find("Welcome Background").gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOver();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void LevelComplete() // This method will be called when the player reaches the finish line
    {
        continueButton.gameObject.SetActive(true);
        levelCompleteText.gameObject.SetActive(true);
        Debug.Log("Level Complete!");
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

        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
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
