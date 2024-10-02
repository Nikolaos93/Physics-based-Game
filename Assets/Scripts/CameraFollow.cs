using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;  // Reference to the player object (ball/point/vehicle)
    private Vector3 offset = new Vector3(0, 5, -7); // offset the camera behind the player by adding to the player's position
    private Vector3 offset2 = new Vector3(0, 7, -14);
    private Vector3 offsetFront = new Vector3(0, 2.25f, 1); //offset the camera (1st person)

    private bool cameraClicked = false; // this is a flag to check if the camera button/switch has been clicked

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C) && !cameraClicked) // if the C key is pressed and the camera is not clicked, then set the cameraClicked flag to true
        {
            cameraClicked = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && cameraClicked) // if the C key is pressed and the camera is clicked, then set the cameraClicked flag to false
        {
            cameraClicked = false;
        }

        if (!cameraClicked) // if the camera is not clicked, then follow the player from behind
        {
            // offset the camera behind the player by adding to the player's position
            transform.position = player.transform.position + offset;
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                transform.position = player.transform.position + offset2;
            }
        }
        if (cameraClicked)  // if the camera is clicked, then follow the player from the front (1st person view)
        {
            // offset the camera through the windshield
            transform.position = player.transform.position + offsetFront;
        }
    }
}
