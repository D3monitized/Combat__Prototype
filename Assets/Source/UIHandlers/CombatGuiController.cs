using UnityEngine;

public class CombatGuiController : MonoBehaviour
{
    [SerializeField] private GameObject hotbar;
    [SerializeField] private GameObject initTracker;     

    private void displayUI(bool shouldDisplay)
    {
        hotbar.SetActive(shouldDisplay);
        initTracker.SetActive(shouldDisplay);
    }

    private void handleGameStateChanged(GameManager.GameState state)
    {
        displayUI(state == GameManager.GameState.Fighting);
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateUpdated += handleGameStateChanged;

        displayUI(false); //This gui shouldn't be enabled when starting game
    }
}
