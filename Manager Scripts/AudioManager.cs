using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I { get; private set; } //I means Instance since this class is a singleton
    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource bgmSource, sfxSource;

    private void Start()
    {
    }

    public void PlayBGM(AudioClip audioClip)
    {
        bgmSource.clip = audioClip;
        bgmSource.Play();

        //bgmSource.PlayOneShot(audioClip);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {   
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}