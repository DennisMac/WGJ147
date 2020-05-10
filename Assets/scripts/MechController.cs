using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */

public class MechController : MonoBehaviour {

	public float lookRadius = 10f;
    [SerializeField]
    float animSpeed = 100f;

	Transform target;
	NavMeshAgent agent;
    Rigidbody rBody;
    Animator anim;
    Vector3 lastPosition;
	void Start()
	{
        target = FindObjectOfType<Cloak>().transform;
		agent = GetComponent<NavMeshAgent>();
        rBody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
	}

	void Update ()
	{
        if (target == null) return;

		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);

        // If inside the radius
        if (!Global.PlayerCloaked)
        {
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

        
	}

    private void LateUpdate()
    {
        float forward = Vector3.Dot((transform.position - lastPosition), transform.forward);
        forward = Mathf.Clamp(forward, 0, 1f);
        anim.SetFloat("Forward", forward * animSpeed);
        lastPosition = transform.position;
    }




    // Point towards the player
    void FaceTarget ()
	{ 
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5); //slow he's a mech
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
