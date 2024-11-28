using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas main;
    public Canvas opitions;

    // Start is called before the first frame update
    void Start()
    {
        main.enabled = true;
        opitions.enabled = false;
    }

    public void Onplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnOptions()
    {
        main.enabled = false;
        opitions.enabled = true;
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnBackToMain()
    {
        main.enabled = true;
        opitions.enabled = false;
    }

}
