using System;
using UnityEngine;

namespace Scripts.Logic
{
    public class ShieldPrefab : MonoBehaviour
    {
        public bool ReturnDamage;
        public float ReturnedDamage;
        internal void Construct( bool returnDamage, float returnedDamage)
        {
            ReturnDamage = returnDamage;
            ReturnedDamage = returnedDamage;

        }
    }
}


