using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon defaulWeapon = null;

        Health target;
        Animator animator;
        Weapon currentWeapon = null;
        float timeSinceLastAttack = 0;

        private void Start()
        {
            animator = GetComponent<Animator>();
            timeSinceLastAttack = timeBetweenAttacks;

            EquipWeapon(defaulWeapon);
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

        public void EquipWeapon(Weapon weapon)
        {
            if (animator == null) return;

            weapon.Spawn(handTransform, animator);
            currentWeapon = weapon;
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
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
            animator.ResetTrigger("stopAttack");
            //This will trigger the Animation Hit() event
            animator.SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= currentWeapon.GetRange();
        }

        //Animation Hit() event
        void Hit()
        {
            if (target == null || currentWeapon == null) return;

            target.TakeDamage(currentWeapon.GetDamage());
        }
    }
}