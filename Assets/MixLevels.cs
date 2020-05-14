using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer masterMixer;
    //public AudioMixer sfxMixer;
    //public AudioMixer musicMixer;
    

    public void SetSfxLevel( float level)
    {
        masterMixer.SetFloat("SFX", level);
      }

    public void SetMusicLevel(float level)
    {
        masterMixer.SetFloat("Music", level);
    }
}
