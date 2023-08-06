﻿using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "GateData", menuName = "StaticData/Gate")]
    public class GateStaticData : ScriptableObject
    {
        public GateTypeId GateTypeId;
        public AssetReferenceGameObject PrefabGateReference;
    }
}
