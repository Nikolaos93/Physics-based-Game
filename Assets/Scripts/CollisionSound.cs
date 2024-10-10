using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Manages the sound that should be played when the player hits the bomb or collects a star/diamond */
public class CollisionSound : MonoBehaviour
{

    public AudioClip AudioClip1;
    public AudioClip AudioClip2;

    public AudioSource collisionAudio;
    public AudioSource playerSound;

    public Toggle sfxToggle;
    public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        collisionAudio = GetComponent<AudioSource>(); // Reference to the Audio Source component of CollisionSoundManager Game Object

        playerSound = GameObject.Find("Player").GetComponent<AudioSource>();

        if (DataManager.Instance.isSfxEnabled)
        {
            sfxToggle.isOn = true;
        }
        if (!DataManager.Instance.isSfxEnabled)
        {
            sfxToggle.isOn = false;
        }

        sfxSlider.value = DataManager.Instance.sfxVolume;
        collisionAudio.volume = DataManager.Instance.sfxVolume;
        playerSound.volume = DataManager.Instance.sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (sfxToggle.isOn)
        {
            DataManager.Instance.isSfxEnabled = true;
            collisionAudio.enabled = true;
            playerSound.enabled = true;
        }
        if (!sfxToggle.isOn)
        {
            DataManager.Instance.isSfxEnabled = false;
            collisionAudio.enabled = false;
            playerSound.enabled = false;
        }

        DataManager.Instance.sfxVolume = sfxSlider.value;
        collisionAudio.volume = sfxSlider.value;
        playerSound.volume = sfxSlider.value;
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
