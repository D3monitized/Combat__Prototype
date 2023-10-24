using UnityEngine;
using UnityEngine.AI; 

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] private float stayTimer; 

    public void MoveCharacterToPoint(GameObject character)
    {
        NavMeshAgent agent = character.GetComponent<NavMeshAgent>();
        if (!agent) { return; }

        Vector3 targetPoint = new Vector3(transform.position.x, character.transform.position.y, transform.position.z);
        agent.SetDestination(targetPoint); 
        
    }

    private void Start()
    {
        stayDuration(); 
    }

    private void stayDuration()
    {
        TimerHelper.Instance.AddTimer(gameObject.GetInstanceID().ToString(), 5, timerFinished);
    }

    private void timerFinished()
    {
        print("Timer finished"); 
    }
}
