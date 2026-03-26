using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class JumpBuffSystem : IEcsRun, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Player> players = Inc;
            public EcsPool<JumpBuff> jumpBuffs = Inc;
        }

        EcsDefaultWorld _world;

        public void Run()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                ref var player = ref a.players.Get(e);
                ref var jumpBuff = ref a.jumpBuffs.Get(e);

                jumpBuff.Timer -= Time.deltaTime;

                if (jumpBuff.Timer <= 0)
                {
                    player.JumpForce /= 2f;
                    a.jumpBuffs.Del(e);
                }
            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
    }

}
