using UnityEngine;

public class SubtractParentRotation : MonoBehaviour
{
    public Transform Parent;
    public Quaternion LastParentRotation;

    private void Start()
    {
        LastParentRotation = transform.parent.rotation;
    }
    void Update()
    {
        transform.localRotation = Quaternion.Inverse(transform.parent.localRotation) * LastParentRotation * transform.localRotation;

        LastParentRotation = transform.parent.rotation;

    }
}
