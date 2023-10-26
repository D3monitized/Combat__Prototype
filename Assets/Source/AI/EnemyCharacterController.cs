using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Character))]
public class EnemyCharacterController : MonoBehaviour
{
    private Character character; 

    public void HandleEnemyDetected(Character enemyCharacter)
    {
        CombatHandler.InitiateCombat(character, enemyCharacter); 
    }

    private void Start()
    {
        character = GetComponent<Character>(); 
    }
}
