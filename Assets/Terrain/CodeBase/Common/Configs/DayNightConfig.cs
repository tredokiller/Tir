using UnityEngine;

namespace CodeBase.Common.Configs
{
    [CreateAssetMenu(fileName = "DayNightConfig", menuName = "DayNight/DayNightConfig")]
    public class DayNightConfig : ScriptableObject
    {
        public int minSunRotationValue = -90;
        public int maxSunRotationValue = 270;
    }
}
