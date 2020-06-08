// using System.Collections;
// using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.position = FindObjectOfType<PlayerController>().transform.position;
        }
    }
}