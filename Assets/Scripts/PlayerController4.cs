using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController4 : MonoBehaviour
{
    // Variables for player movement
    public GameObject player;
    public float speed = 0f;
    public float acceleration = 1.0f; 
    public Rigidbody playerRb;
    //private float turnSpeed = 25.0f;
    private float horizontalInput;
    private float forwardInput;

    private GameManager gameManager; // This is the GameManager script that checks if the game is active

    // Variables for tracking player's distance/displacement
    private Vector3 lastPosition;
    public float totalDistance;

    // Variables for displaying player's stats
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI accelerationText;

    //public GameObject cube; // ???
    public int checkpointReached = 0;
    private float maxSpeed = 35.0f;
    public AudioSource playerAs;

    private float leftShift = 2.25f;
    private float rightShift = 2.25f;

    public bool isOnGround = true;
    public float jumpForce;
    public AudioClip jumpSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Getting the Rigidbody component from the player object
        playerAs.GetComponent<AudioSource>(); // Getting the AudioSource component from the player object
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Finding the GameManager object and get the GameManager script from it
        lastPosition = transform.position; // Set the last position of the player to the starting position

        playerAudio = GetComponent<AudioSource>();
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

        if (gameManager.isGameActive) // If the game is active, then the player can move
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && acceleration < 5)
            {
                acceleration++;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && acceleration > -5)
            {
                acceleration--;
            }

            // Move the vehicle forward
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * acceleration * forwardInput);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            //transform.position.x = transform.position.x + speed * Time.deltaTime;

            // Move the vehicle right(left)
            //transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); // Replaces the above line

            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && player.transform.position.x != -2.25)
            {
                player.transform.position = new Vector3(player.transform.position.x - leftShift, player.transform.position.y, player.transform.position.z);
            }
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && player.transform.position.x != 2.25)
            {
                player.transform.position = new Vector3(player.transform.position.x + rightShift, player.transform.position.y, player.transform.position.z);
            }
            if (player.transform.position.x < -2.25)
            {
                player.transform.position = new Vector3(-2.25f, player.transform.position.y, player.transform.position.z); ;
            }
            if (player.transform.position.x > 2.25)
            {
                player.transform.position = new Vector3(2.25f, player.transform.position.y, player.transform.position.z); ;
            }

            if (Input.GetKeyDown(KeyCode.W) && !playerAs.loop/* && gameManager.isGameActive*/)
            {
                playerAs.loop = true;
                playerAs.Play();
            }
            else if (Input.GetKeyUp(KeyCode.W) && playerAs.loop)
            {
                playerAs.Stop();
                playerAs.loop = false;
            }

            if (Input.GetKeyDown(KeyCode.F) && isOnGround && (transform.position.x > 0.5 || transform.position.x < -0.5))
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }

            float distance = Vector3.Distance(lastPosition, transform.position); // Distance between the last position and the current position
            totalDistance += distance; // Adds the distance to the total distance 
            lastPosition = transform.position; // Updates the last position to the current position
        }

        if (!gameManager.isGameActive)
        {
            playerAs.Stop();
            playerAs.loop = false;
        }

        playerValues(); // Calls the playerValues method to display the player's stats

        if (transform.position.y < 0)
        {
            gameManager.GameOver(); // If the player falls off the platform, then the game is over
        }
    }

    private void playerValues() // This method will display the player's stats (e.g. velocity, distance, acceleration...) on the screen
    {
        velocityText.text = "Velocity: " + (int)speed + "[m/s]";
        distanceText.text = "Distance: " + (int)totalDistance + "[m]";
        accelerationText.text = "Acceler: " + (int)acceleration + "[m/s^2]";
    }

    private void OnTriggerEnter(Collider other) // This method will be called when the player collides with another object (e.g. the finish line)
    {
        if (other.CompareTag("Finish")) // If the player collides with an object that has the tag "Finish", then the level is complete
        {
            gameManager.LevelComplete(); // Call the LevelComplete method from the GameManager script
            //transform.position = new Vector3(0, 0.5f, 0); // Reset the player's position to the starting position
            Debug.Log("Level Finished");
        }

        if (other.CompareTag("Checkpoint")) // If the player collides with an object that has the tag "Finish", then the level is complete
        {
            checkpointReached++;
            gameManager.CheckpointReached(); // Call the LevelComplete method from the GameManager script  
            Debug.Log("Checkpoint Reached: " + checkpointReached);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
