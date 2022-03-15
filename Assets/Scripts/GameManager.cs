using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int time = 30;
    [SerializeField] public int dificulty = 1;
    [SerializeField] private int score;
    public bool gameOver;

    private Camera _mainCamera;
    AudioSource _cameraAudioSource;

    public int Time
    {
        get => time;
        set
        {
            time = value;
            UIManager.Instance.UpdateUITime(time);
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        _mainCamera = Camera.main;
        _cameraAudioSource = _mainCamera.GetComponent<AudioSource>();
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
            UIManager.Instance.UpdateUIScore(score);
            if (score % 1000 == 0)
                //Dificulty increase
                EnemySpawner.Instance.increaseSpawnRate();
        }
    } 

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        if (Time > 0)
        {
            Time--;
            StartCoroutine("CountDown");
        }
        else
        {
            gameOver = true;
            _cameraAudioSource.Stop(); ;
            if(FindObjectOfType<EnemySpawner>() != null) FindObjectOfType<EnemySpawner>().gameObject.SetActive(false);
            if(FindObjectOfType<Enemy>() != null) FindObjectOfType<Enemy>().speed = 0;
            UIManager.Instance.ShowGameOverScreen();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    
}
