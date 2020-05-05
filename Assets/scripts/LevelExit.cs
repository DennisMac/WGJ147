using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string levelToLoad ="01_Scene";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInChildren<Animator>().SetTrigger("interact");
            //make a sound and load level
            Invoke("LoadLevelNow", 1);
        }
    }

    void LoadLevelNow()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
