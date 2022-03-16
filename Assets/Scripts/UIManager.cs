using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Text scoreValue;
    [SerializeField] Text healthValue;
    [SerializeField] Text timeValue;
    [SerializeField] Text finalScoreValue;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gamePauseScreen;
    [SerializeField] GameObject gameAudioObject;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void UpdateUIScore(int newScoreValue)
    {
        scoreValue.text = newScoreValue.ToString();
    }
    public void UpdateUIhealth(int newHealthValue)
    {
        healthValue.text = newHealthValue.ToString();
    }
    public void UpdateUITime(int newTimeValue)
    {
        timeValue.text = newTimeValue.ToString();
    }

    public void ShowGameOverScreen()
    {
        if (FindObjectOfType<MainLoopAudio>() != null)
        {
            
            Debug.Log($"FoundThefuckingAudioSource {FindObjectOfType<MainLoopAudio>().gameObject.name}");
            FindObjectOfType<MainLoopAudio>().gameObject.SetActive(false);
        };
        gameOverScreen.SetActive(true);
        finalScoreValue.text =  GameManager.Instance.Score.ToString();
    }

    public void ShowPauseMenuScreen()
    {
        //if (FindObjectOfType<MainLoopAudio>() != null)
        //    FindObjectOfType<MainLoopAudio>().gameObject.SetActive(false);
        gameAudioObject.GetComponent<AudioSource>().enabled = false;

        Time.timeScale = 0;
        gamePauseScreen.SetActive(true);
    }

    public void HidePauseMenuScreen()
    {
        //if (FindObjectOfType<MainLoopAudio>() != null)
        //{
        //    Debug.Log($"FoundThefuckingAudioSource {FindObjectOfType<MainLoopAudio>().gameObject.name}");
        //    FindObjectOfType<MainLoopAudio>().gameObject.SetActive(true);
        //}

        gameAudioObject.GetComponent<AudioSource>().enabled = true;


        Time.timeScale = 1;
        gamePauseScreen.SetActive(false);
    }
}
