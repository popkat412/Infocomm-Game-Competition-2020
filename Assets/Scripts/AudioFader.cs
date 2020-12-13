using UnityEngine;
using System.Collections;

public static class AudioFader
{
    // public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    // {
    //     float startVolume = audioSource.volume;

    //     while (audioSource.volume > 0)
    //     {
    //         audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

    //         yield return null;
    //     }

    //     audioSource.Stop();
    //     audioSource.volume = startVolume;
    // }

    public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float targetVolume)
    {
        float currentTime = 0;

        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, targetVolume, currentTime / fadeTime);
            yield return null;
        }
    }
}

