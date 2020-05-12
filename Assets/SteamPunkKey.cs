using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPunkKey : MonoBehaviour
{
    LevelExit levelExit;
    public BoxCollider exitCollider;
    // Start is called before the first frame update
    void Start()
    {
        levelExit = GetComponentInParent<LevelExit>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            levelExit.DoorLocked = false;
            Destroy(this.gameObject);
        }
    }
}
