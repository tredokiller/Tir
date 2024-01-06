using CodeBase.Common.Configs;
using CodeBase.Services.TerrainDataService;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Components.UI
{
    public class WindSlider : MonoBehaviour
    {
        [SerializeField] private TerrainDataService _terrainDataService;
        [SerializeField] private TerrainsConfig _terrainsConfig;
        
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            
            SetDefaultValue();
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(SetGrassParameters);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(SetGrassParameters);
        }

        private void SetDefaultValue()
        {
            _slider.value = _terrainsConfig.defaultGrassWavingSpeed;
            _terrainDataService.SetWavingGrassSpeed(_terrainsConfig.defaultGrassWavingSpeed);
        }

        private void SetGrassParameters(float value)
        {
            _terrainDataService.SetWavingGrassSpeed(value);
            _terrainDataService.SetWavingGrassStrength(value);
            _terrainDataService.SetWavingGrassAmount(value);
        }
    }
}