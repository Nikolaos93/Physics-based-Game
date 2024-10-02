using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTilter : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * 40, 30) - 15, 0, Mathf.PingPong(Time.time * 30, 40) - 20);
        }
        
    }
}
