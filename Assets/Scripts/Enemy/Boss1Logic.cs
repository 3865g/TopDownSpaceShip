using Scripts.Enemy;
using Scripts.Logic;
using Scripts.StaticData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

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

        foreach (GameObject gameObject in Turels)
        {
            IHealth health = gameObject.GetComponent<IHealth>();
            health.CurrentHP = 60;
            health.MaxHP = 60;
            gameObject.GetComponent<Attack>().Construct(heroTransform, 10);

            gameObject.GetComponent<IHealth>().MaxHP = 60;
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
