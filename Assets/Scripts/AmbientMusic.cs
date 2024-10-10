using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/* Controls Ambient Music on each level */
public class AmbientMusic : MonoBehaviour
{
    public AudioSource ambientMusic; 
    private GameManager gameManager; // Reference to the GameManager

    public Toggle musicToggle;
    public Slider ambientMusicSlider;

    /*public AudioSource bombAudioSource;
    public AudioSource starAudioSource;
    public AudioSource gemAudioSource;*/
    //public Toggle sfxToggle;


    // Start is called before the first frame update
    void Start()
    {
        ambientMusic = GetComponent<AudioSource>(); // Getting the Audio Source component of the Main Camera
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Finding the GameManager and getting the GameManager script from it


        if (DataManager.Instance.isMusicEnabled)
        {
            musicToggle.isOn = true;
        }
        if (!DataManager.Instance.isMusicEnabled)
        {
            musicToggle.isOn = false;
        }

        ambientMusicSlider.value = DataManager.Instance.ambientMusicVolume;
        ambientMusic.volume = DataManager.Instance.ambientMusicVolume;



       /* if (DataManager.Instance.isSfxEnabled)
        {
            sfxToggle.isOn = true;
        }
        if (!DataManager.Instance.isSfxEnabled)
        {
            sfxToggle.isOn = false;
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        if (musicToggle.isOn)
        {
            DataManager.Instance.isMusicEnabled = true;
            ambientMusic.enabled = true;
        }
        if (!musicToggle.isOn)
        {
            DataManager.Instance.isMusicEnabled = false;
            ambientMusic.enabled = false;
        }

        DataManager.Instance.ambientMusicVolume = ambientMusicSlider.value;
        ambientMusic.volume = ambientMusicSlider.value;




        /*if (sfxToggle.isOn)
        {
            DataManager.Instance.isSfxEnabled = true;
            bombAudioSource.enabled = true;
            starAudioSource.enabled = true;
            gemAudioSource.enabled = true;
        }
        if (!sfxToggle.isOn)
        {
            DataManager.Instance.isSfxEnabled = false;
            bombAudioSource.enabled = false;
            starAudioSource.enabled = false;
            gemAudioSource.enabled = false;
        }*/

        /*if (dataManager.isMusicEnabled)
        {
            ambientMusic.enabled = true;*/

        if (gameManager.isGameActive) // Checking whether the game is active
            {
                ambientMusic.UnPause(); // Play the music if the game is active
            }
            if (!gameManager.isGameActive) // Checks if the game is inactive
            {
                ambientMusic.Pause(); // Pause the music if the game is inactive
            }
        /*}
        if (!dataManager.isMusicEnabled)
        {
            ambientMusic.enabled = false;
        }*/

    }

}
