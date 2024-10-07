using UnityEngine;

public class PauseState : IState
{
    public void Enter()
    {
        UIManager.Instance.OpenPauseMenu();
        AudioManager.Instance.PauseMusic();
        Time.timeScale = 0;
    }

    public void Exit()
    {
        UIManager.Instance.ClosePauseMenu();
        UIManager.Instance.CloseSettingsMenu();
        AudioManager.Instance.PlayMusic();
        Time.timeScale = 1;
    }

    public void Process(float dt)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PopState();
        }
    }
}
