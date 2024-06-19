using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "HeroStaticData", menuName = "StaticData/HeroStats")]
    public class HeroStaticData : ScriptableObject
    {

        public HeroTyoeId HeroTyoeId;
        
        public int CurrentHP;
        public int MaxHP;
        public float Damage;
        public float Speed;

        
    }
}
