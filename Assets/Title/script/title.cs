using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    public AudioClip menuSound; 
    private AudioSource audioSource; 

    private void Start()
    {
        Time.timeScale = 0.1f;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Start_game()
    {
        PlaySound();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene");
    }

    public void How_to_play()
    {
        PlaySound();
        this.transform.Find("How to play Image").gameObject.SetActive(true);
    }

    public void Credits()
    {
        PlaySound();
        this.transform.Find("credits").gameObject.SetActive(true);
    }

    public void Escape()
    {
        PlaySound();
        Application.Quit();
    }

    private void PlaySound()
    {
        if (menuSound != null)
        {
            audioSource.PlayOneShot(menuSound);
        }
    }
}
