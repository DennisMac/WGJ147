using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float minDelay = 1f;
    public float maxDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMe", Random.Range(minDelay, maxDelay));
    }

    void DestroyMe()
    {
        Instantiate(Resources.Load<GameObject>("BodyPartExplosion"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
