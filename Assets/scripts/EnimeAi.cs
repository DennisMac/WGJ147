using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class heath {
	public static int Player = 5;
}

public class EnimeAi : MonoBehaviour

{

	public Transform target;
	public float speed = 2f;
    Cloak playerCloak;
    Vector3 originalPosition;
    Rigidbody rbody;

	void Start ()
    {
        playerCloak = FindObjectOfType<Cloak>();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
        originalPosition = transform.position;
        rbody = GetComponentInChildren<Rigidbody>();
	}

    private void FixedUpdate()
    {
        if (target == null) return;
        if (!playerCloak.IsCloaked) // If the player is pressing the "a " key
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, (target.position - transform.position), out hit, 100);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 8 && hit.distance >5)
                {
                    rbody.AddForce((target.position - transform.position) * speed);
                }
            }
        }
    }

    void Update ()
    {		

        //if ((target.position - transform.position).magnitude > 50) //todo: remove this after testing. It just respawns if the enemy gets too far away
        //{
        //    transform.position = originalPosition;
        //    rbody.velocity = Vector3.zero;
        //}

	}
}