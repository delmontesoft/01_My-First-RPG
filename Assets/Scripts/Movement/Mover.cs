using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float maxSpeed = 4f;

        NavMeshAgent navMeshAgent;
        Health health = null;

        public void StartMoveAction(Vector3 destination, float speedModifier)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedModifier);
        }

        public void MoveTo(Vector3 destination, float speedModifier)
        {
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedModifier);
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void Start() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}

