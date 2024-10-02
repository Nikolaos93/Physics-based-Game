using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVectors2 : MonoBehaviour
{
    public GameObject player;  // Reference to the player object (ball/point/vehicle)
    private PlayerController2 playerController;
    private Vector3 offset = new Vector3(0, -5, 0); // offset the camera behind the player by adding to the player's position
    //private Vector3 offsetFront = new Vector3(0, 2.25f, 1); //offset the camera (1st person)

    private Vector3 vectorMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController2>();
        vectorMagnitude = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        transform.rotation = player.transform.rotation;
        transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController.speed * 0.4f));
    }
}
