using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioMixerGroup Mixer;

    void Start()
    {
        Mixer.audioMixer.SetFloat("Highpass", 4000);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {        
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {        
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
}
