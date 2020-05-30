using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */

public class MechController2 : MonoBehaviour
{
    public float turnSpeed = 1f;
    public float lookRadius = 50f;
    public float stopDistance = 4f;
    private Transform target;
    private Vector3 altTarget;
    private Animator anim;
    private Vector3 lastPosition;
    public  float animSpeed = 100f;
    private Rigidbody rbody;
    public float movementForce = 1f;
    private Cloak playerCloak;
    bool pathObstructed = false;
    float pathFinding = 0f;

    public GameObject waypointPrefab;
  

    void Start()
    {
        target = FindObjectOfType<Cloak>().transform;
        anim = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody>();
        playerCloak = FindObjectOfType<Cloak>();
    }

    void Update()
    {
        if (playerCloak == null) return;
        float distance = Vector3.Distance(playerCloak.transform.position, transform.position);
        if (!playerCloak.IsCloaked && distance <= lookRadius && pathFinding <= 0)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, (playerCloak.transform.position - transform.position), out hit, 100);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 8 && hit.distance > 5)
                {
                    FaceTarget();
                }
            }
        }
        else
        {
            pathFinding -= Time.deltaTime;
            Vector3 direction = (altTarget - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (pathObstructed)
        {
            if (pathFinding <= 0)
            {
                Vector3 offset = new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 0));
                altTarget = transform.position + transform.rotation * offset.normalized * 20;
                pathFinding = 2f;
                Instantiate(waypointPrefab, altTarget, Quaternion.identity);
            }
        }
        if (pathFinding >= 0)
        {
            if (Vector3.Dot(transform.forward, altTarget - transform.position) > 0.8f) //facing the target close enough
            {
                rbody.AddForce(movementForce * transform.forward, ForceMode.Force);
            }
        }
        else//go after player
        {
            if (Vector3.Dot(transform.forward, playerCloak.transform.position - transform.position) > 0.8f) //facing the target close enough
            {
                rbody.AddForce(movementForce * transform.forward, ForceMode.Force);                
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + movementForce * transform.forward);
    }

    // Point towards the player
    void FaceTarget()
    {
        //dmc todo: make a turning in place animation for this 
        
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        
    }

    private void LateUpdate()
    {
        float forward = Vector3.Dot((transform.position - lastPosition), transform.forward);
        anim.SetFloat("Forward", forward * animSpeed);
        lastPosition = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        pathObstructed = true;
        Debug.Log("path obstructed");
    }
    private void OnTriggerExit(Collider other)
    {
        pathObstructed = false;
        Debug.Log("clear");
    }
}
