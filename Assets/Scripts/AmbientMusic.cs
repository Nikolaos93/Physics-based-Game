using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls Ambient Music on each level */
public class AmbientMusic : MonoBehaviour
{
    public AudioSource ambientMusic; 
    private GameManager gameManager; // Reference to the GameManager

    // Start is called before the first frame update
    void Start()
    {
        ambientMusic = GetComponent<AudioSource>(); // Getting the Audio Source component of the Main Camera
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Finding the GameManager and getting the GameManager script from it
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive) // Checking whether the game is active
        {
            ambientMusic.UnPause(); // Play the music if the game is active
        }
        if (!gameManager.isGameActive) // Checks if the game is inactive
        {
            ambientMusic.Pause(); // Pause the music if the game is inactive
        }
    }
}
