using UnityEngine;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Weapon weapon;

        private void Start()
        {
            SetWeaponData(weaponData);
        }

        public void Reload()
        {
            weapon.Reload();
        }
        
        public void Shoot()
        {
            weapon.Shoot();
        }

        private void SetWeaponData(WeaponData newWeaponData)
        {
            weapon.SetWeaponData(newWeaponData);
        }
    }
}
