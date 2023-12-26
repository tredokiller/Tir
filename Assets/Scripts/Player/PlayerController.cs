using Managers;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
        
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private bool canShoot = true;
        
        private InputManager _inputManager;
        private InputMap.PlayerActions _playerActions;

        private void Awake()
        {
            instance = this;
            
            _inputManager = InputManager.instance;
            _playerActions = _inputManager.PlayerActions;
        }

        private void Shoot()
        {
            if (canShoot)
            {
                weaponManager.Shoot();
            }
        }

        public void UpdateWeapon()
        {
            if (_playerActions.LeftMouse.IsPressed())
            {
                Shoot();
            } 
        }

        private void DisableShooting()
        {
            canShoot = false;
        }

        private void EnableShooting()
        {
            canShoot = true;
        }
    }
}
