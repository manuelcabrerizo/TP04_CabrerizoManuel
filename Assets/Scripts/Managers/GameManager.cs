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
    [SerializeField] private PlayerController player;
    [SerializeField] private ObstacleSpawner spawner;
    [SerializeField] private AlienShoot alien;

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            spawner.PauseSpawner();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            spawner.ResumeSpawner();
        }

        stateMachine.Update(Time.deltaTime);
    }

    public void ResetGameplayObjects()
    {
        player.ResetPlayer();
        spawner.ResetSpawner();
        spawner.ResumeSpawner();
        alien.gameObject.SetActive(false);
        alien.ResetShoots();
        alien.PauseShoot();
    }

    public void StartPlatformerMode()
    {
        AudioManager.Instance.PlayClip(alien.AlienData.SpawnClip, AudioSourceType.SFX);
        alien.gameObject.SetActive(false);
        alien.ResetShoots();
        alien.PauseShoot();
        spawner.ResumeSpawner();
    }

    public void PauseSpawner()
    {
        spawner.PauseSpawner();
    }

    public void StartBossFightMode()
    {
        AudioManager.Instance.PlayClip(alien.AlienData.SpawnClip, AudioSourceType.SFX);
        alien.gameObject.SetActive(true);
        alien.ResumeShoot();
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
