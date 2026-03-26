using DCFApixels.DragonECS;
using UnityEngine;


namespace Platformer
{
    public class PlayerJumpSystem : IEcsFixedRunProcess, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Player> Players = Inc;
            public EcsPool<PlayerInput> PlayerInputs = Inc;
            public EcsTagPool<IsGrounded> IsGroundeds = Inc;
        }
        class TryJumpAspect : EcsAspect
        {
            public EcsTagPool<TryJump> TryJumps = Inc;
        }

        EcsDefaultWorld _world;

        public void FixedRun()
        {
            foreach (var tryJumpE in _world.Where(out TryJumpAspect tryJumpA))
            {
                _world.DelEntity(tryJumpE);
                foreach (var playerE in _world.Where(out Aspect a))
                {
                    ref var player = ref a.Players.Get(playerE);

                    player.Rigidbody.AddForce(Vector3.up * player.JumpForce, ForceMode.VelocityChange);
                }
            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
    }
}