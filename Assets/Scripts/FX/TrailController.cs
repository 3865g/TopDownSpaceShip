using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TrailController : MonoBehaviour
{
    public VisualEffect visualEffect;

    public bool test;

    public void SetVariables(Vector3 startPosition, Vector3 endPosition, Vector3 rotation)
    {



        visualEffect.SetVector3("StartPosition_position", startPosition);
        visualEffect.SetVector3("EndPosition_position", endPosition);
        visualEffect.SetVector3("Rotation", rotation);

        visualEffect.initialEventName = "EnableTrail";


        visualEffect.Reinit();

        visualEffect.Play();
        visualEffect.initialEventID = 0;
        visualEffect.SendEvent("EnableTrail");
        visualEffect.resetSeedOnPlay = true;

        Debug.LogError(visualEffect.initialEventID.GetHashCode().ToString());
    }

  
}
