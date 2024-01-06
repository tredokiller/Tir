using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindSlider : MonoBehaviour
{
    public TerrainData[] terrains;
    
    private Slider _slider;
    
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetGrassWavingSpeed);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetGrassWavingSpeed);
    }

    private void SetGrassWavingSpeed(float value)
    {
        foreach (var terrain in terrains)
        {
            terrain.wavingGrassSpeed = value;
        }
    }
}
