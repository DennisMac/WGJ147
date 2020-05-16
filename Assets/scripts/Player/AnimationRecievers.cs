using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Standard_Assets.Utility;

public class AnimationRecievers : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] footSteps;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



    public void FootStepRight()
    {
        if (Global.PlayerTransform == null) return;
        if ((Global.PlayerTransform.position - transform.position).sqrMagnitude < 50)
        {
            audioSource.pitch = 0.3f + 0.7f * Time.timeScale; //hack because mixer doesn't work in WebGL
            audioSource.PlayOneShot(footSteps[UnityEngine.Random.Range(0, footSteps.Length)]);
        }
    }
    public void FootStepLeft()
    {
        if (Global.PlayerTransform == null) return;
        if ((Global.PlayerTransform.position - transform.position).sqrMagnitude < 25)
        {
            audioSource.pitch = 0.3f + 0.7f * Time.timeScale; //hack because mixer doesn't work in WebGL
            audioSource.PlayOneShot(footSteps[UnityEngine.Random.Range(0, footSteps.Length)]);
        }
    }
}
