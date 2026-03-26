using DCFApixels.DragonECS;

namespace Platformer
{
    public class BuffHitSystem : IEcsRun, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class HitAspect : EcsAspect
        {
            public EcsPool<Hit> Hits = Inc;
        }
        class PlayerAspect : EcsAspect
        {
            public EcsPool<Player> Players = Inc;
            public EcsPool<JumpBuff> JumpBuffs = Opt;
            public EcsPool<SpeedBuff> SpeedBuffs = Opt;
        }

        EcsDefaultWorld _world;
        GameData _gameData;

        public void Run()
        {
            var playerEs = _world.Where(out PlayerAspect playerAspect);
            foreach (var hitE in _world.Where(out HitAspect hitAspect))
            {
                ref var hit = ref hitAspect.Hits[hitE];

                foreach (var playerE in playerEs)
                {
                    ref var player = ref playerAspect.Players[playerE];

                    if (hit.Other.CompareTag(Constants.Tags.SpeedBuffTag))
                    {
                        hit.Other.gameObject.SetActive(false);
                        player.Speed *= 2f;
                        ref var speedBuff = ref playerAspect.SpeedBuffs.TryAddOrGet(playerE);
                        speedBuff.Timer = _gameData.C.speedBuffDuration;
                    }

                    if (hit.Other.CompareTag(Constants.Tags.JumpBuffTag))
                    {
                        hit.Other.gameObject.SetActive(false);
                        player.JumpForce *= 2f;
                        ref var jumpBuff = ref playerAspect.JumpBuffs.TryAddOrGet(playerE);
                        jumpBuff.Timer = _gameData.C.jumpBuffDuration;
                    }
                }

            }
        }


        public void Inject(EcsDefaultWorld obj) => _world = obj;
        public void Inject(GameData obj) => _gameData = obj;
    }
}
