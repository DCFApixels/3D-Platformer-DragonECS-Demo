using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    [System.Serializable]
    public struct PlayerInput : IEcsComponent
    {
        public Vector3 MoveInput;
    }
}