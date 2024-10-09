using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVectors3 : MonoBehaviour
{
    public GameObject player;  // Reference to the player object (ball/point/vehicle)
    private PlayerController3 playerController3; // Reference to the PlayerController3 script
    private Vector3 offset = new Vector3(0, -5, 0); // offset the vector behind the player by adding to the player's position
    private Vector3 offset2 = new Vector3(0, -5, 0.25f); // offset the vector behind&beside the player by adding to the player's position

    public GameObject arrowVector1; // Game object for the vector that represents player's velocity
    public GameObject arrowVector2; // Game object for the vector that represents velocity of the river

    private Vector3 vectorMagnitude; // Variable that will scale the magnitude of the vector

    private Vector3 vectorStationary; // Variable that will scale the magnitude in stagnant water (checkpoint)

    // Start is called before the first frame update
    void Start()
    {
        playerController3 = GameObject.Find("Player").GetComponent<PlayerController3>(); // Finding Player game object and its component script
        vectorMagnitude = transform.localScale; // Initital scale/magnitude of the vector arrow
        vectorStationary = new Vector3(0, 0, 0); // Vector in stagnant water will be the zero/null vector
        arrowVector1 = GameObject.Find("Arrow 1"); // assigning "Arrow 1" game object as "player's vector"
        arrowVector2 = GameObject.Find("Arrow 2"); // assigning "Arrow 2" game object as "river's vector"
    }

    // Update is called once per frame
    void Update()
    {
        arrowVector1.transform.position = player.transform.position + offset; // Updating the position of the player's vector arrow by adding the offset to the player's position
        arrowVector1.transform.rotation = player.transform.rotation; // Updating the rotation of the player's vector arrow to match the orientation of the player
        arrowVector1.transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController3.speed * 0.4f)); // Scaling the vector by appropriate magnitude (in terms of player's vellocity)

        arrowVector2.transform.position = player.transform.position + offset2; // Updating the position of the river's vector arrow by adding the offset to the player's position
        arrowVector2.transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController3.speedOfRiver * 0.4f)); // Scaling the vector by appropriate magnitude (in terms of river's vellocity)

        if (player.transform.position.z > 2.5 && player.transform.position.z < 37.5) // Checking if the player is located in the "fisrt river" (flowing from right to left)
        {
            arrowVector2.transform.rotation = Quaternion.Euler(0, -90, 0); // Adjusting the rotation/direction of the river's vector to match the direction of the river's flow
        }
        else if (player.transform.position.z > 47.5 && player.transform.position.z < 83.5) // Checking if the player is located in the "second" river (flowing from left to right)
        {
            arrowVector2.transform.rotation = Quaternion.Euler(0, 90, 0); // Adjusting the rotation/direction of the river's vector to match the direction of the river's flow
        }
        else // Player is located in stagnant waters (checkpoint)
        {
            arrowVector2.transform.localScale = vectorStationary; // Water doesn't flow hence the river's vector is equal to the zero/null vecctor
        }
    }
}
