using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public GameObject backpackObject;
    public GameObject jetpackObject;
    public static JetPack instance;
    public static void EnableJetpack(bool enabled)
    {        
        JetpackEnabled = enabled;
        instance.backpackObject.SetActive(!enabled);
        instance.jetpackObject.SetActive(enabled);        
    }
    private static bool JetpackEnabled = false;
    bool m_Jump = false;
    Rigidbody m_Rigidbody;
    [SerializeField]
    float m_JetPower = 10f;
    float m_TurnSpeed = 100f;
    float m_TurnAmount;
    bool engaged = false;

    [SerializeField]
    ParticleSystem ps;


    // Start is called before the first frame update
    void Start()
    {
        ps.Stop();
        m_Rigidbody = GetComponent<Rigidbody>();
        instance = this;
        EnableJetpack(false);
    }

    private void FixedUpdate()
    {
        if (!JetpackEnabled) return;
        m_Jump = Input.GetButton("Jump");
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        m_TurnAmount = h;
        if (m_Jump)
        {
            ps.Play();
            // jump!
            transform.Rotate(0, m_TurnAmount * m_TurnSpeed * Time.deltaTime, 0);
            //m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z ) + transform.rotation * new Vector3(0,m_JetPower,.05f * m_JetPower);
            m_Rigidbody.AddForce(transform.rotation * new Vector3(0, m_JetPower, .05f * m_JetPower));
            //m_Rigidbody.velocity *= .95f; // make some drag so you can't just fly away
        }
        else
        {
            ps.Stop();
        }
    }
}
