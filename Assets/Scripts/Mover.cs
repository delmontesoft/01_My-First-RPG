﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target = null;

// Update is called once per frame
void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }
}
