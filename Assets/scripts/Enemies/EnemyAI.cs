using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Standard_Assets.Utility;

public class heath {
	public static int Player = 5;
}

public class EnemyAI : MonoBehaviour

{
    bool targetAquired = false;
    public Transform target;
    public float speed = 2f;
    Cloak playerCloak;
    Vector3 originalPosition;
    Rigidbody rbody;
    AudioSource audioSource;
    public GameObject gun;
    void Start()
    {
        playerCloak = FindObjectOfType<Cloak>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
        rbody = GetComponentInChildren<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        gun.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (target == null) return;
        if (!playerCloak.IsCloaked)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, (target.position - transform.position), out hit, 100);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    if (hit.distance > 5)
                    {
                        gun.SetActive(true);
                        rbody.AddForce((target.position - transform.position) * speed);
                    }
                }
                else
                {
                    gun.SetActive(false);
                }
            }
        }
        else
        {
            gun.SetActive(false);
        }

        if (audioSource != null)
        {
            audioSource.pitch = 0.3f + 0.7f * Time.timeScale; //hack because mixer doesn't work in WebGL
            if (rbody.velocity.sqrMagnitude > 0.1f)
            {
                if(!audioSource.isPlaying) audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
}