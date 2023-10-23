using UnityEngine;

public class AssetRepository 
{
   public static GameObject GetCharacterPrefab()
   {
        return (GameObject)Resources.Load("AssetStorage/Character", typeof(GameObject));
   }
}
