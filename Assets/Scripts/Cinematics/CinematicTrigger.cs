using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool wasTriggered = false;

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag != "Player" || wasTriggered) return;

            wasTriggered = true;
            GetComponent<PlayableDirector>().Play();    
        }
    }
}
