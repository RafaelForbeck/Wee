using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    public AudioSource intro;
    public AudioSource game;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayIntro()
    {
        if (intro.isPlaying)
        {
            return;
        }
        game.Stop();
        intro.Play();
    }

    public void PlayGame()
    {
        intro.Stop();
        game.Play();
    }
    
}
