using System;
using Common;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        
        [SerializeField] private GameObject hitTextPrefab;

        private void Awake()
        {
            instance = this;
        }

        public void CreateHitText(Vector3 position, string text, bool isCritical)
        {
            var hitText = Instantiate(hitTextPrefab, transform);
            hitText.GetComponent<HitText>().CreateText(position, text, isCritical);
        }
    }
}
