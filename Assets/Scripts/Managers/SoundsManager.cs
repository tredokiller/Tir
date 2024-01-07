using System;
using UnityEngine;

namespace Managers
{
    public class SoundsManager : MonoBehaviour
    {
        public static SoundsManager instance;
        
        [SerializeField] private AudioSource[] audioSources;

        private void Awake()
        {
            instance = this;
        }

        public void PlaySound(AudioClip sound, float volume = 0)
        {
            foreach (var source in audioSources)
            {
                if (!source.isPlaying)
                {
                    source.clip = sound;
                    source.Play();
                    return;
                }
            }
        }
    }
}
