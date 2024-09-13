using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public Rigidbody playerRb;

    private GameManager gameManager; // This is the GameManager script that we will use to check if the game is active

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // We get the Rigidbody component from the player object
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // We find the GameManager object and get the GameManager script from it
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive) // If the game is active, then the player can move
        {
            float forwardInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            playerRb.AddForce(Vector3.forward * speed * forwardInput);
            playerRb.AddForce(Vector3.right * speed * horizontalInput);
        }
    }
}
