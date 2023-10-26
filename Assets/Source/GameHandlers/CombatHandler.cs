using UnityEngine;
using System.Collections.Generic; 

public class CombatHandler
{
    private static List<Character> fighters = new List<Character>(); 

    public static void InitiateCombat(Character c1, Character c2)
    {
        Debug.Log("Initiating Combat: " + c1.name + " vs " + c2.name);
    }

    public static void EndCombat()
    {

    }

}
