using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic; 

public class PartyController : MonoBehaviour
{
    public static PartyController Instance;

    public CharDelegate OnCharacterSelected;

    private List<Character> characters = new List<Character>();
    private Character selectedCharacter;

    #region Runtime

    private void moveCharacter(Vector3 position)
    {
        selectedCharacter.GetComponent<NavMeshAgent>().SetDestination(position); 
    }

    private void onSelectButton(Vector2 mousePos)
    {
        RaycastHelper.LineTraceResult result = RaycastHelper.LineTrace(Camera.main, mousePos, InternalSettings.MAX_INTERACTION_DISTANCE, false);
        //DebugHelper.Instance.DrawDebugShape("PartyController_OnSelect", result.HitPosition, DebugHelper.Shape.Sphere, new Color(0, 120, 120, .5f), 1);

        Character hitCharacter = result.HitObject?.GetComponent<Character>(); 

        if (hitCharacter) //If clicked on character
        {
            selectedCharacter = hitCharacter;
            OnCharacterSelected?.Invoke(selectedCharacter); //Event that other scripts can subscribe to
            DebugHelper.Instance.DrawDebugShape("PartyController_Selected", hitCharacter.transform, DebugHelper.Shape.Box, Color.green, 1);
        }
        else if (selectedCharacter) //If clicked on something else
        {
            moveCharacter(result.HitPosition);
        }
    }

    #endregion

    #region Startup
    private void spawnParty(int amount)
    {
        GameObject characterPrefab = AssetRepository.GetCharacterPrefab();

        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-10, -8), 1, Random.Range(-10, 10));
            GameObject characterClone = Instantiate(characterPrefab, randomPos, Quaternion.identity);
            characters.Add(characterClone.GetComponent<Character>());
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            this.enabled = false; 
        }
        else { Instance = this; }
    }

    private void Start()
    {
        spawnParty(InternalSettings.AMOUNT_PARTYMEMBERS);
        InputHandler.Instance.LMBPressed += onSelectButton;
    }
    #endregion

    public delegate void CharDelegate(Character ch); 
}
