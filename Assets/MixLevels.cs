using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class MixLevels : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    private void Start()
    {
        float musicVolume, sfxVolume;
        if (masterMixer.GetFloat("MusicVolume", out musicVolume)) musicVolumeSlider.value = musicVolume;
        if (masterMixer.GetFloat("SFX", out sfxVolume)) sfxVolumeSlider.value = sfxVolume;
    }

    public void SetSfxLevel( float level)
    {
        masterMixer.SetFloat("SFX", level);
      }

    public void SetMusicLevel(float level)
    {
        masterMixer.SetFloat("MusicVolume", level);
    }
}
