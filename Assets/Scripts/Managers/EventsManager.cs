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
        public event Action OnRotationChanged;

        public void SendOnTrainingIsReady() => OnTrainingIsReady?.Invoke();
        public void SendOnTrainingIsStarted() => OnTrainingIsStarted?.Invoke();
        public void SendOnTrainingIsFinished() => OnTrainingIsFinished?.Invoke();
        public void SendOnRotationChanged() => OnRotationChanged?.Invoke();

        private void Awake()
        {
            instance = this;
        }
    }
}
