using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDestroy : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMe", 3);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
