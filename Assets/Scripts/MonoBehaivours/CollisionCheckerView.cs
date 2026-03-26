using DCFApixels.DragonECS;
using UnityEngine;

namespace Platformer
{
    public class CollisionCheckerView : MonoBehaviour
    {
        // auto-injected fields.
        public EcsWorld ecsWorld { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            var hit = ecsWorld.NewEntity();

            var hitPool = ecsWorld.GetPool<Hit>();
            hitPool.Add(hit);
            ref var hitComponent = ref hitPool.Get(hit);

            hitComponent.Target = transform.root.gameObject;
            hitComponent.Other = collision.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tags.CoinTag) || other.CompareTag(Constants.Tags.BadCoinTag))
            {
                // instantly destroy coin to avoid multiple OnTriggerEnter() calls.
                other.gameObject.SetActive(false);
            }
            var hit = ecsWorld.NewEntity();

            var hitPool = ecsWorld.GetPool<Hit>();
            hitPool.Add(hit);
            ref var hitComponent = ref hitPool.Get(hit);

            hitComponent.Target = transform.root.gameObject;
            hitComponent.Other = other.gameObject;
        }
    }
}