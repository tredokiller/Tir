using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Training", menuName = "Training/Difficulty", order = 1)]
    public class DifficultyData : ScriptableObject
    {
        [SerializeField] private TrainingDifficulty difficulty;
        public TrainingDifficulty Difficulty => difficulty;

        [SerializeField] private float timeToUpNewTarget;
        public float TimeToUpNewTarget => timeToUpNewTarget;

    }
}
