using CodeBase.Services.TerrainDataService;
using Terrain.CodeBase.Common.Constants;
using UnityEngine;

namespace Terrain.CodeBase.Services.TerrainDataService
{
    public class TerrainDataService : MonoBehaviour, ITerrainDataService
    {
        [SerializeField] private TerrainData[] _terrainsData;
        
        [SerializeField] private Material[] _treeMaterials; 
        
        public TerrainData[] GetTerrainsData()
        {
            return _terrainsData;
        }
        
        public void SetWavingGrassSpeed(float value)
        {
            foreach (var terrain in _terrainsData)
            {
                terrain.wavingGrassSpeed = value;
            }
        }
        
        public void SetWavingGrassStrength(float value)
        {
            foreach (var terrain in _terrainsData)
            {
                terrain.wavingGrassStrength = value;
            }
        }
        
        public void SetWavingGrassAmount(float value)
        {
            foreach (var terrain in _terrainsData)
            {
                terrain.wavingGrassAmount = value;
            }
        }

        public void SetTreesWind(float value)
        {
            foreach (var material in _treeMaterials)
            {
                material.SetFloat(TerrainProperties.TumbleStrength, value);
                material.SetFloat(TerrainProperties.LeafTurbulence, Mathf.Clamp(value, 0, 4));
            }
        }
    }
}