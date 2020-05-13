using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
public class CameraStart : MonoBehaviour
{
    Vector3 originalPosition;
    Quaternion originalRotation;
    public Vector3 StartLookat;
    public Vector3 StartPosition = new Vector3(0f, 40, 40);
    public FreeLookCam freeLookCam;
    public ProtectCameraFromWallClip protectCameraFromWallClip;



    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        transform.position = StartPosition;
        transform.rotation = Quaternion.LookRotation(StartLookat - transform.position, Vector3.up);
    }

    float n = 1;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, originalPosition, (Time.deltaTime/20f) * Mathf.Sqrt(n++) );
        transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, (Time.deltaTime / 20f) * Mathf.Sqrt(n++));
        if ((transform.position - originalPosition).sqrMagnitude < 0.02f)
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            this.enabled = false;
            freeLookCam.enabled = true;
            protectCameraFromWallClip.enabled = true;
        }
    }
}
