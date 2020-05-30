using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGround : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Player")
        {
            collision.transform.GetComponent<PlayerHealth>().DamageHealth(1000);
        }
    }
}
