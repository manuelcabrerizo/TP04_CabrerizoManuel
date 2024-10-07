using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();

    }

    private void OnResumeButtonClicked()
    {
        UIManager.Instance.PlayClickSound();
        GameManager.Instance.PopState();
    }

    private void OnSettingsButtonClicked()
    {
        UIManager.Instance.PlayClickSound();
        UIManager.Instance.ClosePauseMenu();
        UIManager.Instance.OpenSettingsMenu();
    }

    private void OnMainMenuButtonClicked()
    {
        UIManager.Instance.PlayClickSound();
        GameManager.Instance.ChangeState(GameManager.Instance.MainMenuState);
    }
}
