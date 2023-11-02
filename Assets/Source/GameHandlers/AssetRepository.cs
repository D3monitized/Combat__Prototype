using UnityEngine;

public class AssetRepository 
{
   public static GameObject GetCharacterPrefab()
   {
        return (GameObject)Resources.Load("AssetStorage/Prefabs/Character", typeof(GameObject));
   }

   public static GameObject GetEnemyCharacterPrefab()
   {
        return (GameObject)Resources.Load("AssetStorage/Prefabs/Character_Enemy", typeof(GameObject)); 
   }
}
