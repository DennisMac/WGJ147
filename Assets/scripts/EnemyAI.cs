using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    void Start()
    {
        playerCloak = FindObjectOfType<Cloak>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
        rbody = GetComponentInChildren<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
                if (hit.collider.gameObject.layer == 8 && hit.distance > 5)
                {
                    rbody.AddForce((target.position - transform.position) * speed);
                }
            }
        }

        if (audioSource != null)
        {
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