using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    [System.Serializable]
    public struct CameraController : IEcsComponent
    {
        public Transform Transform;
        public Vector3 Velocity;
        public Vector3 Offset;
        public float Smoothness;
    }
}