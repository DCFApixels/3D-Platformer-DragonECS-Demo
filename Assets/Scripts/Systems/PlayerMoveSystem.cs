using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class PlayerMoveSystem : IEcsFixedRun, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Player> Players = Inc;
            public EcsPool<PlayerInput> PlayerInputs = Inc;
        }

        EcsDefaultWorld _world;

        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                ref var player = ref a.Players[e];
                ref var playerInput = ref a.PlayerInputs[e];

                player.Rigidbody.AddForce(playerInput.MoveInput * player.Speed, ForceMode.Acceleration);
            }

        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
    }
}