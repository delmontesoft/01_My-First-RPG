using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;

        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag != "Player") return;

            LoadScene();
        }

        private void LoadScene()
        {
            if (sceneToLoad < 0 || sceneToLoad >= SceneManager.sceneCountInBuildSettings) return;

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
