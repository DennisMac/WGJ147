using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFrame : MonoBehaviour
{
    Vector3 originalPosition;
    Quaternion originalRotation;


    private void Start()
    {
        originalRotation = transform.rotation;
        originalPosition = transform.localPosition;
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = originalPosition;
        transform.rotation = originalRotation;        
    }
}
