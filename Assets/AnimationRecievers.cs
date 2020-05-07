using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            audioSource.PlayOneShot(footSteps[UnityEngine.Random.Range(0, footSteps.Length)]);
        }
    }
    public void FootStepLeft()
    {
        if (Global.PlayerTransform == null) return;
        if ((Global.PlayerTransform.position - transform.position).sqrMagnitude < 25)
        {
            audioSource.PlayOneShot(footSteps[UnityEngine.Random.Range(0, footSteps.Length)]);
        }
    }
}
