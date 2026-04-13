using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioMixerGroup Mixer;   
    private void Start()
    {
        if (GetComponentInChildren<Toggle>() != null)
        {
            GetComponentInChildren<Toggle>().isOn = PlayerPrefs.GetInt("MusicEnabled") == 1;
        }        
    }    

    public void ToogleMusic(bool enabled)
    {        
        if (enabled)
        {
            Mixer.audioMixer.SetFloat("MusicVolume", 1);
        }
        else
        {
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
        }        
        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
    }

    public void SoundInMenu()
    {
        Mixer.audioMixer.SetFloat("Highpass", 8000);
    }

    public void SoundWithoutMenu()
    {
        Mixer.audioMixer.SetFloat("Highpass", 10);
    }
}
