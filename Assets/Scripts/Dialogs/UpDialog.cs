using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs
{
    [RequireComponent(typeof(RectTransform))]
    public class UpDialog : DialogBase
    {
        private RectTransform _rectTransform;

        [SerializeField] private Button menuButton;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            menuButton.onClick.AddListener(ExitToMenu);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            var startPos = _rectTransform.anchoredPosition;
            startPos.y = 50;

            _rectTransform.anchoredPosition = startPos;

            _rectTransform.DOAnchorPosY(-50, 0.4f);
        }

        public override void Hide()
        {
            _rectTransform.DOAnchorPosY(50, 0.4f).OnComplete((() =>
            {
                gameObject.SetActive(false);
            }));
        }

        private void ExitToMenu()
        {
            
        }

        private void OnDisable()
        {
            menuButton.onClick.RemoveListener(ExitToMenu);
        }
    }
}
