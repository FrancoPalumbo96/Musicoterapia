using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoShifter : MonoBehaviour
{
    public VideoClip[] clips = new VideoClip[1];
    public Sprite[] images = new Sprite[1];
    public Image preview;
    private int _currentClip;

    public VideoPlayer videoPlayer;
    public GameObject backButton;

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
        videoPlayer.loopPointReached += EndReached;
    }


    public void StartExperience()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = clips[_currentClip];
        videoPlayer.Play();
        gameObject.SetActive(false);
        backButton.SetActive(true);
    }

    public void StopExperience()
    {
        videoPlayer.gameObject.SetActive(false);
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
        gameObject.SetActive(true);
        int id = PlayerPrefs.GetInt(userName, -1);

        Debug.Log("Saved ID: " + id);
        Debug.Log("Is this the time: " + videoPlayer.time);
        
        String videoName = clips[_currentClip].name;
        if (videoName.Length >= 50) videoName = videoName.Substring(0, 49);
        _apiDataController.updateUserDataPOST(id, videoName, "No song", (int) videoPlayer.time);
    }
}