using UnityEngine;
using System.Collections.Generic;

public class TimerHelper : MonoBehaviour
{
    public static TimerHelper Instance;
    private Dictionary<string, Timer> timers = new Dictionary<string, Timer>();
    private List<string> timersToRemove = new List<string>(); 

    public void AddTimer(string key, float time, System.Action action, bool loop = false)
    {
        if (timers.ContainsKey(key)) { return; }

        Timer timer = new Timer(time, action, loop);
        timers.Add(key, timer);
    }

    private void Update()
    {
        foreach (KeyValuePair<string, Timer> pair in timers)
        {
            timers[pair.Key].TimeLeft -= Time.deltaTime;

            if (pair.Value.TimeLeft <= 0)
            {
                pair.Value.TimerCallback?.Invoke();
                if (pair.Value.Loop)
                {
                    timers[pair.Key].TimeLeft = pair.Value.Time; 
                }
                else
                {
                    timersToRemove.Add(pair.Key); 
                }
            }
        }

        for (int i = 0; i < timersToRemove.Count; i++)
        {
            timers.Remove(timersToRemove[i]);
            timersToRemove.RemoveAt(i);
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

    public class Timer
    {
        public bool Loop;
        public float Time;
        public float TimeLeft; 
        public System.Action TimerCallback; 

        public Timer(float time, System.Action callback, bool loop)
        {
            Time = time;
            TimeLeft = Time; 
            Loop = loop;
            TimerCallback = callback; 
        }
    }
}
