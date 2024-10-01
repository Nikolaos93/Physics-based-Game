using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{

    public AudioClip AudioClip1;
    public AudioClip AudioClip2;
    public AudioSource collisionAudio;

    // Start is called before the first frame update
    void Start()
    {
        collisionAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayExplosionSound()
    {collisionAudio.PlayOneShot(AudioClip1, 1.0f);
    }
    public void PlayCollectableSound()
    {
        collisionAudio.PlayOneShot(AudioClip2, 1.0f);
    }
}
