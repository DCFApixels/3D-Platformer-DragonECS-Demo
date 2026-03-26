using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class DangerousRunSystem : IEcsRun, IEcsInject<EcsDefaultWorld>
    {
        class Aspect : EcsAspect
        {
            public EcsPool<Dangerous> Dangerouses = Inc;
        }

        EcsDefaultWorld _world;

        public void Run()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                ref var dangerous = ref a.Dangerouses[e];
                Vector3 pos1 = dangerous.PointA;
                Vector3 pos2 = dangerous.PointB;

                dangerous.ObstacleTransform.localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time, 1.0f));
            }
        }

        public void Inject(EcsDefaultWorld obj) => _world = obj;
    }
}