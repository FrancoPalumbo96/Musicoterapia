using System;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


public class Commands : MonoBehaviour
{
    private VideoShifter _videoShifter;
    [SerializeField] private SphereRotation _sphere;

    private void Start()
    {
        _videoShifter = FindObjectOfType<VideoShifter>();
    }

    private void VideoUp()
    {
        _videoShifter.VideoUp();
    }

    private void VideoDown()
    {
        _videoShifter.VideoDown();
    }

    public void VideoToggle()
    {
        _videoShifter.VideoUp();
    }

    private void EndExperience()
    {
        _videoShifter.StopExperience();
    }

    private void StartExperience()
    {
        _videoShifter.StartExperience();
    }

    private bool _experienceRunning = false;
    public void ToggleExperience()
    {
        if (_experienceRunning)
        {
            EndExperience();
            _experienceRunning = false;
        }
        else
        {
            StartExperience();
            _experienceRunning = true;
        }
    }

    public void AngleDown()
    {
        _sphere.RotateDown();
    }
    
    
    public void AngleUp()
    {
        _sphere.RotateUp();
    }
}
