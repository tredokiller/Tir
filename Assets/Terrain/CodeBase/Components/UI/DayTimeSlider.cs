using System;
using Terrain.CodeBase.Components.Time_Changing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Components.UI
{
    public class DayTimeSlider : MonoBehaviour
    {
        [SerializeField] private DayNightChanger _dayNightChanger;
        [SerializeField] private TextMeshProUGUI _hoursText;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.value = _dayNightChanger.currentTime;
            ShowTime();
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
            ShowTime();
        }

        private void ShowTime()
        {
            TimeSpan timeOfDay = TimeSpan.FromHours(_dayNightChanger.currentTime);

            string formattedTime = timeOfDay.ToString("hh\\:mm");
            
            _hoursText.text = formattedTime;
        }
    }
}