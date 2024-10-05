using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Bomb : MonoBehaviour
{
    private Rigidbody bombRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    
    private CollisionSound collisionSound;

    //private Transform pingPongOscillation;
    public float maxHeight1 = 1f;//max height of the object's movement 
    public float yCenter1 = 1f;
    public float maxHeight2 = 0.5f;//max height of the object's movement 
    public float yCenter2 = 6f;
    private Vector3 rotateAmount;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        collisionSound = GameObject.Find("CollisionSoundManager").GetComponent<CollisionSound>();
        
        rotateAmount = new Vector3(0, 60, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != "Bomb" && (SceneManager.GetActiveScene().name != "Level 1" || SceneManager.GetActiveScene().name != "Level 4"))
        {
            transform.position = new Vector3(transform.position.x, yCenter1 + Mathf.PingPong(Time.time * 0.1f, maxHeight1) - maxHeight1 / 2f, transform.position.z);//move on y axis only
            transform.Rotate(rotateAmount * Time.deltaTime);
        }
        if (gameObject.tag != "Bomb" && (SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 4"))
        {
            transform.position = new Vector3(transform.position.x, yCenter2 + Mathf.PingPong(Time.time * 0.1f, maxHeight2) - maxHeight2 / 2f, transform.position.z);//move on y axis only
            transform.Rotate(rotateAmount * Time.deltaTime);
        }
        if (gameObject.tag == "Bomb" && (SceneManager.GetActiveScene().name != "Level 1" || SceneManager.GetActiveScene().name != "Level 4"))
        {
            transform.position = new Vector3(transform.position.x, 0.25f + Mathf.PingPong(Time.time * 0.1f, 0.25f) - 0.25f / 2f, transform.position.z);//move on y axis only
        }
        if (gameObject.tag == "Bomb" && (SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 4"))
        {
            transform.position = new Vector3(transform.position.x, (0.25f + Mathf.PingPong(Time.time * 0.1f, 0.25f) - 0.25f / 2f) + 5.5f, transform.position.z);//move on y axis only
        }
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
            Debug.Log("Bomb triggered by me: ");
            collisionSound.PlayExplosionSound();
            
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.GameOver();
        }
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Gem"))
        {
            Debug.Log("Gem triggered by: ");
            
            collisionSound.PlayCollectableSound();
            
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(10);
        }
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Star"))
        {
            Debug.Log("Star triggered by: ");
            
            collisionSound.PlayCollectableSound();
            
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(5);
        }

    }
}

