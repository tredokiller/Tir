using System;
using DG.Tweening;
using Interfaces;
using Managers;
using UnityEngine;

public class RotateArrow : MonoBehaviour, IInteractable
{
    private EventsManager _eventsManager;
    private CameraManager _cameraManager;
    
    private bool _isDown;

    [SerializeField] private Transform elementsTransform;
    [SerializeField] private float angleToMove = 0;

    [SerializeField, Range(0, 100)] private float newCameraFOV = 60;

    private void Awake()
    {
        _eventsManager = EventsManager.instance;
        _cameraManager = CameraManager.instance;
    }

    private void OnEnable()
    {
        _eventsManager.OnTrainingIsReady += Down;
        _eventsManager.OnTrainingIsFinished += Up;
    }

    private void Start()
    {
        Up();
    }

    private void Up()
    {
        var newRotation = elementsTransform.transform.localRotation.eulerAngles;
        newRotation.x = 0f;

        elementsTransform.transform.DOLocalRotate(newRotation, 0.5f);
        
        
        _isDown = false;
    }

    private void DownWithCooldown()
    {
        Down();
        DOVirtual.DelayedCall(2, Up);
    }
    
    
    private void Down()
    {
        var newRotation = elementsTransform.transform.localRotation.eulerAngles;
        newRotation.x = 90f;
        
        elementsTransform.transform.DOLocalRotate(newRotation, 0.5f);
        _isDown = true;
    }

    private void RotateCamera()
    {
        var newRotation = _cameraManager.Camera.transform.localRotation.eulerAngles;
        newRotation.y = angleToMove;
        
        _cameraManager.Camera.transform.DORotate(newRotation, 0.5f);
    }
    
    private void OnDisable()
    {
        _eventsManager.OnTrainingIsFinished -= Up;
        _eventsManager.OnTrainingIsReady -= Down;
    }

    public void Interact()
    {
        if (_isDown == false)
        {
            DownWithCooldown();
            RotateCamera();
        }
    }
}
