using CodeBase.Common.Configs;
using CodeBase.Common.Constants;
using CodeBase.Components.Sun_Component;
using UnityEngine;

namespace CodeBase.Components.Time_Changing
{
    [ExecuteInEditMode]
    public class DayNightChanger : MonoBehaviour
    {
        [Header("Sun")] 
        [SerializeField] private Sun sun;

        [Header("Parameters")] 
        [SerializeField] private DayNightConfig dayNightConfig;
        [SerializeField] private float sunRotationSpeed;

        [Header("Lighting Presets")] 
        [SerializeField] private Gradient skyGradient;
        [SerializeField] private Gradient horizonGradient;
        [SerializeField] private Gradient sunGradient;
        [field: SerializeField, Range(0, 24)] public float currentTime { get; private set; }

        public bool isAuto;

        private void Update()
        {
            if (isAuto)
            {
                SetCurrentTime();
            }
        }

        private void OnValidate()
        {
            UpdateSunRotation();
            UpdateLightning();
        }
        
        public void SetCurrentTime(float value)
        {
            if (currentTime > DayTime.DayHours)
            {
                currentTime = 0;
                return;
            }

            currentTime = value;
            
            UpdateSunRotation();
            UpdateLightning();
        }

        private void UpdateSunRotation()
        {
            var sunRotation = Mathf.Lerp(dayNightConfig.minSunRotationValue, dayNightConfig.maxSunRotationValue,
                currentTime / DayTime.DayHours);

            RotateSun(sunRotation);
        }

        private void UpdateLightning()
        {
            var timeFraction = currentTime / DayTime.DayHours;

            SetLightGradients(timeFraction);
        }

        private void RotateSun(float rotation)
        {
            sun.RotateSun(rotation);
        }
        
        private void SetLightGradients(float timeFraction)
        {
            RenderSettings.ambientSkyColor = skyGradient.Evaluate(timeFraction);
            RenderSettings.ambientEquatorColor = horizonGradient
                .Evaluate(timeFraction);

            if (sun == null)
                return;
            sun.SetColor(sunGradient.Evaluate(timeFraction));
        }

        private void SetCurrentTime()
        {
            if (currentTime > DayTime.DayHours)
            {
                currentTime = 0;
                return;
            }

            currentTime += Time.deltaTime * sunRotationSpeed;
            
            UpdateSunRotation();
            UpdateLightning();
        }
    }
}