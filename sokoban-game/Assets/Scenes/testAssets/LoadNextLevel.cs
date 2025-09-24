using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject enterLevel2;
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
            enterLevel2.SetActive(true);
        }
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level02");
    }
}
