using System.Collections.Generic;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;

// Unity IL2CPP performance optimization attribute.
namespace Unity.IL2CPP.CompilerServices
{
    using System;
    internal enum Option 
    {
        NullChecks = 1,
        ArrayBoundsChecks = 2,
        DivideByZeroChecks = 3,
    }
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Struct | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate, Inherited = false, AllowMultiple = true)]
    internal class Il2CppSetOptionAttribute : Attribute
    {
        public Option Option { get; private set; }
        public object Value { get; private set; }
        public Il2CppSetOptionAttribute(Option option, object value)
        {
            Option = option;
            Value = value;
        }
    }
}
#endif

namespace DCFApixels.DragonECS
{
    using static EcsOneFrameComponentConsts;

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [MetaGroup(EcsConsts.PACK_GROUP, "Utils")]
    [MetaDescription(EcsConsts.AUTHOR, "System for automatic removal of components of the selected type.")]
    [MetaTags(MetaTags.HIDDEN)]
    [MetaColor(MetaColor.Grey)]
    internal class DeleteOneFrameComponentSystem<TComponent> : IEcsRun, IEcsInject<EcsWorld>
        where TComponent : struct, IEcsComponent
    {
        private sealed class Aspect : EcsAspect
        {
            public readonly EcsPool<TComponent> pool = Inc;
        }
        private readonly List<EcsWorld> _worlds = new List<EcsWorld>();
        private int _runsCount = 0;
        public void Run()
        {
            for (int i = 0, iMax = _worlds.Count; i < iMax; i++)
            {
                EcsWorld world = _worlds[i];
                if (world.IsDestroyed == false && world.IsComponentTypeDeclared<TComponent>())
                {
                    world.GetAspect<Aspect>().pool.ClearAll();
                }
                else
                {
                    if (_runsCount > 2)
                    {
                        _worlds.RemoveAt(i);
                        iMax = _worlds.Count;
                        i--;
                    }
                }
            }
            _runsCount++;
        }
        public void Inject(EcsWorld obj)
        {
            _worlds.Add(obj);
            _runsCount = 0;
        }
    }
    [MetaGroup(EcsConsts.PACK_GROUP, "Utils")]
    [MetaDescription(EcsConsts.AUTHOR, "System for automatic removal of components of the selected type. Removes tag components.")]
    [MetaTags(MetaTags.HIDDEN)]
    [MetaColor(MetaColor.Grey)]
    internal class DeleteOneFrameTagComponentSystem<TComponent> : IEcsRun, IEcsInject<EcsWorld>
        where TComponent : struct, IEcsTagComponent
    {
        private sealed class Aspect : EcsAspect
        {
            public readonly EcsTagPool<TComponent> pool = Inc;
        }
        private readonly List<EcsWorld> _worlds = new List<EcsWorld>();
        private int _runsCount = 0;
        public void Run()
        {
            for (int i = 0, iMax = _worlds.Count; i < iMax; i++)
            {
                EcsWorld world = _worlds[i];
                if (world.IsDestroyed == false && world.IsComponentTypeDeclared<TComponent>())
                {
                    world.GetAspect<Aspect>().pool.ClearAll();
                }
                else
                {
                    if (_runsCount > 2)
                    {
                        _worlds.RemoveAt(i);
                        iMax = _worlds.Count;
                        i--;
                    }
                }
            }
            _runsCount++;
        }
        public void Inject(EcsWorld obj)
        {
            _worlds.Add(obj);
            _runsCount = 0;
        }
    }

    internal static class EcsOneFrameComponentConsts
    {
        public const string AUTO_DEL_LAYER = nameof(AUTO_DEL_LAYER);
    }
    public static class DeleteOneFrameComponentSystemExtensions
    {
        public static EcsPipeline.Builder AutoDel<TComponent>(this EcsPipeline.Builder b, string layerName = null)
            where TComponent : struct, IEcsComponent
        {
            if (AUTO_DEL_LAYER == layerName)
            {
                b.Layers.Add(AUTO_DEL_LAYER).After(EcsConsts.POST_END_LAYER);
            }
            b.AddUnique(new DeleteOneFrameComponentSystem<TComponent>(), layerName);
            return b;
        }
        public static EcsPipeline.Builder AutoDelToEnd<TComponent>(this EcsPipeline.Builder b)
            where TComponent : struct, IEcsComponent
        {
            b.Layers.Add(AUTO_DEL_LAYER).After(EcsConsts.POST_END_LAYER);
            b.AddUnique(new DeleteOneFrameComponentSystem<TComponent>(), AUTO_DEL_LAYER);
            return b;
        }
    }
    internal static class DeleteOneFrameTagComponentSystemExtensions
    {
        public static EcsPipeline.Builder AutoDel<TComponent>(this EcsPipeline.Builder b, string layerName = null)
            where TComponent : struct, IEcsTagComponent
        {
            if (AUTO_DEL_LAYER == layerName)
            {
                b.Layers.Add(AUTO_DEL_LAYER).After(EcsConsts.POST_END_LAYER);
            }
            b.AddUnique(new DeleteOneFrameTagComponentSystem<TComponent>(), layerName);
            return b;
        }
        public static EcsPipeline.Builder AutoDelToEnd<TComponent>(this EcsPipeline.Builder b)
            where TComponent : struct, IEcsTagComponent
        {
            b.Layers.Add(AUTO_DEL_LAYER).After(EcsConsts.POST_END_LAYER);
            b.AddUnique(new DeleteOneFrameTagComponentSystem<TComponent>(), AUTO_DEL_LAYER);
            return b;
        }
    }
}