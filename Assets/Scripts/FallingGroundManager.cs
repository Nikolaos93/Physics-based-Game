using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingGroundManager : MonoBehaviour
{
    public GameObject[] fallingGrounds;
    public GameObject[] fallingGrounds2;
    public GameObject[] fallingGrounds3;
    public float fallingSpeed = 1.0f;
    private GameManager gameManager;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Finding the GameManager object and get the GameManager script from it
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < fallingGrounds.Length - 1; i++)
        {
            Debug.Log(gameManager.timer);
            if (gameManager.timer < 60 - 2*i - 2)
            {
                fallingGrounds[i].GetComponent<Rigidbody>().isKinematic = false;
            }
        }*/

        for (int i = 0; i < fallingGrounds.Length - 1; i++)
        {
            //Debug.Log(gameManager.timer);
            if (playerController.transform.position.z > fallingGrounds[i].transform.position.z + 1)
            {
                fallingGrounds[i].GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if (playerController.checkpointReached == 1)
        {
            for (int i = 0; i < fallingGrounds2.Length - 1; i++)
            {
                //Debug.Log(gameManager.timer);
                if (gameManager.timer < 54 - i - 2)
                {
                    fallingGrounds2[i].GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }

        if (playerController.checkpointReached == 2)
        {
            for (int i = 0; i < fallingGrounds3.Length - 1; i++)
            {
                //Debug.Log(gameManager.timer);
                if (playerController.transform.position.z > fallingGrounds3[i].transform.position.z - 1)
                {
                    fallingGrounds3[i].gameObject.SetActive(true);
                }
                if (playerController.transform.position.z > fallingGrounds3[i].transform.position.z + 1)
                {
                    fallingGrounds3[i].GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }
}
