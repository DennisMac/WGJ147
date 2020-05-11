using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private enum Axis { x, y, z };
    [SerializeField]
    private float angle = 0;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private Axis axis = Axis.y;


    void Update()
    {
        angle += speed * Time.deltaTime;
        if (angle > 360) angle -= 360;
        if (angle < -360) angle += 360;
        switch (axis)
        {
            case Axis.x:
                transform.localRotation = Quaternion.Euler(angle, 0, 0);
                break;
            case Axis.y:
                transform.localRotation = Quaternion.Euler(0, angle, 0);
                break;
            case Axis.z:
                transform.localRotation = Quaternion.Euler(0, 0, angle);
                break;
        }
    }
}
