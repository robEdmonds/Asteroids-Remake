using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFading
{
    // Fade in the audio from
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float endVolume = 1)
    {
        float startVolume;

        if (!audioSource.isPlaying)
        {
            startVolume = audioSource.volume = 0.0f;
            audioSource.Play();
        }
        else
        {
            startVolume = audioSource.volume;
        }

        while (audioSource.volume < endVolume)
        {
            audioSource.volume += (endVolume - startVolume) * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = endVolume;
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime, float endVolume = 0)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > endVolume)
        {
            audioSource.volume -= (startVolume - endVolume) * Time.deltaTime / FadeTime;

            yield return null;
        }

        if (endVolume == 0)
        {
            audioSource.Stop();
            audioSource.volume = startVolume;
        }
        else
        {
            audioSource.volume = endVolume;
        }
    }
}
