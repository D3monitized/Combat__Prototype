using UnityEngine;
using UnityEngine.AI; 

public class PatrollController : MonoBehaviour
{
    [SerializeField] private PatrolPoint a;
    [SerializeField] private PatrolPoint b;

    private NavMeshAgent agent;
    private EnemyDetection enemyDetection;

    private void patrol()
    {
        a.MoveCharacterToPoint(gameObject);
        a.OnPatrolledPoint += () => { b.MoveCharacterToPoint(gameObject); };
        b.OnPatrolledPoint += () => { a.MoveCharacterToPoint(gameObject); };
    }

    private void stopPatrol()
    {
        if (!agent) { return; }
        a.OnPatrolledPoint = null;
        b.OnPatrolledPoint = null; 
        NavMeshAgentHelper.Instance.StopAgent(agent);
    }

    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
        TryGetComponent<EnemyDetection>(out enemyDetection);

        enemyDetection.OnEnemyDetected += stopPatrol;
    }

    private void Start()
    {
        patrol(); 
    }
}
