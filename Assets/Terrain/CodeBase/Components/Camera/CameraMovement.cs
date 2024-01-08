using UnityEngine;

namespace Terrain.CodeBase.Components.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        public float baseMoveSpeed = 5f;
        public float rotateSpeed = 2f;
        public float ascentSpeed = 5f;
        public float sprintMultiplier = 2f;
        public float accelerationTime = 1.5f;

        private float currentMoveSpeed;
        private Vector3 velocity;
        bool cursorLocked = true;

        void Update()
        {
            // Проверка нажатия Escape для освобождения/показа курсора
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleCursorLock();
            }

            // Передвижение камеры
            MoveCamera();

            // Вращение камеры
            RotateCamera();
        }

        void ToggleCursorLock()
        {
            cursorLocked = !cursorLocked;

            if (cursorLocked)
            {
                LockCursor();
            }
            else
            {
                UnlockCursor();
            }
        }

        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Application.focusChanged += OnApplicationFocus;
        }

        void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Application.focusChanged -= OnApplicationFocus;
        }
        void OnApplicationFocus(bool hasFocus)
        {
            // Сбрасываем захват курсора и видимость курсора при потере фокуса
            if (!hasFocus)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Application.focusChanged -= OnApplicationFocus;
            }
        }

        void MoveCamera()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float sprintMultiplier = Input.GetKey(KeyCode.LeftShift) ? this.sprintMultiplier : 1f;

            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

            currentMoveSpeed = Mathf.SmoothDamp(currentMoveSpeed, baseMoveSpeed * sprintMultiplier, ref velocity.y,
                accelerationTime);

            transform.Translate(moveDirection * currentMoveSpeed * Time.deltaTime, Space.Self);

            float ascentInput = Input.GetKey(KeyCode.Q) ? -1f : Input.GetKey(KeyCode.E) ? 1f : 0f;

            Vector3 ascentDirection = new Vector3(0f, ascentInput, 0f).normalized;
            transform.Translate(ascentDirection * ascentSpeed * Time.deltaTime, Space.World);
        }

        void RotateCamera()
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * rotateSpeed; // Инвертируем для корректного вращения по вертикали

            // Вращение камеры вокруг оси Y (горизонталь)
            transform.Rotate(Vector3.up, mouseX);

            // Вращение камеры вокруг оси X (вертикаль)
            transform.Rotate(Vector3.right, mouseY);

            // Убери вращение по Z (зануляем Z-компоненту вращения)
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y, 0f);
        }
    }
}