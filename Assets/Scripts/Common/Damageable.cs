using UnityEngine;

namespace Common
{
    public static class Damageable
    {
        public static bool IsCritical(float damage)
        {
            if (damage >= 75)
            {
                return true;
            }

            return false;
        }
    }
}
