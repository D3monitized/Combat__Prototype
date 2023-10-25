using UnityEngine;
using UnityEngine.AI; 
using System.Collections.Generic; 

public class NavMeshAgentHelper : MonoBehaviour
{
    public static NavMeshAgentHelper Instance;

    private List<Agent> agents = new List<Agent>();

    public void MoveAgent(NavMeshAgent agent, Vector3 destination,System.Action callback)
    {
        Agent navAgent = new Agent(agent, callback);
        navAgent.NavAgent.SetDestination(destination); 
        agents.Add(navAgent);
    }

    public void StopAgent(NavMeshAgent agent)
    {
        for (int i = 0; i < agents.Count; i++)
        {
            NavMeshAgent navAgent = agents[i].NavAgent;
            if (agent.GetInstanceID() == navAgent.GetInstanceID())
            {
                agent.isStopped = true;
                agents.RemoveAt(i);
            }
        }
    }

    private void Awake()
    { 
    	if (Instance && Instance != this)
		{
	    	Destroy(gameObject);
		}
		else { Instance = this; }
    }

    private void Update()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            NavMeshAgent agent = agents[i].NavAgent;
            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
            {
                agents[i].Callback?.Invoke();
                agents.RemoveAt(i); 
            }
        }
    }

    public class Agent
    {
        public NavMeshAgent NavAgent;
        public System.Action Callback;

        public Agent(NavMeshAgent agent, System.Action callback)
        {
            NavAgent = agent;
            Callback = callback; 
        }
    }
}
