using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    // Variables for player movement
    public float speed = 5.0f;
    public Rigidbody playerRb;
    private float turnSpeed = 25.0f;
    private float horizontalInput;
    private float forwardInput;

    public float speedOfRiver = 5.0f;
    private float speedUpstream;

    private GameManager gameManager; // This is the GameManager script that checks if the game is active

    // Variables for tracking player's distance/displacement
    private Vector3 lastPosition;
    public float totalDistance;

    // Variables for displaying player's stats
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI speedOfRiverText;

    public int checkpointReached = 0;

    public ParticleSystem waterSplashParticle;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Getting the Rigidbody component from the player object
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Finding the GameManager object and get the GameManager script from it
        lastPosition = transform.position; // Set the last position of the player to the starting position

        speedUpstream = speed - speedOfRiver; // ??
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

            if (Input.GetKeyDown(KeyCode.Space) && speed < 10)
            {
                speed++;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && speed > 0)
            {
                speed--;
            }

            // Move the vehicle forward
            if (transform.position.z > 2.5 && transform.position.z < 9.5)
            {
                //speed = speedUpstream;
                speedOfRiver = 3.0f;
                Debug.Log("First river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.left * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 9.5 && transform.position.z < 16.5)
            {
                //speed = speedUpstream;
                speedOfRiver = 5.0f;
                Debug.Log("First river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.left * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 16.5 && transform.position.z < 23.5)
            {
                //speed = speedUpstream;
                speedOfRiver = 7.0f;
                Debug.Log("First river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.left * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 23.5 && transform.position.z < 30.5)
            {
                //speed = speedUpstream;
                speedOfRiver = 5.0f;
                Debug.Log("First river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.left * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 30.5 && transform.position.z < 37.5)
            {
                //speed = speedUpstream;
                speedOfRiver = 3.0f;
                Debug.Log("First river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.left * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 47.5 && transform.position.z < 54.5)
            {
                speedOfRiver = 3.0f;
                Debug.Log("Second river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.right * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 54.5 && transform.position.z < 61.5)
            {
                speedOfRiver = 5.0f;
                Debug.Log("Second river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.right * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 61.5 && transform.position.z < 68.5)
            {
                speedOfRiver = 7.0f;
                Debug.Log("Second river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.right * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 68.5 && transform.position.z < 75.5)
            {
                speedOfRiver = 5.0f;
                Debug.Log("Second river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.right * Time.deltaTime * speedOfRiver, Space.World);
            }
            else if (transform.position.z > 75.5 && transform.position.z < 83.5)
            {
                speedOfRiver = 3.0f;
                Debug.Log("Second river: ");
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
                transform.Translate(Vector3.right * Time.deltaTime * speedOfRiver, Space.World);
            }
            else
            {
                speed = 5;
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.Self);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                waterSplashParticle.Play();
                playerAudio.loop = true;
                playerAudio.Play();
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                waterSplashParticle.Stop();
                playerAudio.Stop();
                playerAudio.loop = false;
            }

            // Move the vehicle right(left)
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); // Replaces the above line

            float distance = Vector3.Distance(lastPosition, transform.position); // Distance between the last position and the current position
            totalDistance += distance; // Adds the distance to the total distance 
            lastPosition = transform.position; // Updates the last position to the current position
        }

        if (!gameManager.isGameActive)
        {
            waterSplashParticle.Stop();
            playerAudio.Stop();
            playerAudio.loop = false;
        }

        playerValues(); // Calls the playerValues method to display the player's stats
    }

    private void playerValues() // This method will display the player's stats (e.g. velocity, distance, acceleration...) on the screen
    {
        velocityText.text = "Velocity: " + (int)speed + "[m/s]";
        distanceText.text = "Distance: " + (int)totalDistance + "[m]";
        speedOfRiverText.text = "River: " + (int)speedOfRiver + "[m/s]";
    }

    private void OnTriggerEnter(Collider other) // This method will be called when the player collides with another object (e.g. the finish line)
    {
        if (other.CompareTag("Finish")) // If the player collides with an object that has the tag "Finish", then the level is complete
        {
            gameManager.LevelComplete(); // Call the LevelComplete method from the GameManager script
            transform.position = new Vector3(0, 0.5f, 0); // Reset the player's position to the starting position
            Debug.Log("Level Finished");
        }

        if (other.CompareTag("Checkpoint")) // If the player collides with an object that has the tag "Finish", then the level is complete
        {
            checkpointReached++;
            gameManager.CheckpointReached(); // Call the LevelComplete method from the GameManager script  
            Debug.Log("Checkpoint Reached: " + checkpointReached);
        }
    }
}
