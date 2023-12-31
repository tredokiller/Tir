using System;
using CodeBase.Common.Configs;
using CodeBase.Common.Constants;
using CodeBase.Components.Sun_Component;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Components.DayNight_Changer
{
    public class DayNightChanger : MonoBehaviour
    {
        [Header("Sun")] 
        [SerializeField] private Sun sun;

        [Header("Parameters")] 
        [SerializeField] private DayNightConfig dayNightConfig;
        [SerializeField, Range(0, 24)] private float currentTime;
        [SerializeField] private float sunRotationSpeed;
        
        [Header("Lighting Presets")] 
        [SerializeField] private Gradient skyGradient;
        [SerializeField] private Gradient horizonGradient;
        [SerializeField] private Gradient sunGradient;

        private void Update()
        {
            SetCurrentTime();
            
            UpdateSunRotation();
            UpdateLightning();
        }

        private void OnValidate()
        {
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
        }
    }
}
