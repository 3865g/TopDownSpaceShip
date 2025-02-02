using UnityEngine;

public class SimpleObjectSpawner : MonoBehaviour
{
    public GameObject GameObject;
    void Start()
    {
        GameObject.SetActive(true);
        //Instantiate(GameObject, gameObject.transform);   
    }
}
