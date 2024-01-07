using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;

namespace Dialogs
{
    public class StartTrainingTimerDialog : DialogBase
    {
        [SerializeField] private TextMeshProUGUI counterText;
        
        public override void Show()
        {
            gameObject.SetActive(true);
            
            counterText.rectTransform.anchoredPosition = Vector2.zero;

            counterText.text = "3";
            counterText.rectTransform.DOScale(1.25f, 0.1f);
            counterText.rectTransform.DOScale(1f, 0.1f).SetDelay(0.1f);

            DOVirtual.DelayedCall(1f, () =>
            {
                counterText.text = "2";
                counterText.rectTransform.DOScale(1.25f, 0.1f);
                counterText.rectTransform.DOScale(1f, 0.1f).SetDelay(0.1f);
            });
            
            DOVirtual.DelayedCall(2f, () =>
            {
                counterText.text = "1";
                counterText.rectTransform.DOScale(1.25f, 0.1f);
                counterText.rectTransform.DOScale(1f, 0.1f).SetDelay(0.1f);
            });
            
            DOVirtual.DelayedCall(3f, () =>
            {
                counterText.text = "GO!";
                counterText.rectTransform.DOScale(1.5f, 0.4f);
                counterText.rectTransform.DOScale(1f, 0.1f).SetDelay(0.4f).OnComplete(() =>
                {
                    EventsManager.instance.SendOnTrainingIsStarted();
                    Hide();
                });
            });
        }

        public override void Hide()
        {
            DOTween.Kill(this);
            gameObject.SetActive(false);
        }
    }
}
