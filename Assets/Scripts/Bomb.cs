using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody bombRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bomb triggered by: " + other.name);
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Bomb"))
        {
            Debug.Log("Bomb triggered by: ");
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.GameOver();
        }
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Gem"))
        {
            Debug.Log("Gem triggered by: ");
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(10);
        }
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Star"))
        {
            Debug.Log("Star triggered by: ");
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(5);
        }

    }
}

