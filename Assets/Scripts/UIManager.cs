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
        gameOverScreen.SetActive(true);
        finalScoreValue.text =  GameManager.Instance.Score.ToString();
    }
}
