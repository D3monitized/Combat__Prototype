using UnityEngine;
using UnityEngine.AI; 

public class PatrollController : MonoBehaviour
{
    [SerializeField] private PatrolPoint a;
    [SerializeField] private PatrolPoint b;

    private NavMeshAgent agent; 

    private void patrol()
    {
        a.MoveCharacterToPoint(gameObject);
        a.OnPatrolledPoint += () => { b.MoveCharacterToPoint(gameObject); };
        b.OnPatrolledPoint += () => { a.MoveCharacterToPoint(gameObject); };

        TimerHelper.Instance.AddTimer(GetInstanceID().ToString(), 2, stopPatrol);
    }

    private void stopPatrol()
    {
        a.OnPatrolledPoint = null;
        b.OnPatrolledPoint = null; 
        NavMeshAgentHelper.Instance.StopAgent(agent);
    }

    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent); 
    }

    private void Start()
    {
        patrol(); 
    }
}
