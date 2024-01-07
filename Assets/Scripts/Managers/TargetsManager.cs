using System;
using DG.Tweening;
using Environment.Target;
using UnityEngine;

namespace Managers
{
    public class TargetsManager : MonoBehaviour
    {
        public static TargetsManager instance;
        
        [SerializeField] private TargetsPart[] allTargetParts;
        private TargetsPart _currentTargetsPart;

        private EventsManager _eventsManager;
        private CameraManager _cameraManager;

        private TrainingData _currentTrainingData;

        private Tween _timerTween;

        private void Awake()
        {
            instance = this;
            
            _eventsManager = EventsManager.instance;
            _cameraManager = CameraManager.instance;
        }

        private void UpdateCurrentTargetsData()
        {
            foreach (var targetsPart in allTargetParts)
            {
                if (targetsPart.IsCorrectTargetPart(_cameraManager.Camera.transform.eulerAngles.y))
                {
                    _currentTargetsPart = targetsPart;
                    return;
                }
            }
        }
        
        public void StartTraining(TrainingData data)
        {
            UpdateCurrentTargetsData();

            _currentTrainingData = data;
            
            if (_currentTargetsPart != null)
            {
                _currentTargetsPart.DownAllTargets();
                ResetTimerToUpTarget();
            }

            Target.OnTargetDamaged += ResetTimerToUpTarget;
        }

        private void ResetTimerToUpTarget()
        {
            _timerTween.Kill();
            _currentTargetsPart.UpRandomTarget(_currentTrainingData.difficultyData.TimeToUpNewTarget);
                
            _timerTween = DOVirtual.DelayedCall(_currentTrainingData.difficultyData.TimeToUpNewTarget, () => _currentTargetsPart.UpRandomTarget(_currentTrainingData.difficultyData.TimeToUpNewTarget)).SetLoops(-1);
        }

        public void StopTraining()
        {
            _timerTween.Kill();
            DOTween.Kill(this);
            
            if (_currentTargetsPart != null)
            {
                _currentTargetsPart.UpAllTargets();
            }

            Target.OnTargetDamaged -= ResetTimerToUpTarget;
        }
    }
}


[Serializable]
public class TargetsData
{ 
    [Range(0, 360)] public int cameraYAngle;
    public Target[] targets;
}