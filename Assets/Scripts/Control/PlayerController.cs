// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit raycastHit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out raycastHit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(raycastHit.point);
            }
        }
    }
}
