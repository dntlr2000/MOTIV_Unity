using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_UI : MonoBehaviour
{
    public void title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Escape()
    {
        Application.Quit();
    }
}
