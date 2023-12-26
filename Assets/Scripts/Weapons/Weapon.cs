using Common;
using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        private WeaponData _weaponData;

        private SpaceRaycaster _spaceRaycaster;
        
        private int _currentAmmo;
        private bool _isReloading;
        private bool _isCoolDownBetweenShots;

        public void Init()
        {
            _currentAmmo = _weaponData.MaxMagazineAmmo;
            _isReloading = false;
        }

        private void Start()
        {
            _spaceRaycaster = SpaceRaycaster.instance;
        }

        public void Shoot()
        {
            if (CanShoot())
            {
                _currentAmmo -= 1;
                SpawnBullet();
                
                _isCoolDownBetweenShots = true;
                DOVirtual.DelayedCall( 60/ _weaponData.ShotsPerSecond, () => _isCoolDownBetweenShots = false);
            }
        }
        
        public void Reload()
        {
            _isReloading = true;
            DOVirtual.DelayedCall(_weaponData.ReloadTime, OnReloaded);
        }
        
        private void OnReloaded()
        {
            _isReloading = false;
            _currentAmmo = _weaponData.MaxMagazineAmmo;
        }
        private bool CanShoot()
        {
            if (_currentAmmo <= 0)
            {
                Reload();
            }
            if (_isReloading || _isCoolDownBetweenShots)
            {
                return false;
            }

            return true;
        }
        
        private void SpawnBullet()
        {
            var interactData = _spaceRaycaster.GetInteractData();
            var bullet = Instantiate(_weaponData.BulletPrefab).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);

            bullet.Init(_weaponData);
            bullet.Push(interactData);
        }

        public void SetWeaponData(WeaponData newWeaponData)
        {
            _weaponData = newWeaponData;
            Init();
        }
        
        
    }
}
