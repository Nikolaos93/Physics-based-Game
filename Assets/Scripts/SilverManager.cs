using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverManager : MonoBehaviour
{
    private Rigidbody silverRb;
    private GameManager gameManager;
    public ParticleSystem silverExplosionParticle;

    public bool silverCheck1 = false;

    private PlayerController5 playerController5;
    private BronzeManager bronzeManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController5 = GameObject.Find("Player").GetComponent<PlayerController5>();
        bronzeManager = GameObject.Find("Bronze Cube (1)").GetComponent<BronzeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) // This method will be called when the player collides with another object (e.g. the finish line)
    {
        if (collision.gameObject.CompareTag("TrayS") && gameObject.CompareTag("CubeS")) // If the player collides with an object that has the tag "Finish", then the level is complete
        {
            playerController5.tray2 = true;
            Instantiate(silverExplosionParticle, transform.position, silverExplosionParticle.transform.rotation);
            Debug.Log("Tray 2" + playerController5.tray2);
        }

        if (collision.gameObject.CompareTag("Tray4") && gameObject.CompareTag("CubeS")) // If the player collides with an object that has the tag "Finish", then the level is complete
        {
            silverCheck1 = true;
            Instantiate(silverExplosionParticle, transform.position, silverExplosionParticle.transform.rotation);
            if (silverCheck1 && bronzeManager.bronzeCheck1)
            {
                playerController5.tray4 = true;
                Debug.Log("Tray 4" + playerController5.tray4);
            }
        }
    }

}
