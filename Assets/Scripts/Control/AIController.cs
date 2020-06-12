using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;

        GameObject player = null;
        Fighter fighter = null;

        private void Start() 
        {
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            AttackPlayer();
        }

        private void AttackPlayer()
        {
            if (InAttackRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            } 
        }

        private bool InAttackRange()
        {
            return Vector3.Distance(player.transform.position, transform.position) <= chaseRange;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}
