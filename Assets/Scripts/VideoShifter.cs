using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoShifter : MonoBehaviour
{
    public VideoClip[] clips = new VideoClip[1];
    public Sprite[] images = new Sprite[1];
    public string[] names = new string[1];
    public Text videoName;
    public Image preview;
    private int _currentClip;
    public Material experienceSelection;
    public Material video360Material;

    public VideoPlayer player;

    public void Start()
    {
        RenderSettings.skybox = experienceSelection;
    }

    public void StartExperience()
    {
        player.clip = clips[_currentClip];
        player.Play();
        Destroy(gameObject);
        RenderSettings.skybox = video360Material;
    }

    public void VideoUp()
    {
        if (_currentClip == clips.Length - 1) _currentClip = 0;
        else _currentClip++;
        UpdateUI();
    }

    public void VideoDown()
    {
        if (_currentClip == 0) _currentClip = clips.Length - 1;
        else _currentClip--;
        UpdateUI();
    }

    public void UpdateUI()
    {
        preview.sprite = images[_currentClip];
    }
}
