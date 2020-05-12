using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    private float oldCloak;
    private float oldTimeDialator;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 keyLocation = FindObjectOfType<SteamPunkKey>().transform.position;
            FindObjectOfType<PlayerHealth>().transform.position = keyLocation + Vector3.one;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3 exitLocation = FindObjectOfType<LevelExit>().transform.position;
            FindObjectOfType<PlayerHealth>().transform.position = exitLocation + Vector3.one;

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("LevelTestMenu");
        }

        //God mode
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerHealth.godMode = !PlayerHealth.godMode;
            if (PlayerHealth.godMode)
            {
                oldCloak = Cloak.powerUsage;
                oldTimeDialator = TimeDialator.powerUsage;
                Cloak.powerUsage = 0f;
                TimeDialator.powerUsage = 0f;
            }
            else
            {
                Cloak.powerUsage = oldCloak;
                TimeDialator.powerUsage = oldTimeDialator;
            }
        }
    }
}
