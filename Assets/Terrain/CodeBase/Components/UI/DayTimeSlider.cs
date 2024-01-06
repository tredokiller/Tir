using Terrain.CodeBase.Components.Time_Changing;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Components.UI
{
    public class DayTimeSlider : MonoBehaviour
    {
        [SerializeField] private DayNightChanger _dayNightChanger;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.value = _dayNightChanger.currentTime;
        }
        
        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(SetTimeOfDay);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(SetTimeOfDay);
        }

        private void SetTimeOfDay(float value)
        {
            _dayNightChanger.SetCurrentTime(value);
        }
    }
}