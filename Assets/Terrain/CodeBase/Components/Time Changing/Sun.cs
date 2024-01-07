using UnityEngine;
using Color = UnityEngine.Color;

namespace CodeBase.Components.Sun_Component
{
    [ExecuteInEditMode]
    public class Sun : MonoBehaviour
    {
        private Light _sunLight;

        private void Awake()
        {
            _sunLight = GetComponent<Light>();
        }

        public void RotateSun(float rotation)
        {
            if (_sunLight == null)
            {
                return;
            }

            _sunLight.transform.rotation =
                Quaternion.Euler(rotation, _sunLight.transform.rotation.y, _sunLight.transform.rotation.z);
        }

        public void SetColor(Color targetColor)
        {
            if (_sunLight == null)
                return;
            _sunLight.color = targetColor;
        }
    }
}