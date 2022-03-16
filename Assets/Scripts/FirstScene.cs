using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    [SerializeField] AudioClip clickAudioClip;

    public void PlayFirstScreenButtonAudioClip()
    {
        Debug.Log("Triggered start game");

        AudioSource.PlayClipAtPoint(clickAudioClip, transform.position);
        Invoke("StartGame", .5f);
    }

   
    private void StartGame()
    {
        Debug.Log("Triggered start game");
        SceneManager.LoadScene("Game");
    }
}
