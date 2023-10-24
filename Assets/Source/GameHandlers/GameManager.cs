using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event StateChangedDelegate OnGameStateUpdated; 

    public GameState currentState { get; private set; } 

    public void SetGameState(GameState state)
    {
        currentState = state;
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

    public enum GameState
    {
        Fighting, Exploring
    }

    public delegate void StateChangedDelegate(GameState state); 
}
