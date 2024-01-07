using UnityEngine;

namespace CodeBase.Common.Configs
{
    [CreateAssetMenu(fileName = "TerrainsConfig", menuName = "Terrains/TerrainsConfig")]
    public class TerrainsConfig : ScriptableObject
    {
        public float defaultGrassWavingSpeed = 0.5f;
    }
}