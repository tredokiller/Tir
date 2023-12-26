using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs
{
    [RequireComponent(typeof(RectTransform))]
    public class LevelSettingsDialog : MonoBehaviour
    {
        [SerializeField] private Button showButton;
        [SerializeField] private Image arrowImage;

        private RectTransform _rectTransform;
        
        private bool _isActive;


        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            showButton.onClick.AddListener(InteractShowButton);
        }
        
        private void InteractShowButton()
        {
            if (_isActive)
            {
                HideDialog();
            }
            else
            {
                ShowDialog();
            }
        }

        private void ShowDialog()
        {
            _isActive = true;
            _rectTransform.DOAnchorPosY(500f, 0.5f);

            var newRotation = arrowImage.transform.rotation.eulerAngles;
            newRotation.z = 180;
            arrowImage.transform.DORotate(newRotation, 0.2f);
        }

        private void HideDialog()
        {
            _isActive = false;
            
            var newRotation = arrowImage.transform.rotation.eulerAngles;
            newRotation.z = 0;
            arrowImage.transform.DORotate(newRotation, 0.2f);
            
            _rectTransform.DOAnchorPosY(220f, 0.5f);
        }

        private void OnDisable()
        {
            showButton.onClick.RemoveListener(InteractShowButton);
        }
    }
}
