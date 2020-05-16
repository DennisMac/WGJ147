using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Standard_Assets.Utility;

public class Music : MonoBehaviour
{
    private void Update()
    {
        
    }
    private void Awake()
    {
        Music[] objs = GameObject.FindObjectsOfType<Music>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Global.musicAudioSource = GetComponentInParent<AudioSource>();
        Global.musicAudioSource.Play();
    }
}
