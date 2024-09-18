using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingGroundManager : MonoBehaviour
{
    public GameObject[] fallingGrounds;
    public float fallingSpeed = 1.0f;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Finding the GameManager object and get the GameManager script from it
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < fallingGrounds.Length; i++)
        {
            Debug.Log(gameManager.timer);
            if (gameManager.timer < 60 - 2*i - 2)
            {
                fallingGrounds[i].GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
