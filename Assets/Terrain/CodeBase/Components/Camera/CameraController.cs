using UnityEngine;

namespace Terrain.CodeBase.Components.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _baseMoveSpeed = 5f;
        [SerializeField] private float _rotateSpeed = 2f;
        [SerializeField] private float _ascentSpeed = 5f;
        [SerializeField] private float _sprintMultiplier = 2f;
        [SerializeField] private float _accelerationTime = 1.5f;
        
        private Vector3 _velocity;
        private float _currentMoveSpeed;
        
        private void Update()
        {
            MoveCamera();
            RotateCamera();
        }
        
        private void MoveCamera()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float sprintMultiplier = Input.GetKey(KeyCode.LeftShift) ? this._sprintMultiplier : 1f;

            var moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

            _currentMoveSpeed = Mathf.SmoothDamp(_currentMoveSpeed, _baseMoveSpeed * sprintMultiplier, ref _velocity.y,
                _accelerationTime);

            transform.Translate(moveDirection * _currentMoveSpeed * Time.deltaTime, Space.Self);

            float ascentInput = Input.GetKey(KeyCode.Q) ? -1f : Input.GetKey(KeyCode.E) ? 1f : 0f;

            var ascentDirection = new Vector3(0f, ascentInput, 0f).normalized;
            transform.Translate(ascentDirection * _ascentSpeed * Time.deltaTime, Space.World);
        }

        private void RotateCamera()
        {
            float mouseX = Input.GetAxis("Mouse X") * _rotateSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * _rotateSpeed;

            transform.Rotate(Vector3.up, mouseX);

            transform.Rotate(Vector3.right, mouseY);

            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y, 0f);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Application.focusChanged -= OnApplicationFocus;
            }
        }
    }
}