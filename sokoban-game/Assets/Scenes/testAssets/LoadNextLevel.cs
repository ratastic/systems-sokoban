using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.Toolbars;

public class LoadNextLevel : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject enterLevel2;
    public AudioSource bellDing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enterLevel2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.CheckWin())
        {
            WinUpdate();
        }
    }

    public void WinUpdate()
    {
        enterLevel2.SetActive(true);
        bellDing.Play();
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level02");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level03");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level04");
    }

    public void LoadHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }
}
