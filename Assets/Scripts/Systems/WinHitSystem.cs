using DCFApixels.DragonECS;

namespace Platformer
{
    public class WinHitSystem : IEcsRun, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
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
            var playerEs = _world.Where(out PlayerAspect playerA);
            foreach (var hitE in _world.Where(out HitAspect hitA))
            {
                ref var hit = ref hitA.Hits[hitE];

                foreach (var playerE in playerEs)
                {
                    ref var player = ref playerA.Players[playerE];

                    if (hit.Other.CompareTag(Constants.Tags.WinPointTag))
                    {
                        player.Transform.gameObject.SetActive(false);
                        if (_world.IsUsed(playerE))
                        {
                            _world.DelEntity(playerE);
                        }
                        _gameData.S.UI.WinScreen.gameObject.SetActive(true);
                    }
                }

            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        public void Inject(GameData obj) => _gameData = obj;
    }
}