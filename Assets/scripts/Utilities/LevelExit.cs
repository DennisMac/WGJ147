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

    private void Start()
    {
        findKeyText = GameObject.FindWithTag("FindtheKeyText");
       // if (findKeyText != null) findKeyText.SetActive(false);
        Invoke("HideItAgainBecauseBrowserScrewsUp", 2);
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
                Invoke("LoadLevelNow", 1);
            }
        }
    }

    void LoadLevelNow()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
