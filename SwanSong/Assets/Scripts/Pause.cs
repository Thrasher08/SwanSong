﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public int levelToLoad;
    public static bool isPaused = false;
    bool optionsOpen = false;

    public AudioMixer audioMixer;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && optionsOpen == false)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {

    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}