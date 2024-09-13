using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;  // Reference to the player object (vehicle/car)
    private Vector3 offset = new Vector3(0, 5, -7); // Offset the camera behind the player by adding to the player's position
    private Vector3 offsetFront = new Vector3(0, 2.25f, 1);

    private bool cameraClicked = false; // This is a flag to check if the camera has been clicked

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C) && !cameraClicked)
        {
            cameraClicked = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && cameraClicked)
        {
            cameraClicked = false;
        }

        if (!cameraClicked)
        {
            // Offset the camera behind the player by adding to the player's position
            transform.position = player.transform.position + offset;
        }
        if (cameraClicked)
        {
            // Offset the camera through the windshield
            transform.position = player.transform.position + offsetFront;
        }
    }
}
