using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class DangerousInitSystem : IEcsInit, IEcsInject<EcsDefaultWorld>
    {
        EcsDefaultWorld _world;
        public void Init()
        {
            var dangerousPool = _world.GetPool<Dangerous>();

            foreach (var i in GameObject.FindGameObjectsWithTag(Constants.Tags.DangerousTag))
            {
                var dangerousE = _world.NewEntity();

                dangerousPool.Add(dangerousE);
                ref var dangerous = ref dangerousPool.Get(dangerousE);

                dangerous.ObstacleTransform = i.transform;
                dangerous.PointA = i.transform.Find("A").position;
                dangerous.PointB = i.transform.Find("B").position;
            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
    }
}
