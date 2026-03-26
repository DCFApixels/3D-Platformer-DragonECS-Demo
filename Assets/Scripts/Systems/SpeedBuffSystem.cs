using DCFApixels.DragonECS;
using UnityEngine;


namespace Platformer
{
    public class SpeedBuffSystem : IEcsRun, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Player> Players = Inc;
            public EcsPool<SpeedBuff> SpeedBuffs = Inc;
        }

        EcsDefaultWorld _world;

        public void Run()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                ref var player = ref a.Players[e];
                ref var speedBuff = ref a.SpeedBuffs[e];

                speedBuff.Timer -= Time.deltaTime;

                if (speedBuff.Timer <= 0)
                {
                    player.Speed /= 2f;
                    a.SpeedBuffs.Del(e);
                }
            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
    }
}