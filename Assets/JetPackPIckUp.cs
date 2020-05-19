using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackPIckUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        JetPack.EnableJetpack(true);
        Destroy(gameObject);
    }
}
