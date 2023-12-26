using System;
using Common;
using DG.Tweening;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask bulletMask;
        [SerializeField] private Transform raycastTransform;

        [Header("Audio")] 
        [SerializeField, CanBeNull] private AudioClip bulletSpawnSound; //Optional

        private float _bulletForce;
        private float _bulletMass;

        private const float ToDestroyTime = 5f;

        private Rigidbody _rigidbody;
        private InteractData _interactData;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(WeaponData weaponData)
        {
            _bulletForce = weaponData.BulletForce;
            _bulletMass = weaponData.BulletMass;

            DOVirtual.DelayedCall(ToDestroyTime, DestroyBullet);

            _rigidbody.mass = _bulletMass;
        }

        public void Push(InteractData interactData)
        {
            _interactData = interactData;
            
            transform.position = interactData.position;
            transform.rotation = Quaternion.LookRotation(interactData.direction);
            _rigidbody.velocity = transform.forward * _bulletForce;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, bulletMask))
            {
                var obj = hit.collider.GetComponentInParent<IDamageable>();

                PointData data = new PointData(hit.point, _interactData.screenPosition);

                obj.TakeDamage(data);
                DestroyBullet();
            }
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
        
        
    }
}
