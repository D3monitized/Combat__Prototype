using UnityEngine;
using System.Collections.Generic; 

/*
 * Load savefiles
 * Spawn characters
 * Load gamestate
 */
public class GameSetup : MonoBehaviour
{
    public static GameSetup Instance; 

    private void spawnParty()
    {
        List<Character> characters = new List<Character>(); 
        GameObject characterPrefab = AssetRepository.GetCharacterPrefab();

        for (int i = 0; i < InternalSettings.AMOUNT_PARTYMEMBERS; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-10, -8), 1, Random.Range(-10, 10));
            GameObject characterClone = Instantiate(characterPrefab, randomPos, Quaternion.identity);
            characters.Add(characterClone.GetComponent<Character>());
        }
    }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject); 
        }
        else { Instance = this; }

        spawnParty(); 
    }
}
