using Scripts.Enemy;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Logic;
using System.Threading;
using UnityEngine;

public class Boss1Logic : MonoBehaviour
{
    public GameObject[] Turels;
    public Transform heroTransform;
    public GameObject HealthBar;
    public GameObject HurtBox;
    public GameObject CollisionBox;

    int i;


    void Start()
    {
        heroTransform = GetComponent<Attack>()._heroTransform;

        HealthBar.SetActive(false);
        HurtBox.SetActive(false);
        CollisionBox.SetActive(true);


        foreach (GameObject turel in Turels)
        {
            IHealth health = turel.GetComponent<IHealth>();
            health.CurrentHP = 200;
            health.MaxHP = 200;
            health.TextPrefab = gameObject.GetComponent<IHealth>().TextPrefab;

        }

        InvokeRepeating("CheckTurels", 1f, 1f);
    }


    public void CheckTurels()
    {
        i = Turels.Length;


        foreach (GameObject gameObject in Turels)
        {
            if (gameObject == null)
            {
                i--;
            }
            else
            {
                i++;
            }
        }


        if (i == 0)
        {
            HurtBox.SetActive(true);
            CollisionBox.SetActive(false);
            HealthBar.SetActive(true);
        }
    }

    
}
