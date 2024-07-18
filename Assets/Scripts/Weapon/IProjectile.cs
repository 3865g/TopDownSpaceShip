
using UnityEngine;

namespace Scripts.Weapon
{
    public interface IProjectile
    {
        public Color Color { get; set; }
        void Construct(Vector3 Direction, float damage, Color color);
    }
}

