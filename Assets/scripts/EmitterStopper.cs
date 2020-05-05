using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterStopper : MonoBehaviour
{
    [SerializeField] private float m_TimeOut = 2.0f;
    [SerializeField] private float m_EmitterOff = 0.20f;
    [SerializeField] private bool m_DetachChildren = false;


    private void Awake()
    {
        Invoke("StopEmitter", m_EmitterOff);
        Invoke("DestroyNow", m_TimeOut);
    }

    private void StopEmitter()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Stop();
        }
    }

    private void DestroyNow()
    {
        if (m_DetachChildren)
        {
            transform.DetachChildren();
        }
        Destroy(gameObject);
    }
}
