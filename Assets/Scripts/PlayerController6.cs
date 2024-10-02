using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController6 : MonoBehaviour
{
    // Variables for player movement
    public float speed = 5.0f;
    //public float acceleration = 1.0f;
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
    //public int checkpointReached = 0; ??
    //private float maxSpeed = 35.0f;

    private GameObject focalPoint; // ???

    /*public GameObject collectable1;
    public GameObject collectable2;*/
    public GameObject[] collectables;
    public Transform[] spawningPoints;
    public int collectableCount;

    private Bomb bCollectable; // refference to Bomb.cs where number of collected pickups is tracked


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Getting the Rigidbody component from the player object
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Finding the GameManager object and get the GameManager script from it
        lastPosition = transform.position; // Set the last position of the player to the starting position

        focalPoint = GameObject.Find("Focal Point"); // ???

        collectableCount = 0;
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
            forwardInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
            playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput);


            //Debug.Log(bCollectable.collectedNumber);
            //Debug.Log(bCollectable.collectedNumber);
            /*if (collectableCount < bCollectable.collectedNumber)
            {
                GameObject collectable = Instantiate(collectables[UnityEngine.Random.Range(0, collectables.Length)], spawningPoints[UnityEngine.Random.Range(0, spawningPoints.Length)]);
                collectableCount++;
            }*/


            /*if (Input.GetKeyDown(KeyCode.Space) && acceleration < 5)
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
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); // Replaces the above line*/

            float distance = Vector3.Distance(lastPosition, transform.position); // Distance between the last position and the current position
            totalDistance += distance; // Adds the distance to the total distance 
            lastPosition = transform.position; // Updates the last position to the current position
        }

        playerValues(); // Calls the playerValues method to display the player's stats

        //StartCoroutine(CollectableCountdownRoutine());

        if (transform.position.y < -5)
        {
            gameManager.GameOver(); // If the player falls off the platform, then the game is over
        }

    }

    private void playerValues() // This method will display the player's stats (e.g. velocity, distance, acceleration...) on the screen
    {
        velocityText.text = "Velocity: " + (int)speed + "[m/s]";
        distanceText.text = "Distance: " + (int)totalDistance + "[m]";
        accelerationText.text = "Acceler: " + (int)2 + "[m/s^2]";
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
            //checkpointReached++;
            gameManager.CheckpointReached(); // Call the LevelComplete method from the GameManager script  
            //Debug.Log("Checkpoint Reached: " + checkpointReached);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gem") && gameObject.CompareTag("Player"))
        {
            GameObject collectable = Instantiate(collectables[UnityEngine.Random.Range(0, collectables.Length)], spawningPoints[UnityEngine.Random.Range(0, spawningPoints.Length)]);
        }
        if (collision.gameObject.CompareTag("Star") && gameObject.CompareTag("Player"))
        {
            GameObject collectable = Instantiate(collectables[UnityEngine.Random.Range(0, collectables.Length)], spawningPoints[UnityEngine.Random.Range(0, spawningPoints.Length)]);
        }

    }

    /*IEnumerator CollectableCountdownRoutine()
    {
        yield return new WaitForSeconds(6);
        SpawnCollectable();
    }

    private void SpawnCollectable()
    {
        GameObject collectable = Instantiate(collectables[UnityEngine.Random.Range(0, collectables.Length)], spawningPoints[UnityEngine.Random.Range(0, spawningPoints.Length)]);
    }*/

}
