using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme"); //tema müziðinin oyun baslar baslamaz çalmasýný istiyorum
        // AudioManager.Instance.musicSource.Stop(); theme müziðin felan durmasýný istediðim zaman çaðýracaðým kod.
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.sName == name);

        if (s == null)
        {
            Debug.Log("Sound not found cano.");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    } //source.Play();

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.sName == name);

        if (s == null)
        {
            Debug.Log("Sound not found cano.");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    } //source.PlayOneShot();

    //CANVAS ÝÇÝN 
    public void ToggleMusic() //açýksa kapalý kapalýysa açýk yap;
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume) //volume deðiþtir.
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
