using System;
using UnityEngine;

namespace Weapons
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;
        public float Damage => damage;
        
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public bool IsInDamageZone(Vector3 point)
        {
            return _boxCollider.bounds.Contains(point);
        }
    }
}
