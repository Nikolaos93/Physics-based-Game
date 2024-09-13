using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public Rigidbody playerRb;
    private float turnSpeed = 25.0f;
    private float horizontalInput;
    private float forwardInput;

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
        /*if (gameManager.isGameActive) // If the game is active, then the player can move
        {
            float forwardInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            playerRb.AddForce(Vector3.forward * speed * forwardInput);
            playerRb.AddForce(Vector3.right * speed * horizontalInput);
        }*/

        if (gameManager.isGameActive)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            // Move the vehicle forward
            //transform.Translate(0, 0, 1);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput); // Replaces the above line

            // Move the vehicle right(left)
            //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput); // Move the vehicle to the right (turn right)
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); // Replaces the above line
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            gameManager.LevelComplete();
            transform.position = new Vector3(0, 0.5f, 0); // Reset the player's position to the starting position
            Debug.Log("Level Finished");
        }
    }
}
