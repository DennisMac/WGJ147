using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */

public class MechController2 : MonoBehaviour
{

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    private Animator anim;
    private Vector3 lastPosition;
    public  float animSpeed = 100f;

    void Start()
    {
        target = FindObjectOfType<Cloak>().transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Get the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the radius
        if (distance <= lookRadius)
        {
            // Move towards the player
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    // Point towards the player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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

}
