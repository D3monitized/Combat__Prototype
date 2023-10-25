using UnityEngine;
using UnityEngine.AI; 

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] private float stayTimer;

    public PatrolPointDoneDelegate OnPatrolledPoint;

    public void MoveCharacterToPoint(GameObject character)
    {
        NavMeshAgent agent = character.GetComponent<NavMeshAgent>();
        if (!agent) { return; }

        Vector3 targetPoint = new Vector3(transform.position.x, character.transform.position.y, transform.position.z);
        NavMeshAgentHelper.Instance.MoveAgent(agent, targetPoint, stayDuration);        
    }

    private void stayDuration()
    {
        TimerHelper.Instance.AddTimer(gameObject.GetInstanceID().ToString(), stayTimer, () => { OnPatrolledPoint?.Invoke(); });
    }

    public delegate void PatrolPointDoneDelegate();
}
