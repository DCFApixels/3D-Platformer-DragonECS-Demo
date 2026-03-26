using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    [System.Serializable]
    public struct Player : IEcsComponent
    {
        public Transform Transform;
        public Rigidbody Rigidbody;
        public CapsuleCollider Collider;
        public Vector3 Velocity;
        public float JumpForce;
        public float Speed;
        public int Coins;
    }
}