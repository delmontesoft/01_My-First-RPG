﻿using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] rayCastHits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit rayCastHit in rayCastHits)
            {
                CombatTarget target = rayCastHit.transform.GetComponent<CombatTarget>();

                if (!GetComponent<Fighter>().CanAttack(target)) continue;
                
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit raycastHit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out raycastHit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(raycastHit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
