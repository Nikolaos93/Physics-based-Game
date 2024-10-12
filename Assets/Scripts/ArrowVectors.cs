using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVectors : MonoBehaviour
{
    public GameObject player;  // Reference to the player object (ball/point/vehicle)
    private PlayerController playerController; // Reference to the PlayerController script
    private Vector3 offset = new Vector3(0, -5, 0); // offset the vector above the player by adding to the player's position

    private Vector3 vectorMagnitude; // Variable that will scale the magnitude of the vector
    private int arrowDirection = 1; // Variable that will determine the direction of the arrow

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // Finding Player game object and its component script
        vectorMagnitude = transform.localScale; // Initital scale/magnitude of the vector arrow
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset; // Updating the position of the vector arrow by adding the offset to the player's position
        transform.rotation = player.transform.rotation; // Updating the rotation of the vector so that it matches the orientation/direction of the player
        //transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController.speed * 0.4f)); // Scaling the vector by appropriate magnitude (in terms of vellocity)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            arrowDirection = 1; // If the player is moving forward, the arrow should point in the positive direction
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrowDirection = -1; // If the player is moving backward, the arrow should point in the negative direction
        }
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, playerController.speed * arrowDirection); // Scaling the vector by appropriate magnitude (in terms of vellocity)
    }
}
