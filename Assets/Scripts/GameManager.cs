using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameObject.transform.Find("Welcome Background").gameObject.activeSelf)
        {
            Debug.Log("Escape key pressed");
            gameObject.transform.Find("Welcome Background").gameObject.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape) && gameObject.transform.Find("Welcome Background").gameObject.activeSelf)
        {
            Debug.Log("Escape key pressed");
            gameObject.transform.Find("Welcome Background").gameObject.SetActive(false);
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
