using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;
        private InputMap _inputMap;

        public InputMap.PlayerActions PlayerActions { private set; get; }
        
        private void Awake()
        {
            instance = this;
            _inputMap = new InputMap();
            
            PlayerActions = _inputMap.Player;
        }

        private void OnEnable()
        {
            _inputMap.Enable();
        }

        private void OnDisable()
        {
            _inputMap.Disable();
        }
    }
}
