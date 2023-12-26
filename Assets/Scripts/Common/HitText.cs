using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Common
{
    public class HitText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [Header("Colors")] 
        [SerializeField] private Color defaultHit;
        [SerializeField] private Color criticalHit;

        [SerializeField] private float maxHeight;
        [SerializeField] private float liveTime = 1.5f;

        public void CreateText(Vector3 position, string text, bool isCritical)
        {
            this.text.text = text;
            print(Screen.width);

            var currentColor = defaultHit;
            if (isCritical)
            {
                currentColor = criticalHit;
            }
            this.text.color = currentColor;
            
            
            this.text.rectTransform.anchoredPosition = position;
            this.text.rectTransform.DOAnchorPosY(this.text.rectTransform.anchoredPosition.y + maxHeight, 0.2f);

            var currentColorTransparent = currentColor;
            currentColorTransparent.a = 0f;
            
            this.text.DOColor(currentColor, 0.2f).From(currentColorTransparent);
            this.text.DOColor(currentColorTransparent, 0.2f).SetDelay(liveTime-0.2f);
            DOVirtual.DelayedCall(liveTime, () => Destroy(gameObject));
        }
    }
}
