using System;
using Common;
using Dialogs;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        
        [SerializeField] private GameObject hitTextPrefab;

        private EventsManager _eventsManager;

        [Header("Dialogs")] 
        [SerializeField] private ScoresDialog scoresDialog;
        [SerializeField] private LevelSettingsDialog levelSettingsDialog;

        private void Awake()
        {
            instance = this;
            _eventsManager = EventsManager.instance;
        }

        private void OnEnable()
        {
            _eventsManager.OnTrainingIsFinished += ShowScoresDialog;
        }

        private void ShowScoresDialog()
        {
            scoresDialog.Show();
        }
        

        public void CreateHitText(Vector3 position, string text, bool isCritical)
        {
            var hitText = Instantiate(hitTextPrefab, transform);
            hitText.GetComponent<HitText>().CreateText(position, text, isCritical);
        }
    }
}
