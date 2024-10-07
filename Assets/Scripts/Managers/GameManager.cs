using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // States
    private StateMachine stateMachine;
    private MainMenuState mainMenuState;
    private PlayState playState;
    private PauseState pauseState;
    private GameOverState gameOverState;
    public MainMenuState MainMenuState => mainMenuState;
    public PlayState PlayState => playState;
    public PauseState PauseState => pauseState;
    public GameOverState GameOverState => gameOverState;

    // Gameplay GameObjects
    [SerializeField] PlayerController player;
    [SerializeField] Spawner spawner;

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

        stateMachine = new StateMachine();
        mainMenuState = new MainMenuState();
        playState = new PlayState();
        pauseState = new PauseState();
        gameOverState = new GameOverState();
    }

    // Start is called before the first frame update
    private void Start()
    {
        stateMachine.PushState(mainMenuState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }

    public void ResetGameplayObjects()
    {
        player.ResetPlayer();
        spawner.ResetSpawner();
    }

    public void PushState(IState state)
    {
        stateMachine.PushState(state);
    }

    public void PopState()
    {
        stateMachine.PopState();
    }

    public void ChangeState(IState state)
    {
        stateMachine.ChangeState(state);
    }

    public IState PeekState()
    {
        return stateMachine.PeekState();
    }
}
