using UnityEngine;
using UnityEngine.VFX;

public class TrailController : MonoBehaviour
{
    public GhostTrail GhostTrail;

    public void SetVariables(Vector3 startPosition, Vector3 endPosition, Vector3 rotation)
    {

        GhostTrail.pointA = startPosition;
        GhostTrail.pointB = endPosition;
        GhostTrail.Direction = rotation;
        GhostTrail.SpawnMeshesBetweenPoints();

    }

  
}
