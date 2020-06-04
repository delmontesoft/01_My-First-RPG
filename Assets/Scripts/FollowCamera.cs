using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target = null;

    void Update()
    {
            transform.position = target.position;
    }
}
