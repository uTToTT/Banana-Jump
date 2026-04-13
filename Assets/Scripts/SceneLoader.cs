using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public RewardedAd rAd;
    [SerializeField] private GameObject menu;
    private static bool isCloseMenu;

    private void Start()
    {
        if (isCloseMenu)
        {
            menu.SetActive(false);
        } 
        isCloseMenu = false;
    }

    public void ReloadScene()
    {
        string curentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(curentSceneName);
        isCloseMenu = true;
    }  
    
}
