using UnityEngine;

public class RaycastHelper
{
   public static LineTraceResult LineTrace(Camera cam, Vector2 screenPos, float maxDistance, bool debug = false)
   {
        LineTraceResult result = new LineTraceResult();
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit rayHit;

        if (debug) { DebugHelper.Instance.DrawDebugShape("RaycastHelper_Ray", ray, new Color(0, .5f, .5f, .5f)); }
       
        if (Physics.Raycast(ray, out rayHit, maxDistance))
        {
            result.Hit         = true;
            result.HitPosition = rayHit.point;
            result.HitObject   = rayHit.collider.gameObject;
            return result;
        }

        return new LineTraceResult(false); 
   }

    public class LineTraceResult
    {
        public LineTraceResult(bool hit = false)
        {
            Hit = hit;
        }

        public bool Hit;
        public Vector3 HitPosition;
        public GameObject HitObject;
    }
}
