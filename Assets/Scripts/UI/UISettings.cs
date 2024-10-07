using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Button backButton;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundEffectVolumeSlider;
    [SerializeField] private Slider uiSoundVolumeSlider;

    void Awake()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderValueChange);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderValueChange);
        soundEffectVolumeSlider.onValueChanged.AddListener(OnSoundEffectVolumeSliderValueChange);
        uiSoundVolumeSlider.onValueChanged.AddListener(OnUISoundVolumeSliderValueChange);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
        masterVolumeSlider.onValueChanged.RemoveAllListeners();
        musicVolumeSlider.onValueChanged.RemoveAllListeners();
        soundEffectVolumeSlider.onValueChanged.RemoveAllListeners();
        uiSoundVolumeSlider.onValueChanged.RemoveAllListeners();

    }

    private void OnBackButtonClicked()
    {
        UIManager.Instance.PlayClickSound();
        UIManager.Instance.CloseSettingsMenu();
        if (GameManager.Instance.PeekState() == GameManager.Instance.MainMenuState)
        {
            UIManager.Instance.OpenMainMenu();
        }
        else
        {
            UIManager.Instance.OpenPauseMenu();
        }
    }

    private void OnMasterVolumeSliderValueChange(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    private void OnMusicVolumeSliderValueChange(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    private void OnSoundEffectVolumeSliderValueChange(float value)
    {
        AudioManager.Instance.SetSoundEffectVolume(value);
    }

    private void OnUISoundVolumeSliderValueChange(float value)
    {
        AudioManager.Instance.SetUISoundVolume(value);
    }
}
