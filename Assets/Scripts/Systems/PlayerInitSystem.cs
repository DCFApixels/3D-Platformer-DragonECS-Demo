using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class PlayerInitSystem : IEcsInit, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class PlayerAspect : EcsAspect
        {
            public EcsPool<Player> Players = Inc;
        }

        EcsDefaultWorld _world;
        GameData _gameData;

        public void Init()
        {
            var playerE = _world.NewEntity();

            var playerPool = _world.GetPool<Player>();
            playerPool.Add(playerE);
            ref var player = ref playerPool.Get(playerE);
            var playerInputPool = _world.GetPool<PlayerInput>();
            playerInputPool.Add(playerE);
            ref var playerInput = ref playerInputPool.Get(playerE);

            var playerGO = GameObject.FindGameObjectWithTag("Player");
            playerGO.GetComponentInChildren<GroundCheckerView>().groundedPool = _world.GetPool<IsGrounded>();
            playerGO.GetComponentInChildren<GroundCheckerView>().playerEntity = playerE;
            playerGO.GetComponentInChildren<CollisionCheckerView>().ecsWorld = _world;
            player.Speed = _gameData.C.playerSpeed;
            player.Transform = playerGO.transform;
            player.JumpForce = _gameData.C.playerJumpForce;
            player.Collider = playerGO.GetComponent<CapsuleCollider>();
            player.Rigidbody = playerGO.GetComponent<Rigidbody>();
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        public void Inject(GameData obj) => _gameData = obj;
    }
}