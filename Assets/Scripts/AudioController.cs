using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private bool fadeInOn;
    private bool fadeOutOn;

    private IEnumerator fadeInSound1;
    private IEnumerator fadeOutSound1;

    public float fadeInRate;
    public float fadeOutRate;

    public AudioSource audioSource1;

    // Use this for initialization
    void Start () {
        fadeInSound1 = AudioFading.FadeIn(audioSource1, 1.0f / fadeInRate);
        fadeOutSound1 = AudioFading.FadeOut(audioSource1, 1.0f / fadeOutRate);
    }

    public void SetVolume(float volume)
    {
        if(audioSource1.volume < volume)
        {
            fadeIn(volume);
        }
        else if(audioSource1.volume > volume)
        {
            fadeOut(volume);
        }
    }

    private void fadeIn(float endVolume)
    {
        StopCoroutine(fadeInSound1);
        StopCoroutine(fadeOutSound1);

        fadeInSound1 = AudioFading.FadeIn(audioSource1, (endVolume - audioSource1.volume) / fadeInRate, endVolume);

        StartCoroutine(fadeInSound1);
    }

    private void fadeOut(float endVolume)
    {
        StopCoroutine(fadeOutSound1);
        StopCoroutine(fadeInSound1);

        fadeOutSound1 = AudioFading.FadeOut(audioSource1, (audioSource1.volume - endVolume) / fadeInRate, endVolume);

        StartCoroutine(fadeOutSound1);
    }
}