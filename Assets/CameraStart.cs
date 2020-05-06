using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
public class CameraStart : MonoBehaviour
{
    Vector3 originalPosition;
    Quaternion originalRotation;
    public Vector3 StartLookat;
    public FreeLookCam freeLookCam;
    public ProtectCameraFromWallClip protectCameraFromWallClip;


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        transform.localPosition = new Vector3(0f, 40, 40);
        transform.rotation = Quaternion.LookRotation(StartLookat - transform.position, Vector3.up);
    }

    float n = 1;
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, (Time.deltaTime/20f) * Mathf.Sqrt(n++) );
        transform.rotation = Quaternion.LookRotation(StartLookat - transform.position, Vector3.up);
        if ((transform.localPosition - originalPosition).sqrMagnitude < 0.02f)
        {
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
            this.enabled = false;
            freeLookCam.enabled = true;
            protectCameraFromWallClip.enabled = true;
        }
    }
}
