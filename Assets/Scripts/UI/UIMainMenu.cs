using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

    }

    private void OnPlayButtonClicked()
    {
        UIManager.Instance.PlayClickSound();
        GameManager.Instance.ChangeState(GameManager.Instance.PlayState);
    }

    private void OnSettingsButtonClicked()
    {
        UIManager.Instance.PlayClickSound();
        UIManager.Instance.CloseMainMenu();
        UIManager.Instance.OpenSettingsMenu();
    }

    private void OnExitButtonClicked()
    {
        UIManager.Instance.PlayClickSound();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
