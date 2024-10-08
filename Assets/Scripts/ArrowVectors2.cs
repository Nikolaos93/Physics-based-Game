using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVectors2 : MonoBehaviour
{
    public GameObject player;  // Reference to the player object (ball/point/vehicle)
    private PlayerController2 playerController; // Reference to the PlayerController2 script
    private Vector3 offset1 = new Vector3(0, -5, 0); // offset the vector behind the player by adding to the player's position
    private Vector3 offset2 = new Vector3(0.25f, -5, 0); // offset the vector behind&beside the player by adding to the player's position

    public GameObject arrowVector1; // Game object for the vector that represents player's velocity
    public GameObject arrowVector2; // Game object for the vector that represents velocity of the river

    private Vector3 vectorMagnitude; // Variable that will scale the magnitude of the vector

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController2>(); // Finding Player game object and its component script
        vectorMagnitude = transform.localScale; // Initital scale/magnitude of the vector arrow
        arrowVector1 = GameObject.Find("Arrow 1"); // assigning "Arrow 1" game object as "player's vector"
        arrowVector2 = GameObject.Find("Arrow 2"); // assigning "Arrow 2" game object as "river's vector"
    }

    // Update is called once per frame
    void Update()
    {
        arrowVector1.transform.position = player.transform.position + offset1; // Updating the position of the player's vector arrow by adding the offset to the player's position
        arrowVector1.transform.rotation = player.transform.rotation; // Updating the rotation of the player's vector arrow to match the orientation of the player
        arrowVector1.transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController.speed * 0.4f)); // Scaling the vector by appropriate magnitude (in terms of player's vellocity)

        arrowVector2.transform.position = player.transform.position + offset2; // Updating the position of the river's vector arrow by adding the offset to the player's position
        arrowVector2.transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController.speedOfRiver * 0.4f)); // Scaling the vector by appropriate magnitude (in terms of river's vellocity)
    }
}
