using System;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Camera))]
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager instance;
        public Camera Camera { private set; get; }
        
        private void Awake()
        {
            instance = this;
            Camera = GetComponent<Camera>();
        }
    }
}
