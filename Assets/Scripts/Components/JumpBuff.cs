using DCFApixels.DragonECS;

namespace Platformer
{
    [System.Serializable]
    public struct JumpBuff : IEcsComponent
    {
        public float Timer;
    }
}
