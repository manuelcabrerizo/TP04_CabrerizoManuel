using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject gameOverMenuPanel;

    [SerializeField] private AudioClip selectAudioClip;
    [SerializeField] private AudioClip clickAudioClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CloseMainMenu();
        ClosePauseMenu();
        CloseSettingsMenu();
        CloseGameOverMenu();
    }

    public void OpenMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    public void CloseMainMenu()
    {
        mainMenuPanel.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        settingsMenuPanel.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        settingsMenuPanel.SetActive(false);
    }

    public void OpenGameOverMenu()
    {
        gameOverMenuPanel.SetActive(true);
    }

    public void CloseGameOverMenu()
    {
        gameOverMenuPanel.SetActive(false);
    }

    public void PlaySelectSound()
    {
        AudioManager.Instance.PlayClip(selectAudioClip, AudioSourceType.UI);
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlayClip(clickAudioClip, AudioSourceType.UI);
    }
}
