using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager instance;

    AudioSource[] audioSources;
    [SerializeField]
    AudioClip[] enemyLasers;
    [SerializeField]
    AudioClip[] robotTalking;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSources = GetComponents<AudioSource>();
    }

    


}
