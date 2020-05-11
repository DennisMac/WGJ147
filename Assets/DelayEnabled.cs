using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEnabled : MonoBehaviour
{
    public float delay;
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child.SetActive(false);
        Invoke("EnableChild", delay);
    }

    void EnableChild()
    {
        child.SetActive(true);
    }
    
}
