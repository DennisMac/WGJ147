using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
   
    }

    List<AudioSource> audioSourcesToPause = new List<AudioSource>();
    float oldTimeScale;
    void Update()
    {
  
        if (Input.GetKeyDown(KeyCode.P) ||  Input.GetKeyDown(KeyCode.O))
        {
            Global.IsPaused = !Global.IsPaused;
            if (Global.IsPaused == true)
            {
                oldTimeScale = Time.timeScale;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pausePanel.SetActive(true);
                AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
                audioSourcesToPause.Clear();
                foreach (AudioSource audioSource in allAudioSources)
                {
                    if (audioSource.isPlaying)
                    {
                        audioSource.Pause();
                        audioSourcesToPause.Add(audioSource);
                    }
                }
                
            }
            else
            {
                Time.timeScale = oldTimeScale;
                foreach (AudioSource audioSource in audioSourcesToPause)
                {
                    audioSource.UnPause();
                }
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None;
                resumeButton();
            }
        }  
        
    }

    public void resumeButton()
    {
        Global.IsPaused = false;
        pausePanel.SetActive(false);
    }

    public void exitButton()
    {
        Global.IsPaused = false;
        pausePanel.SetActive(false);

        SceneManager.LoadScene("LevelTestMenu");
        Cursor.visible = true;
    }

}
