using UnityEngine;

public class Character : MonoBehaviour
{
    public bool IsHostile;
    
    [HideInInspector] public int Id { get; private set; }
    [HideInInspector] public int Initiative { get; private set; }
    [HideInInspector] public int Damage { get; private set; }
    [HideInInspector] public int Health { get; private set; }


    private void Start()
    {
        Id = GetInstanceID(); 
    }
}
