using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Character))]
public class EnemyCharacterController : MonoBehaviour
{
    private Character character; 

    public void HandleEnemyDetected()
    {
        
    }

    private void Start()
    {
        character = GetComponent<Character>(); 
    }
}
