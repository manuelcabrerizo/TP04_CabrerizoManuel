using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnDestroy()
    {
        mainMenuButton.onClick.RemoveAllListeners();

    }

    private void OnMainMenuButtonClicked()
    {
        UIManager.Instance.PlayClickSound();
        GameManager.Instance.ChangeState(GameManager.Instance.MainMenuState);
    }
}
