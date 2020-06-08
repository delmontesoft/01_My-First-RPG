using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] rayCastHits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit rayCastHit in rayCastHits)
            {
                CombatTarget target = rayCastHit.transform.GetComponent<CombatTarget>();

                if (!target) continue;
                
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                } 
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit raycastHit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out raycastHit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(raycastHit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
