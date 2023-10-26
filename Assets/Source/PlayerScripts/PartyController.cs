﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PartyController : MonoBehaviour
{
    public static PartyController Instance;
    public CharDelegate OnCharacterSelected;
    [SerializeField] private LayerMask interactableMask; 
    
    private Character selectedCharacter;

    #region Runtime

    private void moveCharacter(Vector3 position)
    {
        selectedCharacter.GetComponent<NavMeshAgent>().SetDestination(position);
    }

    private void handleSelectedCharacter(Character hitCharacter)
    {
        if (hitCharacter.IsHostile) { return; }
        selectedCharacter = hitCharacter;
        OnCharacterSelected?.Invoke(selectedCharacter); //Event that other scripts can subscribe to
        DebugHelper.Instance.DrawDebugShape("PartyController_Selected", hitCharacter.transform, Vector2.up * 2, DebugHelper.Shape.Box, Color.green, 1);
    }

    private void onSelectButton(Vector2 mousePos)
    {
        RaycastHelper.LineTraceResult result = RaycastHelper.LineTrace(
        Camera.main, 
        mousePos, 
        InternalSettings.MAX_INTERACTION_DISTANCE, 
        interactableMask, false
        );
        //DebugHelper.Instance.DrawDebugShape("PartyController_OnSelect", result.HitPosition, DebugHelper.Shape.Sphere, new Color(0, 120, 120, .5f), 1);

        Character hitCharacter = result.HitObject?.GetComponent<Character>();

        if (hitCharacter) { handleSelectedCharacter(hitCharacter); }
        else if (selectedCharacter && GameManager.Instance.CurrentState == GameManager.GameState.Exploring) //If clicked on something else
        {
            moveCharacter(result.HitPosition);
        }                
    }

    #endregion

    #region Startup

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
        InputHandler.Instance.LMBPressed += onSelectButton;
    }
    #endregion

    public delegate void CharDelegate(Character ch);
}
