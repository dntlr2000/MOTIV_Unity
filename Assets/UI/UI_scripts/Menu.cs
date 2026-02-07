using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject UI;
    public GameObject menu;

    private void Awake()
    {
        UI = GameObject.Find("UI");
        menu = GameObject.Find("Menu");
    }

    public void resume()
    {
        Time.timeScale = 1;
        menu.gameObject.SetActive(false);
    }

    public void title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Escape()
    {
        Application.Quit();
    }
}
