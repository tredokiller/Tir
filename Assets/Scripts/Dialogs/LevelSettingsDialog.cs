 using System;
using DG.Tweening;
using Managers;
 using Scriptables;
 using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs
{
    [RequireComponent(typeof(RectTransform))]
    public class LevelSettingsDialog : DialogBase
    {
        [Header("UpDialog")] 
        [SerializeField] private UpDialog upDialog;
        
        [Header("ShowButton")]
        [SerializeField] private Button showButton;
        [SerializeField] private Image arrowImage;

        [Header("Training")] 
        [SerializeField] private Button timeButton;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private StartTrainingTimerDialog visualStartTrainingTimer;

        [SerializeField] private Button stopTrainingButton;
        
        private readonly float[] _timeTraining = {30, 60, 120};
        private int _currentTrainingTimeIndex;
        
        [SerializeField] private Button difficultyButton;
        [SerializeField] private TextMeshProUGUI difficultyText;
        
        [SerializeField] private Button startTrainingButton;

        private readonly TrainingDifficulty[] _trainingDifficulties =
            { TrainingDifficulty.Easy, TrainingDifficulty.Normal, TrainingDifficulty.Hard, TrainingDifficulty.Insane };

        private int _currentDifficultyIndex;
        
        private RectTransform _rectTransform;
        
        private bool _isActive;
        
        private EventsManager _eventsManager;
        private LevelManager _levelManager;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _eventsManager = EventsManager.instance;
        }

        private void Start()
        {
            _levelManager = LevelManager.instance;
            stopTrainingButton.gameObject.SetActive(false);
            UpdateButtonsText();
        }

        private void OnEnable()
        {
            startTrainingButton.onClick.AddListener(StartTraining);
            
            showButton.onClick.AddListener(InteractShowButton);
            stopTrainingButton.onClick.AddListener(StopTraining);
            
            timeButton.onClick.AddListener(ChangeTime);
            difficultyButton.onClick.AddListener(ChangeDifficulty);
        }
        
        private void InteractShowButton()
        {
            if (_isActive)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void ChangeDifficulty()
        {
            _currentDifficultyIndex += 1;
            if (_currentDifficultyIndex >= _trainingDifficulties.Length)
            {
                _currentDifficultyIndex = 0;
            }
            
            UpdateButtonsText();
        }

        private void ChangeTime()
        {
            _currentTrainingTimeIndex += 1;
            if (_currentTrainingTimeIndex >= _timeTraining.Length)
            {
                _currentTrainingTimeIndex = 0;
            }
            
            UpdateButtonsText();
        }

        private void StartTraining()
        {
            var trainingData = new TrainingData
            {
                trainingTime = _timeTraining[_currentTrainingTimeIndex],
                difficulty = _trainingDifficulties[_currentDifficultyIndex]
            };

            stopTrainingButton.gameObject.SetActive(true);
            _levelManager.StartTraining(trainingData);
            visualStartTrainingTimer.Show();
            Hide();
        }

        private void StopTraining()
        {
            stopTrainingButton.gameObject.SetActive(false);
            visualStartTrainingTimer.Hide();
            _levelManager.FinishTraining();
        }

        public override void Show()
        {
            upDialog.Show();
            
            _isActive = true;
            _rectTransform.DOAnchorPosY(500f, 0.5f);

            var newRotation = arrowImage.transform.rotation.eulerAngles;
            newRotation.z = 180;
            arrowImage.transform.DORotate(newRotation, 0.2f);
        }

        public override void Hide()
        {
            upDialog.Hide();
            
            _isActive = false;
            
            var newRotation = arrowImage.transform.rotation.eulerAngles;
            newRotation.z = 0;
            arrowImage.transform.DORotate(newRotation, 0.2f);
            
            _rectTransform.DOAnchorPosY(220f, 0.5f);
        }

        private void OnDisable()
        {
            startTrainingButton.onClick.RemoveListener(StartTraining);
            
            showButton.onClick.RemoveListener(InteractShowButton);
            stopTrainingButton.onClick.RemoveListener(StopTraining);
            
            timeButton.onClick.RemoveListener(ChangeTime);
            difficultyButton.onClick.RemoveListener(ChangeDifficulty);
        }

        private void UpdateButtonsText()
        {
            if (_currentTrainingTimeIndex == 0)
            {
                timeText.text = "30 SECONDS";
            }
            else if(_currentTrainingTimeIndex == 1)
            {
                timeText.text = "1 MINUTE";
            }
            else if (_currentTrainingTimeIndex == 2)
            {
                timeText.text = "2 MINUTES";
            }

            difficultyText.text = _trainingDifficulties[_currentDifficultyIndex].ToString().ToUpper();
        }
    }
}


public enum TrainingDifficulty
{
    Easy,
    Normal,
    Hard,
    Insane
}


public class TrainingData
{
    public DifficultyData difficultyData;
    public TrainingDifficulty difficulty;
    public float trainingTime;
}