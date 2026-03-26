using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    [System.Serializable]
    public struct Hit : IEcsComponent
    {
        public GameObject Target;
        public GameObject Other;
    }
}