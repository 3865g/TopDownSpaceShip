using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 5, 0);
    private Vector3 randomizeIntencity = new Vector3(2, 0.5f, 0);

    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.localPosition = new Vector3(Random.RandomRange(-randomizeIntencity.x, randomizeIntencity.x), Random.RandomRange(-randomizeIntencity.y, randomizeIntencity.y), Random.RandomRange(-randomizeIntencity.z, randomizeIntencity.z));
        rectTransform.localPosition += offset;
        //transform.localPosition += offset;
        //transform.localPosition = new Vector3(Random.RandomRange(-randomizeIntencity.x, randomizeIntencity.x), Random.RandomRange(-randomizeIntencity.y, randomizeIntencity.y), Random.RandomRange(-randomizeIntencity.z, randomizeIntencity.z));
    }
}
