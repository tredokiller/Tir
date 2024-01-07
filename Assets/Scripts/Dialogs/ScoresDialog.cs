using System;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Dialogs
{
    public class ScoresDialog : DialogBase
    {
        [SerializeField] private RectTransform scoresTab;


        [Header("Scores")] 
        [SerializeField] private TextMeshProUGUI shotsText;
        [SerializeField] private TextMeshProUGUI hitsText;
        [SerializeField] private TextMeshProUGUI missesText;

        [SerializeField] private TextMeshProUGUI totalScores;

        [Space] 
        [SerializeField] private Button exitButton;

        private EventsManager _eventsManager;
        private LevelManager _levelManager;

        private void Awake()
        {
            _eventsManager = EventsManager.instance;
            _levelManager = LevelManager.instance;
        }

        private void OnEnable()
        {
            exitButton.onClick.AddListener(Hide);
        }

        private void SetText()
        {
            shotsText.text = "SHOTS - " + _levelManager.shots.ToString();
            hitsText.text = "HITS - " + _levelManager.hits.ToString();
            missesText.text = "MISSES - " + _levelManager.misses.ToString();
            totalScores.text = _levelManager.scores.ToString();
        }
        
        public override void Show()
        {
            gameObject.SetActive(true);
            SetText();
            
            var startPos = scoresTab.anchoredPosition;
            startPos.y = 1000f;

            scoresTab.anchoredPosition = startPos;

            scoresTab.DOAnchorPosY(0, 0.4f);
        }

        public override void Hide()
        {
            scoresTab.DOAnchorPosY(1000, 0.4f).OnComplete((() =>
            {
                gameObject.SetActive(false);
            }));
        }

        private void OnDisable()
        {
            exitButton.onClick.RemoveListener(Hide);
        }
    }
}
