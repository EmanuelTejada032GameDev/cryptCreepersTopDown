using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int time = 30;
    [SerializeField] public int dificulty = 1;
    [SerializeField] private int score;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine("CountDown");
    }

    public int Score
    {
        get => score;
        set
        {
            score = value;
            if (score % 1000 == 0)
                dificulty++;
        }
    } 

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        if (time > 0)
        {
            time--;
            StartCoroutine("CountDown");
        }
    }
}
