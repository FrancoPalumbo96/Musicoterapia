using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicLoader : MonoBehaviour
{
    // Start is called before the first frame update

    private Object[] songs;
    private AudioSource source;
    private int currentTrack;
    public Text text;
    void Start()
    {
        source = GetComponent<AudioSource>();
        currentTrack = 0;
        songs = Resources.LoadAll("Songs", typeof(AudioClip)); //Busca todos los AudioClip dentro de la carpeta Resources
        source.clip = (AudioClip)songs[currentTrack];
        source.Play();
        showTitle(); //Esto solo se usa si necesitamos mostrar el titulo de la cancion
        playMusic();
    }

    private void Update()
    {
        if (source.time > 30) //despues de 30 segundos la cancion se detiene
        {
            source.Stop(); //si quiero que sea una muestra de la cancion simplemente cambio el playNext por un stop y de ultima un loop
        }
    }

    public void playMusic()
    {
        if (source.isPlaying) {
            return;
        } else {
            currentTrack--;
            if (currentTrack < 0) {
                currentTrack = songs.Length - 1;
            }
        }
        StartCoroutine(NextSong());
    }

    IEnumerator NextSong()
    {
        while (source.isPlaying)
        {
            yield return null;
        }
        playNext();
    }

    public void playNext()
    {
        source.Stop();
        currentTrack++;
        if (currentTrack >= songs.Length)
        {
            currentTrack = 0;
        }
        source.clip = (AudioClip)songs[currentTrack];
        source.Play();
        showTitle();
        StartCoroutine(NextSong());
    }

    public void playPrevious()
    {
        source.Stop();
        currentTrack--;
        if (currentTrack < 0)
        {
            currentTrack = songs.Length -1;
        }
        source.clip = (AudioClip)songs[currentTrack];
        source.Play();
        showTitle();
        StartCoroutine(NextSong());
    }

    public void showTitle()
    {
        text.text = source.clip.name;
    }
}
