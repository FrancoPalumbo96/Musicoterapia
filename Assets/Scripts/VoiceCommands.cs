using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class VoiceCommands : MonoBehaviour
{
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer _keywordRecognizer;
    private VideoShifter _videoShifter;
    [SerializeField] private SphereRotation _sphere;

    private void Start()
    {
        _videoShifter = FindObjectOfType<VideoShifter>();
        keywordActions.Add("Arriba",AngleUp);
        keywordActions.Add("Up",AngleUp);
        keywordActions.Add("Abajo",AngleDown);
        keywordActions.Add("Down",AngleDown);
        keywordActions.Add("Frenar", EndExperience);
        keywordActions.Add("Stop", EndExperience);
        keywordActions.Add("Comenzar", StartExperience);
        keywordActions.Add("Start", StartExperience);
        keywordActions.Add("Siguiente",VideoUp);
        keywordActions.Add("Next",VideoUp);
        keywordActions.Add("Anterior",VideoDown);
        keywordActions.Add("Back",VideoDown);
        _keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        _keywordRecognizer.Start();
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void VideoUp()
    {
        _videoShifter.VideoUp();
    }
    
    private void VideoDown()
    {
        _videoShifter.VideoDown();
    }

    private void EndExperience()
    {
        _videoShifter.StopExperience();
    }
    
    private void StartExperience()
    {
        _videoShifter.StartExperience();
    }

    private void AngleDown()
    {
        _sphere.RotateDown();
    }
    
    
    private void AngleUp()
    {
        _sphere.RotateUp();
    }
}
