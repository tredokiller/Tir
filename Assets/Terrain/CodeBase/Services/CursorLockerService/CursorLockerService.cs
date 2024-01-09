using UnityEngine;

namespace Terrain.CodeBase.Components.CursorLocker
{
    public class CursorLockerService : MonoBehaviour
    {
        private bool _isCursorLocked = true;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleCursorLock();
            }
        }

        public void ToggleCursorLock()
        {
            _isCursorLocked = !_isCursorLocked;

            if (_isCursorLocked)
            {
                LockCursor();
            }
            else
            {
                UnlockCursor();
            }
        }

        public void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Application.focusChanged += OnApplicationFocus;
        }

        public void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Application.focusChanged -= OnApplicationFocus;
        }
    }
}