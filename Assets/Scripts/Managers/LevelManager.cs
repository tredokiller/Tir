using DG.Tweening;
using Scriptables;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        
        [Header("Training")] 
        [SerializeField] private DifficultyData[] trainingDifficultyDates;

        public bool IsTraining { private set; get; }

        public int scores;
        public int hits;
        public int misses;
        public int shots;

        public TrainingData CurrentTrainingData { private set; get; }

        private EventsManager _eventsManager;
        private TargetsManager _targetsManager;

        private void Awake()
        {
            instance = this;
            _eventsManager = EventsManager.instance;
        }

        private void Start()
        {
            _targetsManager = TargetsManager.instance;
        }

        public void StartTraining(TrainingData data)
        {
            scores = 0;
            hits = 0;
            misses = 0;
            shots = 0;
            
            IsTraining = true;

            CurrentTrainingData = data;
            CurrentTrainingData.difficultyData = GetTrainingDifficultyData(CurrentTrainingData.difficulty);
            
            DOVirtual.DelayedCall(3, (() =>
            {
                _targetsManager.StartTraining(CurrentTrainingData);
            }));

            _eventsManager.SendOnTrainingIsReady();

            DOVirtual.DelayedCall(CurrentTrainingData.trainingTime, FinishTraining);
        }

        public void FinishTraining()
        {
            if (IsTraining)
            {
                DOTween.Kill(this);

                IsTraining = false;

                _targetsManager.StopTraining();

                misses = shots - hits;

                _eventsManager.SendOnTrainingIsFinished();
            }
        }

        private DifficultyData GetTrainingDifficultyData(TrainingDifficulty difficulty)
        {
            foreach (var data in trainingDifficultyDates)
            {
                if (data.Difficulty == difficulty)
                {
                    return data;
                }
            }

            return null;
        }
    }
}
