using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables for player movement
    public GameObject player;
    public Rigidbody playerRb;
    public float speed = 2.0f;
    private float horizontalInput;
    public float forwardInput;

    private GameManager gameManager; // This is the GameManager script that checks if the game is active

    // Variables for tracking player's distance/displacement
    private Vector3 lastPosition;
    public float totalDistance;

    // Variables for displaying player's stats
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI accelerationText;

    public int checkpointReached = 0; // Counter for number of reached checkpoints in the level
    public AudioSource playerAs; // Reference to the audio source attached to the player

    private float leftShift = 2.25f; // Constant amount for instanteinously shifting to the lane on the left
    private float rightShift = 2.25f; // Constant amount for instanteinously shifting to the lane on the right

    public bool isOnGround = true; // Checking whether the player is touching the ground to prevent double jumping (jumping mid-air)
    public float jumpForce; // Force that will be applied when the player triggers jump
    public AudioClip jumpSound; // Sound that will be played once when the player jumps
    private AudioSource playerAudio; // ??

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

        if (gameManager.isGameActive) // If the game is active, then the player can move
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && speed < 10 && checkpointReached > 0)
            {
                speed++;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && speed > 0 && checkpointReached > 0)
            {
                speed--;
            }

            // Move the vehicle forward
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

            // Move the vehicle right(left)
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
        velocityText.text = "Velocity: " +(int)speed + "[m/s]";
        distanceText.text = "Distance: " + (int)totalDistance + "[m]";
        accelerationText.text = "Acceler: " + 0 + "[m/s^2]";
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

    private void OnCollisionEnter(Collision collision) //checking whether the player is touching ground (in order to prevent double jumping)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; //Setting to true if touching the ground is detected
        }
    }
}
