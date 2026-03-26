using DCFApixels.DragonECS;

namespace Platformer
{
    [System.Serializable]
    public struct SpeedBuff : IEcsComponent
    {
        public float Timer;
    }
}