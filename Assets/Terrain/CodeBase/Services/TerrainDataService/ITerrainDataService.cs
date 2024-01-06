using UnityEngine;

namespace CodeBase.Services.TerrainDataService
{
    public interface ITerrainDataService
    {
        TerrainData[] GetTerrainsData();
        void SetWavingGrassSpeed(float value);
    }
}