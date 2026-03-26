using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private ConstData _constData;
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private RuntimeData _runtimeData;

        private EcsDefaultWorld _world;
        private EcsPipeline _pipeline;

        private void Start()
        {
            _world = new EcsDefaultWorld();

            var gameData = new GameData();
            gameData.C = _constData;
            gameData.S = _sceneData;
            gameData.R = _runtimeData;

            _pipeline = EcsPipeline.New()
                //init only systems
                .Add(new PlayerInitSystem())
                .Add(new DangerousInitSystem())
                //update systems
                .Add(new PlayerInputSystem())
                .Add(new DangerousRunSystem())
                .Add(new CoinHitSystem())
                .Add(new BuffHitSystem())
                .Add(new DangerousHitSystem())
                .Add(new WinHitSystem())
                .Add(new SpeedBuffSystem())
                .Add(new JumpBuffSystem())
                .Add(new LoseScreenSystem())
                .AutoDel<Hit>()
                //fixed update systems
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerJumpSystem())
                //di
                .Inject(_world, gameData)
                //unity integrations debugger
                .AddUnityDebug(_world)
                .BuildAndInit();

        }

        private void Update()
        {
            _pipeline.Run();
        }

        private void FixedUpdate()
        {
            _pipeline.FixedRun();
        }

        private void OnDestroy()
        {
            _pipeline.Destroy();
            _world.Destroy();
        }
    }
}