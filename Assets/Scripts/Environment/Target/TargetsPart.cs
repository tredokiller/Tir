using DG.Tweening;
using UnityEngine;

namespace Environment.Target
{
    public class TargetsPart : MonoBehaviour
    {
        [SerializeField] private TargetsData targetsData;
        
        public void UpRandomTarget(float timeToDown)
        {
            var randomTarget = targetsData.targets[Random.Range(0, targetsData.targets.Length)];
            randomTarget.UpWithTimer(timeToDown);
        }

        public void UpAllTargets()
        {
            foreach (var target in targetsData.targets)
            {
                target.Up();
                target.Reset();
            }
        }

        public void DownAllTargets()
        {
            foreach (var target in targetsData.targets)
            {
                target.Down();
            }
        }

        public bool IsCorrectTargetPart(float currentAngle)
        {
            if (targetsData.cameraYAngle == (int)currentAngle)
            {
                return true;
            }

            return false;
        }
    }
}
