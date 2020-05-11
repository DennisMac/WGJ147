using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMe", Random.Range(1f,3f));
    }

    void DestroyMe()
    {
        Instantiate(Resources.Load<GameObject>("BodyPartExplosion"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
