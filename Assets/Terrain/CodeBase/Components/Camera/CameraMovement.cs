using UnityEngine;

namespace Terrain.CodeBase.Components.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        public float normalSpeed = 10f;
        public float acceleration = 2f;
        public float deceleration = 2f;
        public float rotationSpeed = 100.0f;
        public float minY = 1.0f;
        public float maxY = 80.0f;

        private float currentSpeed;
        private float rotationX = 0;

        void Update()
        {
            HandleMovementInput();
            HandleRotationInput();
        }

        void HandleMovementInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            float speed = normalSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed *= acceleration;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.position -= transform.up * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.position += transform.up * speed * Time.deltaTime;
            }

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            Vector3 targetPosition = transform.position + transform.TransformDirection(moveDirection) * speed * Time.deltaTime;
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            transform.position = targetPosition;
        }

        void HandleRotationInput()
        {
            rotationX -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -90, 90);
            UnityEngine.Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0);
        }
    }
}

