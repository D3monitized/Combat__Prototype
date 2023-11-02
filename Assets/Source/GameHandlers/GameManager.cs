using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event System.Action<GameState> OnGameStateUpdated;

    public GameState CurrentState { get; private set; } 

    public void SetGameState(GameState state)
    {
        CurrentState = state;
        OnGameStateUpdated?.Invoke(state); 
    }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else { Instance = this; }
    }

    private void Start()
    {
        SetGameState(GameState.Exploring); 
    }

    public enum GameState
    {
        Fighting, Exploring
    }
}
