using UnityEngine;
using UnityEngine.Events; 

[DisallowMultipleComponent]
[RequireComponent(typeof(SphereCollider))]
public class EnemyDetection : MonoBehaviour
{
    private SphereCollider sphereCollider;

    [SerializeField] private UnityEvent<Character> onEnemyDetected; 

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (!character || character.IsHostile) { return; }
        onEnemyDetected?.Invoke(character); 
    }

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        DebugHelper.Instance.DrawDebugShape(
        "EnemyDetection_" + GetInstanceID().ToString(), 
        transform, Vector3.zero, DebugHelper.Shape.Sphere, 
        new Color(1, 0, 0, .5f), 
        sphereCollider.radius);
    }
}
