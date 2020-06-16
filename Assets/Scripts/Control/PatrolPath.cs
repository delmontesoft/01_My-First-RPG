using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] bool showPathGizmos = true;
        [SerializeField] float gizmoRadius = 0.3f;
        [SerializeField] Color gizmoColour = Color.white;

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }


        public int GetNextIndex(int i)
        {
            if (i + 1 >= transform.childCount)
            {
                return 0;
            }

            return i + 1;
        }
        

        private void OnDrawGizmos() 
        {
            if (!showPathGizmos) return;
            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), gizmoRadius);
                Gizmos.color = gizmoColour;
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }
    }
}
