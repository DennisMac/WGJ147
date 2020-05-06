using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedExplosion : MonoBehaviour
{
    [SerializeField] private float m_delay = 1f;
    [SerializeField] GameObject ps;
    private void Awake()
    {
        ps.SetActive(false);
        Invoke("Explode", m_delay);
    }

    private void Explode()
    {
        ps.SetActive(true);
    }  
}
