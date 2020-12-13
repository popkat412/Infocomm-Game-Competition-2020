using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    Player player;
    GameOverManager gameOverManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnPlayerDeath += StopPlaying;
        gameOverManager = GameObject.Find("Game Over Manager").GetComponent<GameOverManager>();
        audioSource = GetComponent<AudioSource>();

        StartPlaying();
    }

    void StopPlaying()
    {
        print("time samples (write): " + audioSource.timeSamples);
        audioSource.Stop();
        PlayerPrefs.SetInt(Constants.musicTimeKey, audioSource.timeSamples);
    }

    void StartPlaying()
    {
        int timeSamples = PlayerPrefs.GetInt(Constants.musicTimeKey, 0);
        print("time samples (write): " + timeSamples);
        audioSource.Play();
        audioSource.timeSamples = timeSamples;
        audioSource.volume = 0;
        StartCoroutine(AudioFader.FadeIn(audioSource, 5, 0.7f));
    }
}
