using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        private Transform target;                                    // target to aim for

        bool patrolling = true;
        public Transform[] patrolPoints;
        private int patrolIndex = 0;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }

        float idleTime = 0f;
        private void Update()
        {
            if (target == null)
            {
                idleTime += Time.deltaTime;
                if (idleTime > 2)
                {
                    patrolling = true;
                    patrolIndex = 0;
                    target = patrolPoints[patrolIndex];
                }
            }

            if (patrolling)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (++patrolIndex > patrolPoints.Length-1) patrolIndex = 0;
                    target = patrolPoints[patrolIndex];
                }
            }


            if (target != null)
            {
                agent.SetDestination(target.position);
            }

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity * (patrolling?0.5f:1f), false, false);
            else
                character.Move(Vector3.zero, false, false);
        }


        public void SetTarget(Transform target)
        {
            if (target == null )
            {
                if (!patrolling) this.target = null;
            }
            else
            {
                patrolling = false;
                idleTime = 0f;
                this.target = target;
            }
        }
    }
}
