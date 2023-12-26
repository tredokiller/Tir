using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("Main")]
        [SerializeField] private int maxMagazineAmmo = 30;
        [SerializeField] private float reloadTime = 3.2f;
        [SerializeField] private float shotsPerSecond = 0.1f;
        
        [Space] 
        [SerializeField] private float bulletForce = 750; //AK-47 Basic Speed
        [SerializeField] private float bulletMass = 0.01f; //AK-47 Basic Bullet Mass
        [SerializeField] private GameObject bulletPrefab;

        [Space]
        [Header("Sounds")]
        [SerializeField] private AudioClip fireSound;
        [SerializeField] private AudioClip reloadSound;
        [SerializeField, CanBeNull] private AudioClip zeroAmmoSound;
        
        public int MaxMagazineAmmo => maxMagazineAmmo;
        public float ReloadTime => reloadTime;
        public float ShotsPerSecond => shotsPerSecond;
        public float BulletForce => bulletForce;
        public float BulletMass => bulletMass;
        public GameObject BulletPrefab => bulletPrefab;
        public AudioClip FireSound => fireSound;
        public AudioClip ReloadSound => reloadSound;
        public AudioClip ZeroAmmoSound => zeroAmmoSound;
        
    }
}
