using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Common
{
    public class SpaceRaycaster : MonoBehaviour
    {
        public static SpaceRaycaster instance;
        [SerializeField] private Camera cm;

        private const float MaxRayDistance = 1;

        private InputMap.PlayerActions _playerActions;
        private PlayerController _playerController;

        private bool _canUpdateWeapon = false;

        [Header("Masks")] 
        [SerializeField] private LayerMask interactableMask;

        private void Awake()
        {
            instance = this;
            _playerActions = InputManager.instance.PlayerActions;
        }

        private void Start()
        {
            _playerController = PlayerController.instance;
        }

        private void OnEnable()
        {
            _playerActions.LeftMouse.started += OnClick;
            _playerActions.LeftMouse.canceled += OnClickFinished;
        }

        public bool IsUIRaycast()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                return true;
            }

            return false;
        }
       

        private void OnClick(InputAction.CallbackContext obj)
        {
            _canUpdateWeapon = false;

            if (IsUIRaycast())
            {
                return;
            }
            
            var interactData = GetInteractData();
            RaycastHit hit;
            if (Physics.Raycast(interactData.startPosition, interactData.direction, out hit, 100f, interactableMask))
            {
                var interactable = hit.collider.gameObject.GetComponentInParent<IInteractable>();
                
                interactable.Interact();
            }
            else
            {
                _canUpdateWeapon = true;
            }
        }

        private void OnClickFinished(InputAction.CallbackContext obj)
        {
            _canUpdateWeapon = false;
        }

        public InteractData GetInteractData()
        {
            Ray ray = cm.ScreenPointToRay(Input.mousePosition);

            var targetPoint = ray.origin + ray.direction * MaxRayDistance;

            var data = new InteractData(targetPoint, ray.direction, new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0));

            data.startPosition = ray.origin;

            return data;
        }

        private void Update()
        {
            if (_canUpdateWeapon)
            {
                _playerController.UpdateWeapon();
            }
        }

        private void OnDisable()
        {
            _playerActions.LeftMouse.started -= OnClick;
            _playerActions.LeftMouse.canceled -= OnClickFinished;
        }
    }

    public class InteractData
    {
        public Vector3 position;
        public Vector3 direction;
        public Vector3 screenPosition;

        public Vector3 startPosition;

        public InteractData(Vector3 pos, Vector3 dir, Vector3 screenPos)
        {
            position = pos;
            direction = dir;
            screenPosition = screenPos;
        }
    }
}
