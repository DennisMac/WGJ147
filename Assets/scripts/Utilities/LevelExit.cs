using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelExit : MonoBehaviour
{
    GameObject findKeyText;
    public string levelToLoad ="01_Scene";
    private bool doorLocked = true;
    public bool DoorLocked { get { return doorLocked; } set { doorLocked = value; } }
    AudioSource audioSource;

    private void Start()
    {
        findKeyText = GameObject.FindWithTag("FindtheKeyText");
        Invoke("HideItAgainBecauseBrowserScrewsUp", 2);

        audioSource = GetComponent<AudioSource>();
    }
    private void HideItAgainBecauseBrowserScrewsUp()
    {
        findKeyText = GameObject.FindWithTag("FindtheKeyText");
        if (findKeyText != null) findKeyText.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            findKeyText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
            if (DoorLocked)
            {
                // dmc todo: show message to find key
                findKeyText.SetActive(true);
            }
            else
            {
                GetComponentInChildren<Animator>().SetTrigger("interact");
                //dmc todo: make a sound and load level
                if (!audioSource.isPlaying)
                {
                    audioSource.pitch = 0.3f + 0.7f * Time.timeScale; //hack because mixer doesn't work in WebGL
                    audioSource.Play();
                }

                Invoke("LoadLevelNow", 1);
            }
        }
    }

    void LoadLevelNow()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
