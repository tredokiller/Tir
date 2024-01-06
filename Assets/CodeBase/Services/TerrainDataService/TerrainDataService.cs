using UnityEngine;

namespace CodeBase.Services.TerrainDataService
{
    public class TerrainDataService : MonoBehaviour, ITerrainDataService
    {
        [SerializeField] private TerrainData[] _terrainsData;
        
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
    }
}