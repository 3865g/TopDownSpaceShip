using Scripts.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{

    public float DamageAmount;
    public float DamageInterval;
    public int DamgaeDuration;

    public bool CanDamage;

    public GameObject BurningModule;


    private GameObject _burningModule;

    private const string PlayerTag = "Player";

    private void OnTriggerEnter(Collider collision)
    {
        if (CanDamage && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.parent.GetComponentInChildren<BurningDamage>() == null)
            {
                _burningModule = Instantiate(BurningModule, collision.transform.parent);
                _burningModule.transform.SetParent(collision.transform);
                //Debug.Log(_burningModule);
                BurningDamage burningDamage = _burningModule.GetComponent<BurningDamage>();
                burningDamage.Construct(collision.gameObject);
            }
            else
            {
                collision.transform.parent.GetComponentInChildren<BurningDamage>().DamgaeDuration = DamgaeDuration;
            }
        }
    }

}
