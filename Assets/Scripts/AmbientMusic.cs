using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusic : MonoBehaviour
{
    public AudioSource ambientMusic;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        ambientMusic = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Finding the GameManager object and get the GameManager script from it
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            ambientMusic.UnPause();
        }
        if (!gameManager.isGameActive)
        {
            ambientMusic.Pause();
        }
    }
}
