using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier
        {
            A, B, C, D, E, F, G, H, I, J, K, L, M, N
        }

        [SerializeField] DestinationIdentifier destinationPortalID;
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint = null;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] float fadeInTime = 2f;

        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag != "Player") return;

            LoadScene();
        }

        private void LoadScene()
        {
            if (sceneToLoad < 0 || sceneToLoad >= SceneManager.sceneCountInBuildSettings) 
            {
                Debug.LogAssertion(this.name + " has no destination scene to load set!");
                return;
            }
            

            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(this.gameObject);

            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();

            yield return fader.FadeOut(fadeOutTime);
            wrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            wrapper.Load();
            
            UpdatePlayer(GetDestinationPortal());
            wrapper.Save();
            
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(this.gameObject);            
        }

        private Portal GetDestinationPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destinationPortalID != destinationPortalID) continue;

                return portal;
            }

            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;

        }
    }
}
