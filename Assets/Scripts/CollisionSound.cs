using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Manages the sound that should be played when the player hits the bomb or collects a star/diamond */
public class CollisionSound : MonoBehaviour
{

    public AudioClip AudioClip1;
    public AudioClip AudioClip2;
    public AudioSource collisionAudio;

    // Start is called before the first frame update
    void Start()
    {
        collisionAudio = GetComponent<AudioSource>(); // Reference to the Audio Source component of CollisionSoundManager Game Object
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayExplosionSound() // Method that will me called when the player collides with the object with "Bomb" tag 
    {
        collisionAudio.PlayOneShot(AudioClip1, 1.0f); // Play the explosion sound when the player hits the bomb
    }
    public void PlayCollectableSound() // Method that will me called when the player collides with the object with these tags: "Star, "Gem"
    {
        collisionAudio.PlayOneShot(AudioClip2, 1.0f); // Play the pickup sound when the player collects a collectable
    }
}
