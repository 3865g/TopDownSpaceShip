﻿using System;
using UnityEngine;

namespace Scripts.Logic
{
    public class UniqueId : MonoBehaviour
    {
        public string Id;

        public void GenerateId()
        {
            Id = $"{gameObject.scene.name }_{Guid.NewGuid().ToString()}";
        }
    }
}