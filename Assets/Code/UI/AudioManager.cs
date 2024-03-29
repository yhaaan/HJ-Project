using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager instance;
    [Header("#BGM")] 
    public AudioClip[] bgmClips;
    public float bgmVolume;
    public Slider bgmSlider;
    private AudioSource bgmPlayer;
    
    [Header("#SFX")] 
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public Slider sfxSlider;
    public int channels;
    private AudioSource[] sfxPlayers;
    private int channelIndex;
    
    
    private bool changeable = false;
    public enum Sfx {Jump = 0 , StartCharging , FullCharging, Click}
    private void Awake()
    {
        sfxSlider.value = 0.2f;
        bgmSlider.value = 0.2f;
        instance = this;
        Init();
        PlayBgm(true);
    }

    
    void Init()
    {
        //배경음 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClips[0];
        
        //효과음 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;

        }

        changeable = true;
    }

    public void ChangeBgm(int stageNum)
    {
        bgmPlayer.clip = bgmClips[stageNum];
    }
    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }
    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;
            if(sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[channelIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[channelIndex].Play();
            break;
        }
        
    }

    public void ChangeVolumeBgm()
    {
        if(changeable)
            bgmPlayer.volume = bgmSlider.value;
    }
    
    
    
    public void ChangeVolumeSfx()
    {
        if (changeable)
        {
            for (int index = 0; index < sfxPlayers.Length; index++)
            {
                sfxPlayers[index].volume = sfxSlider.value;
            }
        }
    }
    
}


