using RPG.Saving;
using System;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "DefaultSave";

        [SerializeField] float fadeInSeconds = 0.2f;

        SavingSystem savingSystem;

        IEnumerator Start() 
        {
            savingSystem = GetComponent<SavingSystem>();
            Fader fader = FindObjectOfType<Fader>();

            fader.FadeOutInmediate();
            yield return savingSystem.LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInSeconds);
        }

        private void Update() 
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                Load();
            }

            if(Input.GetKeyDown(KeyCode.F5))
            {
                Save();
            }
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }
    }
}