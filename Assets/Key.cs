using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public BoxCollider exitCollider;
    // Start is called before the first frame update
    void Start()
    {
        exitCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            exitCollider.enabled = true;
            Destroy(this.gameObject);
        }
    }
}
