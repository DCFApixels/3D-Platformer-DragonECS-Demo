using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    [System.Serializable]
    public struct Dangerous : IEcsComponent
    {
        public Transform ObstacleTransform;
        public Vector3 PointA;
        public Vector3 PointB;
    }
}