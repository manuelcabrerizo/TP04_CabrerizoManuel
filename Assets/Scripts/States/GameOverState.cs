using UnityEngine;
public class GameOverState : IState
{
    public void Enter()
    {
        UIManager.Instance.OpenGameOverMenu();
        AudioManager.Instance.StopMusic();
        Time.timeScale = 0;
    }

    public void Exit()
    {
        UIManager.Instance.CloseGameOverMenu();
        Time.timeScale = 1;
    }

    public void Process(float dt)
    {
    }
}
