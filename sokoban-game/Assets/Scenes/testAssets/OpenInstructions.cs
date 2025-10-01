using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.Toolbars;

public class OpenInstructions : MonoBehaviour
{
    public GameObject title;
    public GameObject startButton;
    public GameObject instructionButton;
    public GameObject instructions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        title.SetActive(true);
        startButton.SetActive(true);
        instructionButton.SetActive(true);

        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPanel()
    {
        title.SetActive(false);
        startButton.SetActive(false);
        instructionButton.SetActive(false);

        instructions.SetActive(true);
    }

    public void ClosePanel()
    {
        title.SetActive(true);
        startButton.SetActive(true);
        instructionButton.SetActive(true);

        instructions.SetActive(false);
    }
}
