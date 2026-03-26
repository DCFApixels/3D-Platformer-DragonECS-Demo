using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class PlayerInputSystem : IEcsRun, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<PlayerInput> PlayerInputs = Inc;
            public EcsTagPool<TryJump> TryJumps = Opt;
        }

        EcsDefaultWorld _world;
        GameData _gameData;

        public void Run()
        {

            foreach (var e in _world.Where(out Aspect a))
            {
                ref var playerInput = ref a.PlayerInputs[e];

                playerInput.MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    var tryJump = _world.NewEntity();
                    a.TryJumps.Add(tryJump);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    _gameData.S.ReloadScene();
                }
            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        public void Inject(GameData obj) => _gameData = obj;
    }
}