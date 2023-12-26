using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(PointData pointData, DamageType damageType = DamageType.DefaultBullet);
    }

    public enum DamageType
    {
        MachineBullet,
        DefaultBullet
    }

    public class PointData
    {
        public Vector3 damagePosition;
        public Vector3 uiPosition;

        public PointData(Vector3 damagePos, Vector3 uiPos)
        {
            damagePosition = damagePos;
            uiPosition = uiPos;
        }
    }
}
