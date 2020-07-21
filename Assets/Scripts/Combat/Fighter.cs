using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] float weaponRage = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform rightHandTransform = null;
        // [SerializeField] Transform leftHandTransform = null;
        [SerializeField] AnimatorOverrideController weaponOverride = null;

        Health target;
        float timeSinceLastAttack = 0;

        private void Start()
        {
            timeSinceLastAttack = timeBetweenAttacks;
            SpawnWeapon();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (!target || target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            //This will trigger the Animation Hit() event
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRage;
        }

        private void SpawnWeapon()
        {
            Instantiate(weaponPrefab, rightHandTransform);
            GetComponent<Animator>().runtimeAnimatorController = weaponOverride;
        }

        //Animation Hit() event
        void Hit()
        {
            if (!target) return;
            
            target.TakeDamage(weaponDamage);
        }
    }
}