using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVectors2 : MonoBehaviour
{
    public GameObject player;  // Reference to the player object (ball/point/vehicle)
    private PlayerController2 playerController;
    private Vector3 offset1 = new Vector3(0, -5, 0); // offset the camera behind the player by adding to the player's position
    private Vector3 offset2 = new Vector3(0.25f, -5, 0); // offset the camera behind the player by adding to the player's position
    //private Vector3 offsetFront = new Vector3(0, 2.25f, 1); //offset the camera (1st person)
    public GameObject arrowVector1;
    public GameObject arrowVector2;

    private Vector3 vectorMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController2>();
        vectorMagnitude = transform.localScale;
        arrowVector1 = GameObject.Find("Arrow 1");
        arrowVector2 = GameObject.Find("Arrow 2");
    }

    // Update is called once per frame
    void Update()
    {
        arrowVector1.transform.position = player.transform.position + offset1;
        arrowVector1.transform.rotation = player.transform.rotation;
        arrowVector1.transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController.speed * 0.4f));

        arrowVector2.transform.position = player.transform.position + offset2;
        arrowVector2.transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController.speedOfRiver * 0.4f));
    }
}
