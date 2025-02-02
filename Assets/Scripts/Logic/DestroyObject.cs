using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    private void Start()
    {
       
       gameObject.SetActive(true);
        
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
    
}
