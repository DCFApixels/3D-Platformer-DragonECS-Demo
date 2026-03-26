using DCFApixels.DragonECS;

namespace Platformer
{
    public class CoinHitSystem : IEcsRun, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class HitAspect : EcsAspect
        {
            public EcsPool<Hit> Hits = Inc;
        }
        class PlayerAspect : EcsAspect
        {
            public EcsPool<Player> Players = Inc;
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

                    if (hit.Other.CompareTag(Constants.Tags.CoinTag))
                    {
                        player.Coins += 1;
                        _gameData.S.UI.HUDCounter.text = player.Coins.ToString();
                    }

                    if (hit.Other.CompareTag(Constants.Tags.BadCoinTag))
                    {
                        player.Coins -= 1;
                        _gameData.S.UI.HUDCounter.text = player.Coins.ToString();
                    }
                }

            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        public void Inject(GameData obj) => _gameData = obj;
    }
}