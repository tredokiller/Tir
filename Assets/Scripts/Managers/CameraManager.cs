using System;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Camera))]
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager instance;
        public Camera Camera { private set; get; }

        private EventsManager _eventsManager;
        
        private void Awake()
        {
            instance = this;
            Camera = GetComponent<Camera>();
            
            _eventsManager = EventsManager.instance;
        }
    }
}
