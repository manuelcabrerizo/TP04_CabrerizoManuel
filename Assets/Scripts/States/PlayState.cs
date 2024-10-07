using UnityEngine;

public enum PlayMode
{ 
    Platformer,
    BossFight
}

public class PlayState : IState
{
    private PlayMode playMode;
    private float timeToChangeMode = 40.0f;
    private float currentTime = 0.0f;

    private float waitBeforeTheBoss = 10.0f;
    private float currentWaitBeforeTheBoss = 0;

    public void Enter()
    {
        playMode = PlayMode.Platformer;
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic();
        currentTime = timeToChangeMode;
        currentWaitBeforeTheBoss = waitBeforeTheBoss;
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

        if (currentTime <= 0.0f)
        {
            if (playMode == PlayMode.Platformer)
            {
                playMode = PlayMode.BossFight;
                GameManager.Instance.PauseSpawner();
                currentWaitBeforeTheBoss = waitBeforeTheBoss;
            }
            else 
            {
                playMode = PlayMode.Platformer;
                GameManager.Instance.StartPlatformerMode();
            }
            currentTime = timeToChangeMode;
        }
        currentTime -= dt;


        if (playMode == PlayMode.BossFight)
        {
            if (currentWaitBeforeTheBoss <= 0.0f)
            {
                GameManager.Instance.StartBossFightMode();
                currentWaitBeforeTheBoss = waitBeforeTheBoss;
            }
            currentWaitBeforeTheBoss -= dt;
        }

    }
}
