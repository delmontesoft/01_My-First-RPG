using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool wasTriggered = false;

        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag != "Player" || wasTriggered) return;

            wasTriggered = true;
            GetComponent<PlayableDirector>().Play();    
        }

        public object CaptureState()
        {
            return wasTriggered;
        }

        public void RestoreState(object state)
        {
            wasTriggered = (bool)state;
        }
    }
}
