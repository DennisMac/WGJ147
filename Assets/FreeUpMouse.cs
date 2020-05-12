using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeUpMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Cursor.visible = Cursor.visible;
       Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        //keep it visible on this screen
        Cursor.visible = Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
    }
}
