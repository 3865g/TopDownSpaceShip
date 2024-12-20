using UnityEngine;

public class CustomRotate : MonoBehaviour
{
    public float xAngle;
    public float yAngle;
    public float zAngle;
  

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(xAngle, yAngle, zAngle) * Time.deltaTime);
    }
}
