using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRotate : MonoBehaviour
{
    public float xAngle;
    public float yAngle;
    public float zAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(xAngle, yAngle, zAngle) * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
