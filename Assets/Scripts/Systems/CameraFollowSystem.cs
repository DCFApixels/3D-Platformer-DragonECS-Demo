using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class CameraFollowSystem : IEcsInit, IEcsFixedRunProcess, IEcsInject<EcsDefaultWorld>, IEcsInject<GameData>
    {
        class CameraAspect : EcsAspect
        {
            public EcsPool<CameraController> CameraControllers = Inc;
        }
        class PlayerAspect : EcsAspect
        {
            public EcsPool<Player> Players = Inc;
        }

        EcsDefaultWorld _world;
        GameData _gameData;

        private entlong cameraEntity;

        public void Init()
        {
            var cameraEntity = _world.NewEntity();
            var cameraAspect = _world.GetAspect<CameraAspect>();
            ref var camera = ref cameraAspect.CameraControllers.Add(cameraEntity);

            camera.Transform = _gameData.S.Camera.transform;
            camera.Smoothness = _gameData.C.cameraFollowSmoothness;
            camera.Velocity = Vector3.zero;
            camera.Offset = new Vector3(0f, 1f, -9f);

            this.cameraEntity = (cameraEntity, _world);
        }

        public void FixedRun()
        {
            var cameraAspect = _world.GetAspect<CameraAspect>();
            ref var camera = ref cameraAspect.CameraControllers.Get(cameraEntity.ID);

            foreach (var e in _world.Where(out PlayerAspect playerA))
            {
                ref var player = ref playerA.Players[e];

                Vector3 currentPosition = camera.Transform.position;
                Vector3 targetPoint = player.Transform.position + camera.Offset;

                camera.Transform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref camera.Velocity, camera.Smoothness);
            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
        public void Inject(GameData obj) => _gameData = obj;
    }
}
