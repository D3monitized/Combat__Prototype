using UnityEngine;
using System.Collections.Generic; 

public class DebugHelper : MonoBehaviour
{
    public static DebugHelper Instance;     
    
    private Dictionary<string, DebugShape> shapes = new Dictionary<string, DebugShape>();

    public void DrawDebugShape(string key, Vector3 pos, Shape shapeType, Color drawColor, float size)
    {
        if (shapes.ContainsKey(key)) { shapes.Remove(key); }
        shapes.Add(key, new DebugShape(shapeType, pos, drawColor, size));
    }

    public void DrawDebugShape(string key, Ray ray, Color drawColor)
    {
        if (shapes.ContainsKey(key)) { shapes.Remove(key); }
        shapes.Add(key, new DebugShape(Shape.Ray, ray, drawColor));
    }

    public void RemoveDebugShape(string key)
    {
        shapes.Remove(key); 
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) { return; }
        
        foreach(KeyValuePair<string, DebugShape> pair in shapes)
        {
            Gizmos.color = pair.Value.DrawColor;
            if (pair.Value.DebugShapeType == Shape.Box) 
            {               
                Gizmos.DrawCube(pair.Value.Position, Vector3.one * pair.Value.Size);
            }
            else if (pair.Value.DebugShapeType == Shape.Sphere)
            {                
                Gizmos.DrawSphere(pair.Value.Position, pair.Value.Size);
            }
            else if(pair.Value.DebugShapeType == Shape.Ray)
            {
                Gizmos.DrawRay(pair.Value.DebugRay.origin, pair.Value.DebugRay.direction * 1000); 
            }
        }
    }
#endif

    public class DebugShape
    {
        public Shape DebugShapeType;
        public Vector3 Position;
        public Color DrawColor; 
        public float Size;

        public Ray DebugRay; 

        public DebugShape(Shape shapeType, Vector3 pos, Color drawColor, float size)
        {
            DebugShapeType = shapeType;
            Position = pos;
            DrawColor = drawColor;
            Size = size; 
        }

        public DebugShape(Shape shapeType, Ray ray, Color drawColor)
        {
            DebugShapeType = shapeType;
            DebugRay = ray;
            DrawColor = drawColor; 
        }
    }

    public enum Shape
    {
        Box, Sphere, Ray
    }
}
