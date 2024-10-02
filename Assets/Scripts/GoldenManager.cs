using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenManager : MonoBehaviour
{
    private Rigidbody goldenRb;
    private GameManager gameManager;
    public ParticleSystem goldenExplosionParticle;

    private PlayerController5 playerController5;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController5 = GameObject.Find("Player").GetComponent<PlayerController5>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) // This method will be called when the player collides with another object (e.g. the finish line)
    {
        if (collision.gameObject.CompareTag("TrayG") && gameObject.CompareTag("CubeG")) // If the player collides with an object that has the tag "Finish", then the level is complete
        {
            playerController5.tray1 = true;
            Instantiate(goldenExplosionParticle, transform.position, goldenExplosionParticle.transform.rotation);
            Debug.Log("Tray 1" + playerController5.tray1);
        }

        if (collision.gameObject.CompareTag("Tray5") && gameObject.CompareTag("CubeG")) // If the player collides with an object that has the tag "Finish", then the level is complete
        {
            playerController5.tray5 = true;
            Instantiate(goldenExplosionParticle, transform.position, goldenExplosionParticle.transform.rotation);
            Debug.Log("Tray 5" + playerController5.tray5);
        }
    }

}
