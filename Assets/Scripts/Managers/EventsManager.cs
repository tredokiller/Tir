using System;
using UnityEngine;

namespace Managers
{
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager instance;

        public event Action OnTrainingIsReady;
        public event Action OnTrainingIsStarted;

        public event Action OnTrainingIsFinished;


        public void SendOnTrainingIsReady() => OnTrainingIsReady?.Invoke();
        public void SendOnTrainingIsStarted() => OnTrainingIsStarted?.Invoke();

        public void SendOnTrainingIsFinished() => OnTrainingIsFinished?.Invoke();

        private void Awake()
        {
            instance = this;
        }
    }
}
