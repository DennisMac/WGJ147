using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private float oldCloak;
    private float oldTimeDialator;



    // Update is called once per frame
    void Update()
    {
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
