using UnityEngine;

public class PlayState : IState
{
    public void Enter()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic();
    }

    public void Exit()
    {
    }

    public void Process(float dt)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PushState(GameManager.Instance.PauseState);
        }
    }
}
