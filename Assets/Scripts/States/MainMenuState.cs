using UnityEngine;

public class MainMenuState : IState
{
    public void Enter()
    {
        GameManager.Instance.ResetGameplayObjects();
        UIManager.Instance.OpenMainMenu();
        AudioManager.Instance.StopMusic();
        Time.timeScale = 0;
    }

    public void Exit()
    {
        UIManager.Instance.CloseMainMenu();
        UIManager.Instance.CloseSettingsMenu();
        Time.timeScale = 1;
    }

    public void Process(float dt)
    {

    }
}
