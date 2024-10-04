using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVectors3 : MonoBehaviour
{
    public GameObject player;  // Reference to the player object (ball/point/vehicle)
    private PlayerController3 playerController3;
    private Vector3 offset = new Vector3(0, -5, 0); // offset the camera behind the player by adding to the player's position
    private Vector3 offset2 = new Vector3(0, -5, 0.25f);
    //private Vector3 offsetFront = new Vector3(0, 2.25f, 1); //offset the camera (1st person)
    public GameObject arrowVector1;
    public GameObject arrowVector2;

    private Vector3 vectorMagnitude;

    private Vector3 vectorStationary;

    // Start is called before the first frame update
    void Start()
    {
        playerController3 = GameObject.Find("Player").GetComponent<PlayerController3>();
        vectorMagnitude = transform.localScale;
        vectorStationary = new Vector3(0, 0, 0);
        arrowVector1 = GameObject.Find("Arrow 1");
        arrowVector2 = GameObject.Find("Arrow 2");
    }

    // Update is called once per frame
    void Update()
    {
        arrowVector1.transform.position = player.transform.position + offset;
        arrowVector1.transform.rotation = player.transform.rotation;
        arrowVector1.transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController3.speed * 0.4f));

        arrowVector2.transform.position = player.transform.position + offset2;
        arrowVector2.transform.localScale = vectorMagnitude + new Vector3(0, 0, vectorMagnitude.z + (vectorMagnitude.z * playerController3.speedOfRiver * 0.4f));

        if (player.transform.position.z > 2.5 && player.transform.position.z < 37.5)
        {
            arrowVector2.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (player.transform.position.z > 47.5 && player.transform.position.z < 83.5)
        {
            arrowVector2.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            arrowVector2.transform.localScale = vectorStationary;
        }
    }
}
