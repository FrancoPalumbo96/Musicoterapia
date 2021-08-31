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

    public VideoPlayer videoPlayer;
    public GameObject backButton;

    //TODO sacar MusicLoader de aca, hacerlo lindo-> probablemente con eventos
    public MusicLoader musicLoader;

    private ApiDataController _apiDataController;

    //private String userName = "nombre_de_usuario";
    public String userName = "Default";

    private void Awake()
    {
        //TODO Refactor -> hacer esto en otro lado 
        //TODO get userName in game

        _apiDataController = FindObjectOfType<ApiDataController>();
        userName = parseUserName(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt(userName, -1) == -1)
        {
            Debug.Log("New User");
            _apiDataController.createUser(userName);
        }
    }

    private String parseUserName(String userName)
    {
        if (userName == null)
        {
            return "default";
        }

        String[] breakApart = userName.Split('\\');
        return breakApart[breakApart.Length - 1];
    }

    public void Start()
    {
        RenderSettings.skybox = experienceSelection;
        videoPlayer.loopPointReached += EndReached;
    }


    public void StartExperience()
    {
        videoPlayer.clip = clips[_currentClip];
        videoPlayer.Play();
        RenderSettings.skybox = video360Material;
        gameObject.SetActive(false);
        backButton.SetActive(true);
    }

    public void StopExperience()
    {
        RenderSettings.skybox = experienceSelection;
        gameObject.SetActive(true);
        backButton.SetActive(false);
        EndReached(videoPlayer);
        videoPlayer.Stop();
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

    //TODO refactor
    private void EndReached(VideoPlayer source)
    {
        RenderSettings.skybox = experienceSelection;
        gameObject.SetActive(true);
        musicLoader.stopSong();
        int id = PlayerPrefs.GetInt(userName, -1);

        Debug.Log("Saved ID: " + id);

        Debug.Log("Is this the time: " + videoPlayer.time);
        String videoName = clips[_currentClip].name;
        if (videoName.Length >= 50) videoName = videoName.Substring(0, 49);
        /*Debug.Log("Video name: " + clips[_currentClip].name);
        Debug.Log("Music name: " + musicLoader.getTitleName());*/
        _apiDataController.updateUserDataPOST(id, videoName, musicLoader.getTitleName(), (int) videoPlayer.time);
    }
}