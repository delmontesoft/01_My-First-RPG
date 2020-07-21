using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
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

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            // ONE WAY IS WITH DICTIONARIES
            // Dictionary<string, object> saveData = new Dictionary<string, object>();
            // saveData["position"] = new SerializableVector3(transform.position);
            // saveData["rotation"] = new SerializableVector3(transform.eulerAngles);

            // THE OTHER WAY (BETTER) IS WITH THE STRUCT
            MoverSaveData saveData = new MoverSaveData();
            saveData.position = new SerializableVector3(transform.position);
            saveData.rotation = new SerializableVector3(transform.eulerAngles);

            return saveData;
        }

        public void RestoreState(object state)
        {
            // Dictionary<string, object> saveData = (Dictionary<string, object>)state;
            // SerializableVector3 position = (SerializableVector3)saveData["posicion"];
            // SerializableVector3 rotation = (SerializableVector3)saveData["rotation"];

            MoverSaveData saveData = (MoverSaveData)state;

            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = saveData.position.ToVector();
            transform.eulerAngles = saveData.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}

