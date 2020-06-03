using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // [SerializeField] Transform target = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        RaycastHit raycastHit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out raycastHit);
        
        if (hasHit) {
            GetComponent<NavMeshAgent>().SetDestination(raycastHit.point);
        }
    }
}
