using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
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
